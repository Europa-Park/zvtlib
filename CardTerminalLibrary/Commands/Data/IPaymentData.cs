using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Commands
{

    /// <summary>
    /// Implemented by a protocol specific class that uniquly identifies
    /// a single payment process.
    /// This data can be saved in a database and transmitted to the CardTerminalLibrary for e.g. refund
    /// processing.
    /// </summary>
    public interface IPaymentData : IData
    {
    }
}
