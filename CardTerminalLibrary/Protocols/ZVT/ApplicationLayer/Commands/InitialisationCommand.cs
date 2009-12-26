using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class InitialisationCommand
    {
        /// <summary>
        /// Transportlayer to use
        /// </summary>
        private IZvtTransport _transport;

        /// <summary>
        /// Registration APDU
        /// </summary>
        private InitialisationApdu _initialisation;

        private ICommandTransmitter _commandTransmitter;

        public InitialisationCommand(IZvtTransport transport)
        {
            _transport = transport;
            _initialisation = new InitialisationApdu();
            _commandTransmitter = new MagicResponseCommandTransmitter(_transport);
            _commandTransmitter.ResponseReceived += new Action<IZvtApdu>(_commandTransmitter_ResponseReceived);
        }

        private void _commandTransmitter_ResponseReceived(IZvtApdu responseApdu)
        {

        }

     
        public void Execute()
        {
            _transport.OpenConnection();
            ApduCollection responses = _commandTransmitter.TransmitAPDU(_initialisation);
            _transport.CloseConnection();
        }

    }
}
