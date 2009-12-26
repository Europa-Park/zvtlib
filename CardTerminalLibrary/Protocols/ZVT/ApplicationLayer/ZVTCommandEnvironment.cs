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



        #region ICommandEnvironment Members

        public IInitialisationCommand CreateInitialisationCommand()
        {
            return new RegistrationCommand(_transport, this);
        }

        public IPaymentCommand CreatePaymentCommand()
        {
            return new AuthorizationCommand(_transport);
        }

        public IReversalCommand CreateReversalCommand()
        {
            return new ReversalCommand(_transport);
        }

        public IReportCommand CreateReportCommand()
        {
            return new ReportCommand(_transport);
        }

        public IEndOfDayCommand CreateEndOfDayCommand()
        {
            return new EndOfDayCommand(_transport);
        }

        #endregion
    }
}
