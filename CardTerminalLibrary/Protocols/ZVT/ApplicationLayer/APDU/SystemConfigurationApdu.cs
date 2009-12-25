using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU
{
    public class SystemConfigurationApdu : ApduBase
    {
        protected override byte[] ByteControlField
        {
            get { return new byte[] { 0x06, 0x1A }; }
        }
    }
}
