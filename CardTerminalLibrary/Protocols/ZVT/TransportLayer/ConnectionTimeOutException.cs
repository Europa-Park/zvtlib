using System;

namespace Wiffzack.Devices.CardTerminals
{
	public class ConnectionTimeOutException: SystemException
	{
		public ConnectionTimeOutException () : base("Connection Time Out Occoured")
		{
		}
	}
}

