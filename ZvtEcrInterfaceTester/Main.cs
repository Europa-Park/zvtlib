using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cynox.ZvtEcrInterface;
using Wiffzack.Devices.CardTerminals.Commands;
using Wiffzack.Devices.CardTerminals.PrintSupport;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV;
using ZvtEcrInterface;

namespace ZvtEcrInterfaceTester {
	public partial class Main : Form {
		private ZvtCommunication _ZvtCommunication;

		public Main() {
			InitializeComponent();
			cComPort.DataSource = SerialPort.GetPortNames();
			
			if (cComPort.Items.Count >= 1) {
				cComPort.SelectedIndex = 0;
				cComPort.SelectedItem = Properties.Settings.Default.Port;
			}

			cBaud.SelectedIndex = 0;
			cBaud.SelectedItem = Properties.Settings.Default.Baud.ToString();
		}

		private void Main_Load(object sender, EventArgs e) {
			Text = Application.ProductName + @" " + Program.CreateVersionString(true);
		}

		private void bCreateInterface_Click(object sender, EventArgs e) {
			try {
				TextWriter logger = Console.Out;

				if (cLogToTextBox.Checked) {
					logger = new TextBoxWriter(tLog);
				}

				_ZvtCommunication = new ZvtCommunication(new ZvtSerialPortSettings(cComPort.SelectedItem.ToString(), int.Parse(cBaud.SelectedItem.ToString())), logger);
				_ZvtCommunication.EnvironmentStatus += ZvtCommunicationOnEnvironmentStatus;
				
				Log("ZvtCommunication ready");
			} catch (Exception ex) {
				Log(ex);
			}
		}

		private void ZvtCommunicationOnEnvironmentStatus(IntermediateStatus status) {
			if (InvokeRequired) {
				Invoke(new Action(() => ZvtCommunicationOnEnvironmentStatus(status)));
				return;
			}

			Log($"ZvtEnvironment StatusCode: {status}");
		}

#region Logging

		private void Log(string text, bool lineBreak = true) {
			if (string.IsNullOrEmpty(text)) {
				return;
			}

			tLog.AppendText(text);

			if (lineBreak) {
				tLog.AppendText(Environment.NewLine);
			}
		}

		private void Log(Exception e) {
			Log(e?.ToString().Replace(":", "\0")); // hier nur exception typ bis zum ":" loggen, nicht den ganzen stack trace
			Log(e?.Message);
			Log(e?.InnerException?.Message);
		}

		// Druckdokumente ohne spezielle Zeilenformatierung ausgeben
		private void Log(List<IPrintDocument> documents) {
			foreach (var printDocument in documents) {
				// ein IPrintLine kann mehrere IPrintText Commands enthalten (z.B. wenn in einer Zeile sowohl links als auch rechtbündig Text steht)
				foreach (var printLine in printDocument.PrintLines) {
					if (printLine.Commands.Any()) {
						foreach (var lineCommand in printLine.Commands) {
							Log(lineCommand.Text);
						}
					} else {
						// wenn keine Commands vorhanden sind -> Leerzeile
						Log("");
					}
				}
			}
		}

		private void Log(CommandResult result) {
			if (result == null) {
				Log("CommandResult is NULL");
				return;
			}

			Log($"Success: {result.Success}");

			if (result.ProtocolSpecificErrorCode != null) {
				Log($"ErrorCode: {result.ProtocolSpecificErrorCode} ErrorDescription: {result.ProtocolSpecificErrorDescription}");
			}

			Log(result.PrintDocuments.ToList());
		}

		private void Log(ZvtPaymentResult result) {
			if (result == null) {
				Log("ZvtPaymentResult is NULL");
				return;
			}

			Log($"Success: {result.Success}");
			Log($"Status: {result.StatusInformation.Status}");
			Log($"Status: {result.StatusInformation.PaymentType}");

			propertyGrid1.SelectedObject = result.StatusInformation;

			if (result.ProtocolSpecificErrorCode != null) {
				Log($"ErrorCode: {result.ProtocolSpecificErrorCode} ErrorDescription: {result.ProtocolSpecificErrorDescription}");
			}

			foreach (var printDocument in result.PrintDocuments) {
				foreach (var printLine in printDocument.PrintLines) {
					if (printLine.Commands.Any()) {
						foreach (var lineCommand in printLine.Commands) {
							Log(lineCommand.Text);
						}
					} else {
						Log("");
					}
				}
			}
		}

#endregion

		private void Main_FormClosing(object sender, FormClosingEventArgs e) {
			_ZvtCommunication?.Dispose();
			Properties.Settings.Default.Baud = int.Parse(cBaud.SelectedItem.ToString());
			Properties.Settings.Default.Port = cComPort.SelectedItem.ToString();
			Properties.Settings.Default.Save();
		}

		private void bRegister_Click(object sender, EventArgs e) {
			try {
				Cursor.Current = Cursors.WaitCursor;
				var tlvList = new List<TLVItem>();
				var permittedCommandTlv = new TLVItem(new TLVTag(TagValue.ListOfPermittedZvtCommands));

				// Status Info
				if (cPermStatus.Checked) {
					permittedCommandTlv.SubItems.Add(new TLVItem(new TLVTag(TagValue.ZvtCommand), new List<byte> { 0x04, 0x0F })); 
				}

				// Immediate Info
				if (cPermInterStatus.Checked) {
					permittedCommandTlv.SubItems.Add(new TLVItem(new TLVTag(TagValue.ZvtCommand), new List<byte> { 0x04, 0xFF })); 
				}

				// Completion
				if (cPermCompletion.Checked) {
					permittedCommandTlv.SubItems.Add(new TLVItem(new TLVTag(TagValue.ZvtCommand), new List<byte> { 0x06, 0x0F })); 
				}

				// Abort
				if (cPermAbort.Checked) {
					permittedCommandTlv.SubItems.Add(new TLVItem(new TLVTag(TagValue.ZvtCommand), new List<byte> { 0x06, 0x1E })); 
				}

				// Print Line
				if (cPermLinePrint.Checked) {
					permittedCommandTlv.SubItems.Add(new TLVItem(new TLVTag(TagValue.ZvtCommand), new List<byte> { 0x06, 0xD1 })); 
				}

				// Print Block
				if (cPermBlockPrint.Checked) {
					permittedCommandTlv.SubItems.Add(new TLVItem(new TLVTag(TagValue.ZvtCommand), new List<byte> { 0x06, 0xD3 })); 
				}

				if (permittedCommandTlv.SubItems.Any()) {
					tlvList.Add(permittedCommandTlv);
				}

				var result = _ZvtCommunication?.Register(tlvList);
				Log(result);
			} catch (Exception ex) {
				Log(ex);
			} finally {
				Cursor.Current = Cursors.Default;
			}
		}
		
		private void RegisterAsyncCallback(InitialisationResult result, Exception ex) {
			Log(result);
			Log(ex);
		}

		private void bRegisterAsync_Click(object sender, EventArgs e) {
			_ZvtCommunication?.RegisterAsync(RegisterAsyncCallback);
		}

		private void bPayment_Click(object sender, EventArgs e) {
			try {
				Cursor.Current = Cursors.WaitCursor;
				//_ZvtCommunication.Register()
				var result = _ZvtCommunication?.Pay((uint)numPayAmount.Value);
				Log(result);
			} catch (Exception ex) {
				Log(ex);
			} finally {
				Cursor.Current = Cursors.Default;
			}
		}

		private void PayAsyncCallback(ZvtPaymentResult result, Exception ex) {
			Log(result);
			Log(ex);
		}

		private void bPayAsync_Click(object sender, EventArgs e) {
			_ZvtCommunication.PayAsync((uint)numPayAmount.Value, PayAsyncCallback);
		}

		private void bRefund_Click(object sender, EventArgs e) {
			try {
				Cursor.Current = Cursors.WaitCursor;
				var result = _ZvtCommunication?.Refund((uint)numRefundAmount.Value);
				Log(result);
			} catch (Exception ex) {
				Log(ex);
			} finally {
				Cursor.Current = Cursors.Default;
			}
		}

		private void bReversal_Click(object sender, EventArgs e) {
			try {
				Cursor.Current = Cursors.WaitCursor;
				var result = _ZvtCommunication?.ReversePayment((int)numReceiptNumber.Value);
				Log(result);
			} catch (Exception ex) {
				Log(ex);
			} finally {
				Cursor.Current = Cursors.Default;
			}
		}

		private void ReverseAsyncCallback(ZvtPaymentResult result, Exception ex) {
			Log(result);
			Log(ex);
		}

		private void bReverseAsync_Click(object sender, EventArgs e) {
			_ZvtCommunication?.ReversePaymentAsync((int)numReceiptNumber.Value, ReverseAsyncCallback);
		}

		private void bClearLog_Click(object sender, EventArgs e) {
			tLog.Clear();
		}

		private void bEndOfDay_Click(object sender, EventArgs e) {
			try {
				Cursor.Current = Cursors.WaitCursor;
				var result = _ZvtCommunication?.EndOfDay();
				Log(result);
			} catch (Exception ex) {
				Log(ex);
			} finally {
				Cursor.Current = Cursors.Default;
			}
		}
	}

	public class TextBoxWriter : TextWriter {
		// The control where we will write text.
		private readonly TextBox _TextBox;
		public TextBoxWriter(TextBox control) {
			_TextBox = control;
		}

		public override void Write(char value) {
			_TextBox.AppendText(value.ToString());
		}

		public override void Write(string value) {
			_TextBox.AppendText(value);
		}

		public override Encoding Encoding {
			get { return Encoding.Unicode; }
		}
	}
}
