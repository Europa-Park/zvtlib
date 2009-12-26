using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Commands;
using System.Xml;
using Wiffzack.Diagnostic.Log;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class EndOfDayCommand : CommandBase<EndOfDayApdu, CommandResult>, IEndOfDayCommand
    {
        private Logger _log = LogManager.Global.GetLogger("Wiffzack");

        #region ICommand Members

        public event IntermediateStatusDelegate Status;

        #endregion
        
        public EndOfDayCommand(IZvtTransport transport)
            : base(transport)
        {
            _apdu = new EndOfDayApdu();
        }

        public override CommandResult Execute()
        {
            try
            {
                CommandResult result = new CommandResult();
                result.Success = true;

                _transport.OpenConnection();

                ApduCollection apdus = _commandTransmitter.TransmitAPDU(_apdu);
                CheckForAbortApdu(result, apdus);
                return result;
            }
            finally
            {
                _transport.CloseConnection();
            }
        }

        public void ReadSettings(XmlElement settings)
        {
            _log.Warning("ReadSettings for EndOfDayCommand, but no settings should be read");
        }

    }
}
