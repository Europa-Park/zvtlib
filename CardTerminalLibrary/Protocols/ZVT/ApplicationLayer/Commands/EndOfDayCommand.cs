using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Commands;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class EndOfDayCommand : CommandBase<EndOfDayApdu, CommandResult>, IEndOfDayCommand
    {
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
        
    }
}
