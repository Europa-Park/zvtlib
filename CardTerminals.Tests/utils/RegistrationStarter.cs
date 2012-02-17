using System.Linq;
using System.IO;
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
using Wiffzack.Services.Utils;
namespace Wiffzack.Devices.CardTerminals.Tests

{
	/// <summary>
	/// Registration starter. 
	/// The registration starter awaits a XML file that contains the configuration for transport and registration. 
	/// It executes the command and saves the resulting XML file as /tmp/result.xml.
	/// </summary>
	public class RegistrationStarter
	{
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
                <RemoteIP>192.168.2.3</RemoteIP>
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
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		static void Main(string[] args){
			 LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));
			//create XML file with result message
			XmlDocument resultXML = new XmlDocument();
			XmlElement rootNode=resultXML.CreateElement("Result");
			resultXML.AppendChild(rootNode);
			//check if the first argument is a file
			if(args.Length!=1 || !File.Exists(args[0])){
				LogManager.Global.GetLogger("Wiffzack").Info("Please pass a XML configuration file as first argument!");
				XmlHelper.WriteBool(rootNode, "Success", false);
            	XmlHelper.WriteInt(rootNode, "ProtocolSpecificErrorCode", -1);
            	XmlHelper.WriteString(rootNode, "ProtocolSpecificErrorDescription", "Please pass a XML configuration file as first argument!");
				//save file in /tmp/result.xml
				resultXML.Save("/tmp/result.xml");
				return;
			}
			
			//load the XML file
			XmlDocument config = new XmlDocument();
			try {
				config.Load(args[0]);
 			}
			//if any exception occur, the XML file could not be read and thus the program stops
 			catch {
				LogManager.Global.GetLogger("Wiffzack").Info("Your XML was probably bad...");
				XmlHelper.WriteBool(rootNode, "Success", false);
            	XmlHelper.WriteInt(rootNode, "ProtocolSpecificErrorCode", -2);
            	XmlHelper.WriteString(rootNode, "ProtocolSpecificErrorDescription", "Your XML was probably bad.");
				//save file in /tmp/result.xml
				resultXML.Save("/tmp/result.xml");
				return;
 			}
			//debug message --> remove later
			LogManager.Global.GetLogger("Wiffzack").Info("XML file loaded");
			//initialise environment with the configuration file and execute command
     		ICommandEnvironment environment = new ZVTCommandEnvironment(config.DocumentElement);
			environment.StatusReceived += new IntermediateStatusDelegate(environment_StatusReceived);
			CommandResult result = environment.CreateInitialisationCommand(null).Execute();
			
			
			//create XML file with result message
			result.SerializeToXml(resultXML.DocumentElement);
			//save file in /tmp/result.xml
			resultXML.Save("/tmp/result.xml");
			//debug message --> remove later
			LogManager.Global.GetLogger("Wiffzack").Info("XML file created");
		}
		  static void environment_StatusReceived(IntermediateStatus status)
        {
            Console.WriteLine(status);
        }
	}
}
