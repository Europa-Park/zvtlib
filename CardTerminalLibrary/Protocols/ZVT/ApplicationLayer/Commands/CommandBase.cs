using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public abstract class CommandBase<T> where T: IZvtApdu
    {
           /// <summary>
        /// Transportlayer to use
        /// </summary>
        protected IZvtTransport _transport;

        /// <summary>
        /// Registration APDU
        /// </summary>
        protected T _apdu;

        private ICommandTransmitter _commandTransmitter;

        public CommandBase(IZvtTransport transport)
        {
            _transport = transport;
            _commandTransmitter = new MagicResponseCommandTransmitter(_transport);
            _commandTransmitter.ResponseReceived += new Action<IZvtApdu>(_commandTransmitter_ResponseReceived);
        }

        private void _commandTransmitter_ResponseReceived(IZvtApdu responseApdu)
        {
            ResponseReceived(responseApdu);
        }

        protected virtual void ResponseReceived(IZvtApdu responseApdu)
        {
        }
     
        public virtual void Execute()
        {
            _transport.OpenConnection();
            ApduCollection responses = _commandTransmitter.TransmitAPDU(_apdu);
            _transport.CloseConnection();
        }
    }
}
