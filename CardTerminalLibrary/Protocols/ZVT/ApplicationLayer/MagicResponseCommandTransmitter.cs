using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.ApduHandlerDefinitions;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer
{
    /// <summary>
    /// Transmits the given APDU and waits for a specified "magic apdu" ;-)
    /// </summary>
    public class MagicResponseCommandTransmitter : ICommandTransmitter
    {
        public delegate bool PacketReceivedDelegate(IZvtApdu transmittedApdu, IZvtApdu responseApdu);


        /// <summary>
        /// Checks if the current packet is the completion packet
        /// </summary>
        public event PacketReceivedDelegate IsCompletionPacket;

        /// <summary>
        /// Transport implementation to use
        /// </summary>
        private IZvtTransport _transport;

        private List<IApduHandler> _apduHandlers = new List<IApduHandler>();

        public MagicResponseCommandTransmitter(IZvtTransport transport)
        {
            _transport = transport;

            _apduHandlers.Add(new AckSenderApduHandler(_transport));
        }

        /// <summary>
        /// Finds the handlers of the specified response apdu
        /// </summary>
        /// <param name="apdu"></param>
        /// <returns></returns>
        private void CallResponseApduHandlers(IZvtApdu requestApdu, IZvtApdu responseApdu)
        {
            foreach (IApduHandler handler in _apduHandlers)
            {
                if (handler.IsCompatibleHandler(responseApdu))
                    handler.Process(requestApdu, responseApdu);
            }
        }

        #region ICommandTransmitter Members

        public event Action<IZvtApdu> ResponseReceived;

        public IZvtApdu[] TransmitAPDU(IZvtApdu apdu)
        {

            _transport.Transmit(_transport.CreateTpdu(apdu));

            List<IZvtApdu> responses = new List<IZvtApdu>();

            while (true)
            {
                IZvtApdu responseApdu = ApduResponse.Create(_transport.ReceiveResponsePacket());

                if (responseApdu == null)
                    throw new ArgumentException("Could not retrieve response");

                if (this.ResponseReceived != null)
                    ResponseReceived(responseApdu);

                responses.Add(responseApdu);

                CallResponseApduHandlers(apdu, responseApdu);

                if (IsCompletionPacket == null && InternalIsCompletionPacket(apdu, responseApdu))
                {
                    break;
                }
                else if(IsCompletionPacket != null && IsCompletionPacket(apdu, responseApdu))
                    break;
                
            }

            return responses.ToArray();
        }

        private bool InternalIsCompletionPacket(IZvtApdu transmittedApdu, IZvtApdu responseApdu)
        {
            if (transmittedApdu.SendsCompletionPacket)
            {
                byte[] apduData = responseApdu.GetRawApduData();

                if (apduData[0] == 0x80 && apduData[1] == 0x00)
                {
                    _transport.MasterMode = false;
                }
                if (apduData[0] == 0x06 && (apduData[1] == 0x0F || apduData[1] == 0x0E))
                {
                    _transport.MasterMode = true;
                    return true;
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
