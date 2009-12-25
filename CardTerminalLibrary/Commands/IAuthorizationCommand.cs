using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Commands
{
    /// <summary>
    /// Command that performs the booking process
    /// 
    /// </summary>
    public interface IAuthorizationCommand : ICommand
    {     
   
        /// <summary>
        /// Initiates the payment process
        /// </summary>
        /// <param name="centAmount">Amount to book in EuroCents</param>
        AuthorizationResult Execute(Int64 centAmount);
    }
}
