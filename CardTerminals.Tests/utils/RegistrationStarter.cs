using System;
using System.Xml;
using System.Linq;
using System.IO;

namespace Wiffzack.Devices.CardTerminals.Tests
{
	/// <summary>
	/// Registration starter. 
	/// The registration starter awaits a XML file that contains the configuration for transport and registration. 
	/// It executes the command and saves the resulting XML file as /tmp/result.xml.
	/// </summary>
	public class RegistrationStarter
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		static void Main(string[] args){
			//check if the first argument is a file
			if(args.Length!=1 || !File.Exists(args[0])){
				Console.WriteLine("Please pass a XML configuration file as first argument!");
				return;
			}
			
			//load the XML file
			XmlDocument config = new XmlDocument();
			try {
				config.Load(args[0]);
 			}
			//if any exception occur, the XML file could not be read and thus the program stops
 			catch {
				Console.WriteLine("Your XML was probably bad...");
				return;
 			}
			//debug message --> remove later
			Console.WriteLine("XML file loaded");
			
			//initialise environment with the configuration file and execute command
//			ICommandEnvironment environment = new ZVTCommandEnvironment(configuration.DocumentElement);
//			environment.StatusReceived += new IntermediateStatusDelegate(environment_StatusReceived);
//			CommandResult result = environment.CreateInitialisationCommand(null).Execute();
			
			//create XML file with result message
			XmlDocument resultXML = new XmlDocument();
			resultXML.AppendChild(resultXML.CreateElement("Result"));
//			result.SerializeToXml(resultXML.DocumentElement);
			//save file in /tmp/result.xml
			resultXML.Save("/tmp/result.xml");
			//debug message --> remove later
			Console.WriteLine("XML file created");
		}
	}
}
