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
        public static string _paymentSettings = @"
            <Payment>
                <Amount>192</Amount>
            </Payment>
";
        public static string _configuration = @"
            <Config>
              <!--<Transport>Serial</Transport>-->
              <Transport>Network</Transport>
              <Serial>
                <Port>COM20</Port>
                <BaudRate>9600</BaudRate>
                <StopBits>One</StopBits>
              </Serial>

              <Network>
                <RemoteIP>192.168.0.157</RemoteIP>
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

            XmlDocument paymentSettings = new XmlDocument();
            paymentSettings.LoadXml(_paymentSettings);

            ICommandEnvironment environment = new ZVTCommandEnvironment(configuration.DocumentElement);
            environment.StatusReceived += new IntermediateStatusDelegate(environment_StatusReceived);
            ClassifyCommandResult(environment.CreateInitialisationCommand(null).Execute());

            PaymentResult result = environment.CreatePaymentCommand(paymentSettings.DocumentElement).Execute();
            ClassifyCommandResult(result);
            XmlDocument authorisationIdentifier = new XmlDocument();
            authorisationIdentifier.AppendChild(authorisationIdentifier.CreateElement("Data"));
            result.Data.WriteXml(authorisationIdentifier.DocumentElement);
            
            ClassifyCommandResult(environment.CreateReversalCommand(authorisationIdentifier.DocumentElement).Execute());

            //ClassifyCommandResult(environment.CreateReportCommand(null).Execute());

            
            Console.ReadLine();
        }

        static void environment_StatusReceived(IntermediateStatus status)
        {
            Console.WriteLine(status);
        }

        static void ClassifyCommandResult(CommandResult cmdResult)
        {
            if (cmdResult.Success == false)
                throw new ArgumentException("Command not successful");
        }
    }
}
