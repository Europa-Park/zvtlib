using System;
using System.IO;
using Wiffzack.Diagnostic.Log;
namespace Wiffzack.Devices.CardTerminals.Tests
{
	public class Starter
	{
		public static String result="tmp/result.xml";
		public static String logger="tmp/pagar.log";
		public static TextWriter getFileLoggerStream(){
			if(logger.ToUpper().Equals("-DEBUG")){
				return Console.Out;
			}
			TextWriter returns=null;
			try{
				returns=File.AppendText(logger);
			}catch(Exception e){
				returns=Console.Out;
				Console.Out.WriteLine("Can't Access log file");
				Console.Out.WriteLine(e.Message);
			}
			return returns;
		}
		private static void printTypes(){
			Console.WriteLine("<logfile> <resultfile> <type> <config.xml>");
			Console.WriteLine("Types:");
			Console.WriteLine("		reset:			Reset Terminal");
			Console.WriteLine("		eod:			Terminal End of Day");
			Console.WriteLine("		network:		Network Diagnosis");
			Console.WriteLine("		register:		Terminal Registration");
			Console.WriteLine("		report:			Terminal Report");
			Console.WriteLine("		reversal:		Payment Reversal");
			Console.WriteLine("		payment:		Card Payment");
			Console.WriteLine("		abort:			Abort Payment");
			Console.WriteLine("		repeat:			Repeat last Receipt");
			Console.WriteLine("		------------------------------		");
			Console.WriteLine("Use -debug as log parameter to print the log on the console.");
		}
		static void Main(string[] args){
			try{
				if(args.Length==0 | args[0].Equals("?")){
					LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));
					printTypes();
					return;
				}
				Starter.logger=args[0];
				Starter.result=args[1];
				String type=args[2];
				String[] newargs;
				if(args.Length-3<=0)
					newargs=null;
				else{
					newargs=new String[args.Length-3];
					for(int i=3;i<args.Length;i++){
						newargs[i-3]=args[i];
					}
				}
				switch(type){
					case "reset":
						ResetStarter.Main(newargs);
						break;
					case "eod":
						EndOfDayStarter.Main(newargs);
						break;
					case "network":
						NetworkDiagnosisStarter.Main(newargs);
						break;
					case "register":
						RegistrationStarter.Main(newargs);
						break;
					case "report":
						ReportStarter.Main(newargs);
						break;
					case "reversal":
						ReversalStarter.Main(newargs);
						break;
					case "payment":
						PaymentStarter.Main(newargs);
						break;
					case "abort":
						AbortStarter.Main(newargs);
						break;
					case "repeat":
						RepeatReceiptStarter.Main(newargs);
						break;
					default:
						printTypes();
						return;
				}
			}catch(IndexOutOfRangeException){
				LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));
				printTypes();
				return;
			}catch(Exception e){
				LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Starter.getFileLoggerStream()));
				Logger _log= LogManager.Global.GetLogger("Wiffzack");
				_log.Info("Start up error");
				_log.Info(e.Message);
				_log.Info(e.StackTrace);
				_log.Info(e.GetType().ToString());
			}
			
		}
	}
}

