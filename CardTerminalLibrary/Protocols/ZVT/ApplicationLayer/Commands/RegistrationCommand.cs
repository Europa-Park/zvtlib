using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class RegistrationCommand
    {
        /// <summary>
        /// Transportlayer to use
        /// </summary>
        private IZvtTransport _transport;

        /// <summary>
        /// Registration APDU
        /// </summary>
        private RegistrationApdu _registration;


        public RegistrationCommand(IZvtTransport transport)
        {
            _transport = transport;
            _registration = new RegistrationApdu();
        }


        public void Execute()
        {
            _registration.ConfigByte.ECRPrintsAdministrationReceipts = false;
            _registration.ConfigByte.ECRPrintsPaymentReceipt = true;
            _registration.ConfigByte.ECRPrintType = true;
            _registration.ConfigByte.PTDisableAmountInput = true;
            _registration.ConfigByte.PTDisableAdministrationFunctions = true;
            _registration.ConfigByte.SendIntermediateStatusInformation = true;
            _registration.EnableServiceByte = false;
            _registration.ServiceByte.DisplayAuthorisationInCapitals = true;
            _registration.ServiceByte.NotAssignPTServiceMenuToFunctionKey = true;

            _transport.OpenConnection();
            MagicResponseCommandTransmitter commandTransmitter = new MagicResponseCommandTransmitter(_transport);
            commandTransmitter.ResponseReceived += new Action<IZvtApdu>(commandTransmitter_ResponseReceived);

            IZvtApdu[] responses = commandTransmitter.TransmitAPDU(_registration);
            _transport.CloseConnection();
        }

        private void commandTransmitter_ResponseReceived(IZvtApdu responseApdu)
        {
            
        }

    }
}
