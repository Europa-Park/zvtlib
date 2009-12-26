using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Commands;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;
using Wiffzack.Diagnostic.Log;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class RegistrationCommand:IInitialisationCommand
    {
        public event IntermediateStatusDelegate Status;


        /// <summary>
        /// Transportlayer to use
        /// </summary>
        private IZvtTransport _transport;

        /// <summary>
        /// Registration APDU
        /// </summary>
        private RegistrationApdu _registration;

        private Logger _log = LogManager.Global.GetLogger("Wiffzack");
        public RegistrationCommand(IZvtTransport transport)
        {
            _transport = transport;
            _registration = new RegistrationApdu();
        }


        public InitialisationResult Execute()
        {
            InitialisationResult result = new InitialisationResult();
            result.Success = true;

            _registration.ConfigByte.ECRPrintsAdministrationReceipts = true;
            _registration.ConfigByte.ECRPrintsPaymentReceipt = true;
            _registration.ConfigByte.ECRPrintType = true;
            _registration.ConfigByte.PTDisableAmountInput = false;
            _registration.ConfigByte.PTDisableAdministrationFunctions = true;
            _registration.ConfigByte.SendIntermediateStatusInformation = true;
            _registration.EnableServiceByte = false;
            _registration.ServiceByte.DisplayAuthorisationInCapitals = true;
            _registration.ServiceByte.NotAssignPTServiceMenuToFunctionKey = true;

            try
            {
                _transport.OpenConnection();
                MagicResponseCommandTransmitter commandTransmitter = new MagicResponseCommandTransmitter(_transport);
                commandTransmitter.ResponseReceived += new Action<IZvtApdu>(commandTransmitter_ResponseReceived);

                ApduCollection responses = commandTransmitter.TransmitAPDU(_registration);
                CompletionApduResponse completionApdu = responses.FindFirstApduOfType<CompletionApduResponse>();

                if (completionApdu == null)
                    throw new NotSupportedException("Did not receive Completion from RegistrationApdu");

                //if statusbyte is not supplied (== null) no extra action need to be performed
                CompletionStatusByteParameter statusByte = completionApdu.FindParameter<CompletionStatusByteParameter>(CompletionApduResponse.ParameterTypeEnum.StatusByte);
                
                
                if (statusByte != null && statusByte.InitialisationNecessary)
                {
                    _log.Info("PT needs initialisation");

                    InitialisationApdu init = new InitialisationApdu();
                    ApduCollection apdus = commandTransmitter.TransmitAPDU(init);

                    AbortApduResponse abort = apdus.FindFirstApduOfType<AbortApduResponse>();

                    if (abort != null)
                    {
                        _log.Info("Initialisation failed with '{0}({1})'", abort.ResultCode, (byte)abort.ResultCode);

                        result.Success = false;
                        result.ProtocolSpecificErrorCode = (byte)abort.ResultCode;
                        result.ProtocolSpecificErrorDescription = abort.ResultCode.ToString();
                    }
                }

                if (statusByte != null && statusByte.DiagnosisNecessary && result.Success)
                {
                    _log.Info("PT needs diagnosis");

                    DiagnosisApdu diag = new DiagnosisApdu();
                    ApduCollection apdus = commandTransmitter.TransmitAPDU(diag);

                    AbortApduResponse abort = apdus.FindFirstApduOfType<AbortApduResponse>();

                    if (abort != null)
                    {
                        _log.Fatal("Diagnosis failed with '{0}({1})'", abort.ResultCode, (byte)abort.ResultCode);

                        result.Success = false;
                        result.ProtocolSpecificErrorCode = (byte)abort.ResultCode;
                        result.ProtocolSpecificErrorDescription = abort.ResultCode.ToString();
                    }
                }

            }
            finally
            {
                _transport.CloseConnection();
            }

            return result;


        }

        private void commandTransmitter_ResponseReceived(IZvtApdu responseApdu)
        {
            
        }


      
    }
}
