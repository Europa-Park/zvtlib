using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;
using Wiffzack.Devices.CardTerminals.Commands;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class AuthorizationCommand : IAuthorizationCommand
    {
        public event IntermediateStatusDelegate Status;

        /// <summary>
        /// Transportlayer to use
        /// </summary>
        private IZvtTransport _transport;

        /// <summary>
        /// Authorization APDU
        /// </summary>
        private AuthorizationApdu _apdu;

        private ICommandTransmitter _commandTransmitter;

        public AuthorizationCommand(IZvtTransport transport)
        {
            _transport = transport;
            _apdu = new AuthorizationApdu();
            _commandTransmitter = new MagicResponseCommandTransmitter(_transport);
            _commandTransmitter.ResponseReceived += new Action<IZvtApdu>(_commandTransmitter_ResponseReceived);
        }

        private void _commandTransmitter_ResponseReceived(IZvtApdu responseApdu)
        {

        }

     
        public AuthorizationResult Execute(Int64 centAmount)
        {
            _apdu.SetCentAmount(centAmount);
            _transport.OpenConnection();
            ApduCollection responses = _commandTransmitter.TransmitAPDU(_apdu);
            _transport.CloseConnection();

            //Contains the result (sucess or failure) and much information about the transaction
            StatusInformationApdu statusInformation = responses.FindFirstApduOfType<StatusInformationApdu>();

            //Completion is only sent if everathing worked fine
            CompletionApduResponse completion = responses.FindFirstApduOfType<CompletionApduResponse>();

            //Abort is only sent if something went wrong
            AbortApduResponse abort = responses.FindFirstApduOfType<AbortApduResponse>();


            AuthorizationResult result = new AuthorizationResult();
            
            return null;

        }        
    }
}
