using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Commands;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public abstract class CommandBase<T, U> where T: IZvtApdu where U: class
    {
           /// <summary>
        /// Transportlayer to use
        /// </summary>
        protected IZvtTransport _transport;

        /// <summary>
        /// Registration APDU
        /// </summary>
        protected T _apdu;

        protected ICommandTransmitter _commandTransmitter;

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
     
        public virtual U Execute()
        {
            _transport.OpenConnection();
            ApduCollection responses = _commandTransmitter.TransmitAPDU(_apdu);
            _transport.CloseConnection();

            return null;
        }

        public static bool CheckForAbortApdu(CommandResult cmdResult, ApduCollection collection)
        {
            AbortApduResponse abort = collection.FindFirstApduOfType<AbortApduResponse>();

            if (abort != null)
            {
                cmdResult.Success = false;
                cmdResult.ProtocolSpecificErrorCode = (byte)abort.ResultCode;
                cmdResult.ProtocolSpecificErrorDescription = abort.ResultCode.ToString();
                return true;
            }
            return false;
        }
    }
}
