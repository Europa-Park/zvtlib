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
		private static void printTypes(Logger _log){
			_log.Info("Types:");
			_log.Info("reset		Reset Terminal");
			_log.Info("eof			Terminal End of Day");
			_log.Info("network		Network Diagnosis");
			_log.Info("register		Terminal Registration");
			_log.Info("report		Terminal Report");
			_log.Info("reversal		Payment Reversal");
			_log.Info("payment		Card Payment");
		}
		static void Main(string[] args){
			try{
				if(args.Length==0 | args[0].Equals("?")){
					LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));
					Logger _log= LogManager.Global.GetLogger("Wiffzack");
					_log.Info("<logfile> <resultfile> <type>");
					printTypes(_log);
					return;
				}
				Starter.logger=args[0];
				Starter.result=args[1];
				String type=args[2];
				String[] newargs=new String[args.Length-3];
				for(int i=3;i<args.Length;i++){
					newargs[i-3]=args[i];
				}
				switch(type){
					case "reset":
						ResetStarter.Main(newargs);
						break;
					case "eof":
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
				}
			}catch(IndexOutOfRangeException ae){
				LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));
				Logger _log= LogManager.Global.GetLogger("Wiffzack");
				_log.Info("<logfile> <resultfile> <type>");
				printTypes(_log);
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

