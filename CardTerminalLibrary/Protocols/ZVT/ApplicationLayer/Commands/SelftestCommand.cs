using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands
{
    public class SelftestCommand : CommandBase<SelftestApdu>
    {

        public SelftestCommand(IZvtTransport transport)
            :base(transport)
        {
            _apdu = new SelftestApdu();
        }

    }
}
