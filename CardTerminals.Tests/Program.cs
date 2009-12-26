using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.TransportLayer;
using Wiffzack.Diagnostic.Log;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer;
using System.IO.Ports;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Commands;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;
using Wiffzack.Devices.CardTerminals.Commands;

namespace Wiffzack.Devices.CardTerminals.Tests
{
    class Program
    {
        public static string _configuration = @"
            <Config>
              <Transport>Network</Transport>
              <!-- <Transport>Network</Transport>-->
              <Serial>
                <Port>COM7</Port>
                <BaudRate>9600</BaudRate>
                <StopBits>One</StopBits>
              </Serial>

              <Network>
                <RemoteIP>192.168.0.154</RemoteIP>
                <RemotePort>5577</RemotePort>
              </Network>

              <RegistrationCommand>
                <ECRPrintsAdministrationReceipts>True</ECRPrintsAdministrationReceipts>
                <ECRPrintsPaymentReceipt>True</ECRPrintsPaymentReceipt>
                <PTDisableAmountInput>True</PTDisableAmountInput>
                <PTDisableAdministrationFunctions>True</PTDisableAdministrationFunctions>
              </RegistrationCommand>
            </Config>
            ";

        static void Main(string[] args)
        {
            LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));

            XmlDocument configuration = new XmlDocument();
            configuration.LoadXml(_configuration);

            ICommandEnvironment environment = new ZVTCommandEnvironment(configuration.DocumentElement);
            ClassifyCommandResult(environment.CreateInitialisationCommand().Execute());
            ClassifyCommandResult(environment.CreatePaymentCommand().Execute(90));

            
            Console.ReadLine();
        }

        static void ClassifyCommandResult(CommandResult cmdResult)
        {
            if (cmdResult.Success == false)
                throw new ArgumentException("Command not successful");
        }
    }
}
