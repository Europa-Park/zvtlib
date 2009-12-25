using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU
{
    public class SelftestApdu : ApduBase
    {
        protected override byte[]  ByteControlField
        {
            get { return new byte[] { 0x06, 0x79 }; }
        }

    }
}
