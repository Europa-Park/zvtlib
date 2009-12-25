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

namespace Wiffzack.Devices.CardTerminals.Tests
{
    class Program
    {
        public static string _serialConfiguration = @"
            <Config>
                <PortName>COM7</PortName>
                <BaudRate>9600</BaudRate>
                <StopBits>One</StopBits>
            </Config>";

        public static string _networkConfiguration = @"
            <Config>
                <RemoteIP>192.168.0.154</RemoteIP>
                <RemotePort>5577</RemotePort>
            </Config>
";

        static void Main(string[] args)
        {
            LogManager.Global = new LogManager(true, new TextLogger(null, LogLevel.Everything, "Wiffzack", Console.Out));

            ParameterEncodingHelper.ReadLLVarData(new byte[] { 0xF1, 0xF3, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, 0);

            
            IZvtTransport transportLayer = null;
            XmlDocument transportConfig = new XmlDocument();
            //transportConfig.LoadXml(_serialConfiguration);
            transportConfig.LoadXml(_networkConfiguration);

                
            //transportLayer = new RS232Transport(transportConfig.DocumentElement);
            transportLayer = new NetworkTransport(transportConfig.DocumentElement);

            RegistrationCommand cmd = new RegistrationCommand(transportLayer);
            cmd.Execute();

            //new InitialisationCommand(transportLayer).Execute();
            //new NetworkDiagnosisCommand(transportLayer).Execute();
            //new AuthorizationCommand(transportLayer).Execute(120);
            //new SelftestCommand(transportLayer).Execute();
            //new SystemConfigurationCommand(transportLayer).Execute();

            //BCDNumberParameter param = new BCDNumberParameter();
            //param.SetNumber((Int64)120, 6);
            
            Console.ReadLine();
        }
    }
}
