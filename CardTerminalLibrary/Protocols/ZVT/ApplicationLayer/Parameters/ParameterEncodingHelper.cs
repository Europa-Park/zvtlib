using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters
{
    public static class ParameterEncodingHelper
    {
        /// <summary>
        /// Extracts the length out of bytes with the following format
        /// 
        /// [Fx1] [Fx2] [Fx3]...[FxlengthBytes]
        /// 
        /// ATTENTION: LengthBytes is not added by this generic function
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="lengthBytes"></param>
        /// <returns></returns>
        public static int GetLVarLength(byte[] buffer, int offset, int lengthBytes)
        {
            int length = 0;

            for (int i = 0; i < lengthBytes; i++)
            {
                byte currentByte = buffer[offset + lengthBytes - i - 1];

                //Consistency chek
                if ((currentByte & 0xF0) != 0xF0)
                    throw new ArgumentException("Could not extract length out of variable-length-field");

                length += (int)((currentByte & 0x0F) * Math.Pow(10, i));

            }

            return length;
        }

        public static byte[] GetLVarData(byte[] rawData, int lengthBytes)
        {
            byte[] data = new byte[lengthBytes + rawData.Length];

            int myLength = rawData.Length;
            for (int i = 0; i < lengthBytes; i++)
            {
                byte currentByte = (byte)((myLength % 10) | 0xF0);
                myLength /= 10;

                data[lengthBytes - i - 1] = currentByte;
            }

            if (myLength > 0)
                throw new ArgumentException(string.Format("#{0}-Bytes cannot be compressed in L-{1}-Var value", rawData.Length, lengthBytes));

            Array.Copy(rawData, 0, data, lengthBytes, rawData.Length);
            return data;

        }


        public static int GetLLVarLength(byte[] buffer, int offset)
        {
            if (buffer.Length - offset < 2)
                throw new ArgumentException("For LL-Var at least 2 bytes are required");

            return GetLVarLength(buffer, offset, 2) + 2;
        }

        public static int GetLLLVarLength(byte[] buffer, int offset)
        {
            if (buffer.Length - offset < 3)
                throw new ArgumentException("For LLL-Var at least 3 bytes are required");

            return GetLVarLength(buffer, offset, 3) + 3;
        }


        public static byte[] ReadLVarData(byte[] buffer, int offset, int lengthBytes)
        {
            int dataLength = GetLVarLength(buffer, offset, lengthBytes);

            byte[] data = new byte[dataLength];
            Array.Copy(buffer, offset + 2, data, 0, dataLength);

            return data;
        }


        public static byte[] ReadLLVarData(byte[] buffer, int offset)
        {
            return ReadLVarData(buffer, offset, 2);
        }

        public static byte[] ReadLLLVarData(byte[] buffer, int offset)
        {
            return ReadLVarData(buffer, offset, 3);
        }

        public static byte[] GetLLVarData(byte[] rawData)
        {
            return GetLVarData(rawData, 2);
        }

        public static byte[] GetLLLVarData(byte[] rawData)
        {
            return GetLVarData(rawData, 3);
        }

    }
}
