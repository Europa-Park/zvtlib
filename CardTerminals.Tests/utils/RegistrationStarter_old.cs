using System;
using System.Xml;
using System.Linq;

namespace Wiffzack.Devices.CardTerminals.Tests
{
	public class RegistrationStarter_old : Starter
	{
		bool configFlag = false;
		XmlDocument config;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Wiffzack.Devices.CardTerminals.Tests.RegistrationStarter"/> class.
		/// </summary>
		public RegistrationStarter_old ()
		{
			config = new XmlDocument();
		}
		
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		static void Main(string[] args){
			RegistrationStarter_old rs = new RegistrationStarter_old();
			
			if(rs.checkArgs(args)){
				//arguments passed the test
			}
			if(!rs.configFlag){
				rs.generateXML(args);
			}
		}
		
		/// <summary>
		/// Checks the arguments.
		/// </summary>
		/// <returns>
		/// The arguments.
		/// </returns>
		public bool checkArgs(string[] args){
			if(args.Contains("--help")){
				printHelp();
				return true;
			}
			
			//check if config switch is set
			if (args.Contains("-c")) {
				configFlag = true;
				//use given XML file
    			int indexOfFile = Array.IndexOf(args,"-c")+1;
				string xmlFile = args[indexOfFile];
				
				//read in xml file with xmltextreader and load to xmldocument
				XmlTextReader reader = new XmlTextReader(xmlFile);
				config.Load(reader);
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Generates the XML document.
		/// </summary>
		/// <returns>
		/// The XML document.
		/// </returns>
		public XmlDocument generateXML(string[] args){
			return null;
		}
		
		/// <summary>
		/// Execute the command.
		/// </summary>
		public void execute(){
			
		}
		
		/// <summary>
		/// Prints the help.
		/// </summary>
		public void printHelp(){
			Console.WriteLine("Help for starting Registration:");
			Console.WriteLine("-c <file>  ... use given configuration file");
			Console.WriteLine("Transport settings:");
			Console.WriteLine("-t <type>  ... use the defined transport type (Serial or Network)");
			Console.WriteLine("-sp <port> ... define serial port");
			Console.WriteLine("-br <int>  ... define BaudRate");
			Console.WriteLine("-pr <val>  ... define Parity (None, Odd, Even)");
			Console.WriteLine("-sb <val>  ... define StopBits (One, Two)");
			Console.WriteLine("-db <int>  ... define DataBits (5,6,7,8)");
			Console.WriteLine("-rb <int>  ... define ReadBuffer");
			Console.WriteLine("-wb <int>  ... define WriterBuffer");
			Console.WriteLine("-ip <val>  ... define remote IP (only in Network transport)");
			Console.WriteLine("-np <port> ... define network port");
			Console.WriteLine("Registration commands:");
			Console.WriteLine("-pa <val>  ... activate/deactivate printing of administration receipts on ECR (True, False)");
			Console.WriteLine("-pp <val>  ... activate/deactivate printing of payment receipts on ECR (True, False)");
			Console.WriteLine("-da <val>  ... disable amount input on PT (True, False)");
			Console.WriteLine("-df <val>  ... disable administration functions on PT (True, False)");
		}
	}
}

