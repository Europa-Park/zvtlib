using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Raised while executing the command to update the status on the requesting Station
        /// </summary>
        event IntermediateStatusDelegate Status;
    }
}
