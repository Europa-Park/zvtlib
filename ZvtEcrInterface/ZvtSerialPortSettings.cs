using System.IO.Ports;

namespace Cynox.ZvtEcrInterface {
	/// <summary>
	/// Settings for serial port communication using ZvtCommunication class.
	/// </summary>
	public class ZvtSerialPortSettings {
		public string Port { get; set; }
		public int Baud { get; set; }
		public Parity Parity { get; set; }
		public StopBits StopBits { get; set; }

		public ZvtSerialPortSettings(string port, int baud = 9600, Parity parity = Parity.None, StopBits stopBits = StopBits.Two) {
			Port = port;
			Baud = baud;
			Parity = parity;
			StopBits = stopBits;
		}
	}
}
