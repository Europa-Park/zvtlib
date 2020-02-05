using System.Collections.Generic;
using Wiffzack.Devices.CardTerminals.Commands;
using Wiffzack.Devices.CardTerminals.PrintSupport;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;

namespace Cynox.ZvtEcrInterface {
	/// <summary>
	/// A high level representation of PaymentResult response data for easy use.
	/// </summary>
	public class ZvtPaymentResult {
		public bool Success { get; }
		public int? ProtocolSpecificErrorCode { get; }
		public string ProtocolSpecificErrorDescription { get; }
		public ZvtStatusInformation StatusInformation { get; }
		public List<IPrintDocument> PrintDocuments { get; }

		public ZvtPaymentResult(PaymentResult paymentResult) {
			ProtocolSpecificErrorCode = paymentResult.ProtocolSpecificErrorCode;
			ProtocolSpecificErrorDescription = paymentResult.ProtocolSpecificErrorDescription;
			Success = paymentResult.Success;

			var statusInfo = paymentResult.Data as StatusInformationApdu;
			StatusInformation = new ZvtStatusInformation(statusInfo);
			PrintDocuments = new List<IPrintDocument>();
			PrintDocuments.AddRange(paymentResult.PrintDocuments);
		}
	}
}
