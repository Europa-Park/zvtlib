using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class NetworkDiagnosisCommand
    {
          /// <summary>
        /// Transportlayer to use
        /// </summary>
        private IZvtTransport _transport;

        /// <summary>
        /// Registration APDU
        /// </summary>
        private DiagnosisApdu _apdu;

        private ICommandTransmitter _commandTransmitter;

        public NetworkDiagnosisCommand(IZvtTransport transport)
        {
            _transport = transport;
            _apdu = new DiagnosisApdu();
            _commandTransmitter = new MagicResponseCommandTransmitter(_transport);
            _commandTransmitter.ResponseReceived += new Action<IZvtApdu>(_commandTransmitter_ResponseReceived);
        }

        private void _commandTransmitter_ResponseReceived(IZvtApdu responseApdu)
        {

        }

     
        public void Execute()
        {
            _transport.OpenConnection();
            ApduCollection responses = _commandTransmitter.TransmitAPDU(_apdu);
            _transport.CloseConnection();
        }
    }
}
