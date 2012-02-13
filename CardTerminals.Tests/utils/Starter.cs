using System;
using System.Xml;

namespace Wiffzack.Devices.CardTerminals.Tests
{
	public interface Starter
	{
		bool checkArgs(string[] args);
		XmlDocument generateXML(string[] args);
		void execute();
	}
}

