using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Commands;
using Wiffzack.Diagnostic.Log;
using System.Xml;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class ReversalCommand : CommandBase<ReversalApdu, CommandResult>, IReversalCommand
    {
        
        private Logger _log = LogManager.Global.GetLogger("Wiffzack");


        #region ICommand Members

        public event IntermediateStatusDelegate Status;

        

        public void ReadSettings(XmlElement settings)
        {
            _log.Warning("ReadSettings for Reversal, but no settings should be read");
        }

        #endregion

        public int ReceiptNr
        {
            get { return _apdu.ReceiptNr; }
            set { _apdu.ReceiptNr = value; }
        }

        public ReversalCommand(IZvtTransport transport)
            : base(transport)
        {
            _apdu = new ReversalApdu();
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
