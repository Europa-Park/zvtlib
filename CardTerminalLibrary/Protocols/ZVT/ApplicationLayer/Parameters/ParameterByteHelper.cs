using System;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters
{
	public class ParameterByteHelper
	{
		public ParameterByteHelper ()
		{
		}
		/// <summary>
		/// Converts a Int16 to a byte[]
		/// index 0 is the lowByte
		/// index 1 is the highByte
		/// </summary>
		/// <returns>
		/// The to byte.
		/// </returns>
		/// <param name='toconv'>
		/// Toconv.
		/// </param>
		public static byte[] int16ToByte(Int16 toconv){
			byte highByte = (byte) ( ( toconv >> 8 ) & 0xFF );
			byte lowByte = (byte) ( toconv & 0xFF );
			byte[] array=new byte[2];
			array[0]=lowByte;
			array[1]=highByte;
			return array;
		}
	}
}

