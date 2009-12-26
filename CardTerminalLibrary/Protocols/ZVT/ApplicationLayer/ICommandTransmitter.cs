using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer
{
    /// <summary>
    /// Implemented by all Command transmitters,
    /// a command transmitter not only sends the APDU but also receives the desired amount of 
    /// Responses
    /// </summary>
    public interface ICommandTransmitter
    {
        event Action<IZvtApdu> ResponseReceived;

        ApduCollection TransmitAPDU(IZvtApdu apdu);
    }
}
