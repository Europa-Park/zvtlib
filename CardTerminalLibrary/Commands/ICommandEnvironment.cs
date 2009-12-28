using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
        IInitialisationCommand CreateInitialisationCommand(XmlElement settings);
        IPaymentCommand CreatePaymentCommand(XmlElement settings);
        IReversalCommand CreateReversalCommand(XmlElement settings);
        IReportCommand CreateReportCommand(XmlElement settings);
        IEndOfDayCommand CreateEndOfDayCommand(XmlElement settings);
        #endregion



    }
}
