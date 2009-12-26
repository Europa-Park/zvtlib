using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU
{
    public class CompletionApduResponse : ApduResponse
    {

        public CompletionApduResponse()
            : this(0x06, 0x0F)
        {
        }

        public CompletionApduResponse(params byte[] rawData)
            :base(rawData)
        {
        }

    }
}
