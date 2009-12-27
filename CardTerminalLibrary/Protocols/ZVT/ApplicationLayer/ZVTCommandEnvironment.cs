using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Commands;
using System.Xml;
using Wiffzack.Services.Utils;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer
{
    /// <summary>
    /// Implements the ZVT-specific command environment,
    /// which manages the transport layer and command creation
    /// </summary>
    public class ZVTCommandEnvironment:ICommandEnvironment
    {
        /// <summary>
        /// Raised when an intermediate status is received
        /// </summary>
        public event IntermediateStatusDelegate StatusReceived;

        /// <summary>
        /// Contains the configuration of the environment
        /// </summary>
        private XmlElement _environmentConfig;

        /// <summary>
        /// TransportLayer to use
        /// </summary>
        private IZvtTransport _transport;

        public XmlElement RegistrationCommandConfig
        {
            get
            {
                XmlElement config = (XmlElement)_environmentConfig.SelectSingleNode("RegistrationCommand");

                if (config == null)
                {
                    config = (XmlElement)_environmentConfig.AppendChild(_environmentConfig.OwnerDocument.CreateElement("RegistrationCommand"));
                }

                return config;
            }
        }


        public ZVTCommandEnvironment(XmlElement environmentConfig)
        {
            _environmentConfig = environmentConfig;

            string transport = XmlHelper.ReadString(environmentConfig, "Transport");

            if (transport == null)
                throw new ArgumentException("No transport layer specified");

            if (transport.Equals("serial", StringComparison.InvariantCultureIgnoreCase))
            {
                XmlElement serialConfig = (XmlElement)environmentConfig.SelectSingleNode("Serial");
                if(serialConfig == null)
                    throw new ArgumentException("No serial configuration specified");

                _transport = new RS232Transport(serialConfig);
            }
            else if (transport.Equals("network", StringComparison.InvariantCultureIgnoreCase))
            {
                XmlElement networkConfig = (XmlElement)environmentConfig.SelectSingleNode("Network");
                if (networkConfig == null)
                    throw new ArgumentException("No network configuration specified");

                _transport = new NetworkTransport(networkConfig);
            }
        }

        public void RaiseIntermediateStatusEvent(IntermediateStatus status)
        {
            if (StatusReceived != null)
                StatusReceived(status);
        }

        #region ICommandEnvironment Members

        public IInitialisationCommand CreateInitialisationCommand()
        {
            RegistrationCommand cmd = new RegistrationCommand(_transport, this);
            cmd.Status += RaiseIntermediateStatusEvent;
            return cmd;
        }

        public IPaymentCommand CreatePaymentCommand()
        {
            AuthorizationCommand cmd = new AuthorizationCommand(_transport, this);
            cmd.Status += RaiseIntermediateStatusEvent;
            return cmd;
        }

        public IReversalCommand CreateReversalCommand()
        {
            ReversalCommand cmd = new ReversalCommand(_transport, this);
            cmd.Status += RaiseIntermediateStatusEvent;
            return cmd;
        }

        public IReportCommand CreateReportCommand()
        {
            ReportCommand cmd =  new ReportCommand(_transport, this);
            cmd.Status += RaiseIntermediateStatusEvent;
            return cmd;
        }

        public IEndOfDayCommand CreateEndOfDayCommand()
        {
            EndOfDayCommand cmd = new EndOfDayCommand(_transport, this);
            cmd.Status += RaiseIntermediateStatusEvent;
            return cmd;
        }

        #endregion
    }
}
