using System;
using System.Collections.Generic;
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
		/// <summary>
		/// This methode is used to convert a int length to a lenght field according to the
		/// ZVT-Standard.
		/// 
		/// If the length is 255 or higher a extended length field is used.
		/// A extended length field consists of 3 bytes 
		/// byte 1: low byte 
		/// byte 2: high byte
		///
		/// If the length is smaller than 255 a normal length field of 1 byte is used.
		/// </summary>
		/// <returns>
		/// The length.
		/// </returns>
		/// <param name='len'>
		/// Length.
		/// </param>
		public static byte[] convertLength(int len){
			byte[] lohi=ParameterByteHelper.int16ToByte((Int16)len);
			byte[] buffer;
			if(lohi[1]!=0x00 || lohi[0]==0xFF){
				buffer=new byte[2];
				buffer[0]=lohi[0];
				buffer[1]=lohi[1];
				return buffer;
			}else{
				buffer=new byte[1];
				buffer[0]=lohi[0];
				return buffer;
			}
		}

		public static int getLength(List<byte> buffer,int offset){
				return 0;
		}
	}
}

