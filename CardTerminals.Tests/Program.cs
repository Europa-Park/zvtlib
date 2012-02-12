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
              <!--<TransportSettings>
                <Port>/dev/ttyUSB0</Port>
                <BaudRate>9600</BaudRate>
				<Parity>None</Parity>
                <StopBits>One</StopBits>
              </TransportSettings>-->

              <TransportSettings>
                <RemoteIP>192.168.1.7</RemoteIP>
                <RemotePort>51234</RemotePort>
              </TransportSettings>

              <RegistrationCommand>
                <ECRPrintsAdministrationReceipts>False</ECRPrintsAdministrationReceipts>
                <ECRPrintsPaymentReceipt>False</ECRPrintsPaymentReceipt>
                <PTDisableAmountInput>True</PTDisableAmountInput>
                <PTDisableAdministrationFunctions>False</PTDisableAdministrationFunctions>
              </RegistrationCommand>
            </Config>
            ";

        static void Main(string[] args)
        {
			  
			byte test=5;
			byte testphase2=(byte)(1 << 0);
			Console.WriteLine("test:"+test);
			Console.WriteLine("testphase2:"+testphase2);
			Console.WriteLine("Solution:"+(test & testphase2));
			byte testphase3=test &= (byte)~testphase2;
			Console.WriteLine("testphase3:"+testphase3);
//            LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));
//
//            XmlDocument configuration = new XmlDocument();
//            configuration.LoadXml(_configuration);
//
//            XmlDocument paymentSettings = new XmlDocument();
//            paymentSettings.LoadXml(_paymentSettings);
//
//            ICommandEnvironment environment = new ZVTCommandEnvironment(configuration.DocumentElement);
//            environment.StatusReceived += new IntermediateStatusDelegate(environment_StatusReceived);
//            ClassifyCommandResult(environment.CreateInitialisationCommand(null).Execute());
//
//            PaymentResult result = environment.CreatePaymentCommand(paymentSettings.DocumentElement).Execute();
//            ClassifyCommandResult(result);
//            XmlDocument authorisationIdentifier = new XmlDocument();
//            authorisationIdentifier.AppendChild(authorisationIdentifier.CreateElement("Data"));
//            result.Data.WriteXml(authorisationIdentifier.DocumentElement);
//            
//            ClassifyCommandResult(environment.CreateReversalCommand(authorisationIdentifier.DocumentElement).Execute());
//
//            //ClassifyCommandResult(environment.CreateReportCommand(null).Execute());
//
//            
//            Console.ReadLine();
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
