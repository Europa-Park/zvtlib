using System;
using System.Xml;
using System.Linq;
using System.IO;

namespace Wiffzack.Devices.CardTerminals.Tests
{
	/// <summary>
	/// Registration starter. 
	/// The registration starter awaits a XML file that contains the configuration for transport and registration.
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
 			catch {	//if any exception occur, the XML file could not be read and thus the program stops
				Console.WriteLine("Your XML was probably bad...");
				return;
 			}
			
			Console.WriteLine("XML file loaded");			
		}
	}
}
