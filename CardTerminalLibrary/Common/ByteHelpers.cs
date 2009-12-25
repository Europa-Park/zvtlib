using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Common
{
    public static class ByteHelpers
    {
        public static string ByteToString(params byte[] data)
        {
            StringBuilder sBuilder = new StringBuilder();

            foreach (byte b in data)
                sBuilder.AppendFormat("{0:X2} ", b);

            return sBuilder.ToString();
        }
    }
}
