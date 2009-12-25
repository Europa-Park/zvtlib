using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer
{

    /// <summary>
    /// Transmits the given APDU and receives the specified amount of Responses
    /// </summary>
    public class FixedAmountCommandTransmitter:ICommandTransmitter 
    {
        public event Action<IZvtApdu> ResponseReceived;

        /// <summary>
        /// The amount of responses to receive
        /// </summary>
        private int _amount = 1;

        /// <summary>
        /// Transport implementation to use
        /// </summary>
        private IZvtTransport _transport;

        public FixedAmountCommandTransmitter(IZvtTransport transport, int amount)
        {
            _amount = amount;
            _transport = transport;
        }

        #region ICommandTransmitter Members

        public IZvtApdu[] TransmitAPDU(IZvtApdu apdu)
        {
            _transport.Transmit(_transport.CreateTpdu(apdu));

            List<IZvtApdu> responses = new List<IZvtApdu>();
            for (int i = 0; i < _amount; i++)
            {
                ApduResponse response = new ApduResponse(_transport.ReceiveResponsePacket());
                if (ResponseReceived != null)
                    ResponseReceived(response);
                responses.Add(response);
            }

            return responses.ToArray();
        }

        #endregion
    }
}
