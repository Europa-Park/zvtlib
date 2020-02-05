using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Cynox.ZvtEcrInterface;
using Wiffzack.Devices.CardTerminals;
using Wiffzack.Devices.CardTerminals.Commands;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;
using Wiffzack.Diagnostic.Log;

namespace ZvtEcrInterface {
	public class ZvtCommunication : IDisposable {
		/// <summary>
		/// Reports intermediate status events during command execution.
		/// </summary>
		public event IntermediateStatusDelegate EnvironmentStatus;

		private readonly ZVTCommandEnvironment _ZvtEnvironment;
		private BackgroundWorker _AsyncCommandWorker;

		/// <summary>
		/// Specifies if the connection should be kept open after executing a command.
		/// </summary>
		public bool KeepConnectionOpen { get; set; }

		/// <summary>
		/// Returns true, if an asynchronous command request is still running.
		/// </summary>
		public bool IsAsyncCommandRunning => _AsyncCommandWorker?.IsBusy ?? false;

		/// <summary>
		/// Creates a new instance of the ZvtEcrCommunication.
		/// </summary>
		/// <param name="portSettings">Specifies the serial port where the payment terminal is connected.</param>
		/// <param name="logger">Specifies where logging information should be written to.</param>
		/// <param name="logLevel">Specifies the log verbosity.</param>
		public ZvtCommunication(ZvtSerialPortSettings portSettings, TextWriter logger, LogLevel logLevel = LogLevel.Everything) {
			KeepConnectionOpen = false;

			var textLogger = new TextLogger(null, logLevel, @"ZVTCommandEnvironment", logger);
			LogManager.Global = new LogManager(true, textLogger);

			XElement el = new XElement("Config",
				new XElement("Transport", "Serial"),
				new XElement("TransportSettings",
					new XElement("Port", portSettings.Port),
					new XElement("BaudRate", portSettings.Baud),
					new XElement("StopBits", portSettings.StopBits),
					new XElement("Parity", portSettings.Parity)
				)
			);

			_ZvtEnvironment = new ZVTCommandEnvironment(el.ToXmlElement());
			_ZvtEnvironment.StatusReceived += ZvtEnvironmentOnStatusReceived;

			// keep COM port open
			_ZvtEnvironment.OpenConnection += () => true;
			_ZvtEnvironment.CloseConnection += () => !KeepConnectionOpen;
		}

		#region IDisposable

		// Flag: Has Dispose already been called?
		bool _Disposed;

		// Public implementation of Dispose pattern callable by consumers.
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Protected implementation of Dispose pattern.
		protected virtual void Dispose(bool disposing) {
			if (_Disposed) {
				return;
			}

			if (disposing) {
				_ZvtEnvironment.Dispose();
			}

			// Free any unmanaged objects here.
			_Disposed = true;
		}

		~ZvtCommunication() {
			Dispose(false);
		}

		#endregion

		#region Private Methods

		private void ZvtEnvironmentOnStatusReceived(IntermediateStatus status) {
			EnvironmentStatus?.Invoke(status);
		}

		/// <summary>
		/// Asychrounously executes <param name="workHandler"></param> workHandler and calls completedHandler when finished.
		/// </summary>
		/// <param name="workHandler">Will be executed on BackgroundWorker thread.</param>
		/// <param name="completedHandler">Will be called when BackgroundWorker is completed.</param>
		private void RunCommandWorker(Action workHandler, Action<Exception> completedHandler) {
			_AsyncCommandWorker = new BackgroundWorker();
			_AsyncCommandWorker.WorkerReportsProgress = false;
			_AsyncCommandWorker.WorkerSupportsCancellation = false;

			_AsyncCommandWorker.DoWork += delegate {
				workHandler?.Invoke();
			};

			_AsyncCommandWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args) {
				completedHandler.Invoke(args.Error);
			};
			
			_AsyncCommandWorker.RunWorkerAsync();
		}

		#endregion

		#region Public Command Interface

		/// <summary>
		/// Using the command Registration, the ECR can set up different configurations on the PT
		/// and also control the current status of the PT.
		/// </summary>
		/// <returns>Result of the registering process. Returns null if terminal is not responding after timeout.</returns>
		public InitialisationResult Register(List<TLVItem> tlvParameters = null) {
			try {
				InitialisationResult result = _ZvtEnvironment.CreateInitialisationCommand(null, tlvParameters).Execute();
				return result;
			} catch (ConnectionTimeOutException e) {
				Debug.WriteLine(e.Message);
				return null;
			}
		}

		/// <summary>
		/// /// Refer to <see cref="Register"/>
		/// <list type="bullet">
		/// <item><term>Callback will return InitialisationResult as null if another asyc command is still running.</term></item>
		/// </list>
		/// </summary>
		/// <param name="callback">Callback to be executed when the operation is finished.</param>
		public void RegisterAsync(Action<InitialisationResult, Exception> callback) {
			if (IsAsyncCommandRunning) {
				callback?.Invoke(null, null);
				return;
			}

			InitialisationResult result = null;
			RunCommandWorker(() => result = Register(), ex => callback?.Invoke(result, ex));
		}

		/// <summary>
		/// This command initiates a payment process and transmits the corresponding amount.
		/// </summary>
		/// <param name="amount">Amount to pay in smallest currency unit.</param>
		/// <returns>Result of the payment process. Returns null if terminal is not responding after timeout.</returns>
		public ZvtPaymentResult Pay(uint amount) {
			try {
				XElement el = new XElement("Payment", 
					new XElement("Amount", amount.ToString()));

				PaymentResult result = _ZvtEnvironment.CreatePaymentCommand(el.ToXmlElement()).Execute();

				return (result != null) ? new ZvtPaymentResult(result) : null;
			} catch (ConnectionTimeOutException e) {
				Debug.WriteLine(e.Message);
				return null;
			}
		}

		/// <summary>
		/// Refer to <see cref="Pay(uint)"/>
		/// <list type="bullet">
		/// <item><term>Callback will return PaymentResult as null if another asyc command is still running.</term></item>
		/// </list>
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="callback">Callback to be executed when the operation is finished.</param>
		public void PayAsync(uint amount, Action<ZvtPaymentResult, Exception> callback) {
			if (IsAsyncCommandRunning) {
				callback?.Invoke(null, null);
				return;
			}

			ZvtPaymentResult result = null;
			RunCommandWorker(() => result = Pay(amount), ex => callback?.Invoke(result, ex));
		}

		/// <summary>
		/// This command starts a Refund.
		/// </summary>
		/// <param name="amount">Amount to pay in smallest currency unit.</param>
		/// <returns>Result of the payment process. Returns null if terminal is not responding after timeout.</returns>
		public ZvtPaymentResult Refund(uint amount) {
			try {
				XElement el = new XElement("Refund",
					new XElement("Amount", amount.ToString()));

				PaymentResult result = _ZvtEnvironment.CreateRefundCommand(el.ToXmlElement()).Execute();

				return (result != null) ? new ZvtPaymentResult(result) : null;
			} catch (ConnectionTimeOutException e) {
				Debug.WriteLine(e.Message);
				return null;
			}
		}

		/// <summary>
		/// This command reverses a payment-procedure and transfers the receipt-number of the transaction to be reversed.
		/// <list type="bullet">
		/// <item><term>Will return null if termimal is not responding after timeout.</term></item>
		/// </list>
		/// </summary>
		/// <param name="receiptNumber">Receipt-number of the transaction to be reversed.</param>
		/// <returns>Result of the reversal process. Returns null if terminal is not responding after timeout.</returns>
		public ZvtPaymentResult ReversePayment(int receiptNumber) {
			try {
				ReversalCommand revers = (ReversalCommand)_ZvtEnvironment.CreateReversalCommand(null);
				revers.ReceiptNr = receiptNumber; //(int)XmlHelper.ReadInt((XmlElement)config.DocumentElement.SelectSingleNode("Reversal"), "ReceiptNr");
				PaymentResult result = revers.Execute();

				return (result != null) ? new ZvtPaymentResult(result) : null;
			} catch (ConnectionTimeOutException e) {
				Debug.WriteLine(e.Message);
				return null;
			}
		}

		/// <summary>
		/// Refer to <see cref="ReversePayment(int)"/>.
		/// <list type="bullet">
		/// <item><term>Callback will return PaymentResult as null if another asyc command is still running.</term></item>
		/// </list>
		/// </summary>
		/// <param name="receiptNumber">Receipt-number of the transaction to be reversed.</param>
		/// <param name="callback">Callback to be executed when the operation is finished.</param>
		public void ReversePaymentAsync(int receiptNumber, Action<ZvtPaymentResult, Exception> callback) {
			if (IsAsyncCommandRunning) {
				callback?.Invoke(null, null);
				return;
			}

			ZvtPaymentResult result = null;
			RunCommandWorker(() => result = ReversePayment(receiptNumber), ex => callback?.Invoke(result, ex));
		}

		/// <summary>
		/// With this command the ECR induces the PT to transfer the stored turnover to the host.
		/// </summary>
		/// <returns></returns>
		public CommandResult EndOfDay() {
			try {
				var result = _ZvtEnvironment.CreateEndOfDayCommand(null).Execute();
				return result;
			} catch (ConnectionTimeOutException e) {
				Debug.WriteLine(e.Message);
				return null;
			}
		}

		#endregion
	}
}
