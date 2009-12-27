using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Commands
{
    /// <summary>
    /// Implemented by a protocol specific environment which manages 
    /// all objects (transport,...) need by the commands
    /// </summary>
    public interface ICommandEnvironment
    {
        event IntermediateStatusDelegate StatusReceived;

        #region Command factory
        IInitialisationCommand CreateInitialisationCommand();
        IPaymentCommand CreatePaymentCommand();
        IReversalCommand CreateReversalCommand();
        IReportCommand CreateReportCommand();
        IEndOfDayCommand CreateEndOfDayCommand();
        #endregion



    }
}
