using System;
using System.Collections.Generic;
using System.Text;
namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV
{
	public class TLVTag:IParameter
	{
		/// <summary>
		/// The first byte contains the TLV class, type information and the first 5 bits
		/// of the tag number.
		/// </summary>
		/// <value>
		/// The first byte.
		/// </value>
		protected byte firstByte{
			get{return firstByte;}
			set{firstByte=(byte)value;}
		}
		
		/// <summary>
		/// The further bytes array contains all bytes of the tag number.
		/// </summary>
		/// <value>
		/// The further bytes.
		/// </value> 
		public byte[] furtherBytes{
			get{return furtherBytes;}
			set{furtherBytes=(byte[])value;}
		}
		/// <summary>
		/// This method sets a single bit of the first byte 
		/// </summary>
		/// <param name='bit'>
		/// Bit.
		/// </param>
		/// <param name='val'>
		/// Value.
		/// </param>
		private void setBitofFirstByte(int bit,bool val){
			  if (bit > 7)
                throw new ArgumentException("One byte only has 8 bits, so keep your num in the range from 0 to 7");

            byte bitInByte = (byte)(1<<bit);

            if (val)
                firstByte |= bitInByte;
            else
                firstByte &= (byte)~bitInByte;
		}
		/// <summary>
		/// Gets a single bit of the first byte
		/// </summary>
		/// <returns>
		/// The bits value
		/// </returns>
		/// <param name='bit'>
		/// If set to <c>true</c> bit.
		/// </param>
		/// <exception cref='ArgumentException'>
		/// Is thrown when an argument passed to a method is invalid.
		/// </exception>
		private bool getBitofFirstByte(int bit){
			 if (bit > 7)
                throw new ArgumentException("One byte only has 8 bits, so keep your num in the range from 0 to 7");
            byte bitInByte = (byte)(1 << bit);
            if ((firstByte & bitInByte) != 0)
                return true;
            else
                return false;
		}
		
		/// <summary>
		/// The objectType is either 0 or 1
		/// 0  .... primitive data object
		/// 1  .... constructed data object
		/// <see cref="Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV.TLVTag"/> object type.
		/// </summary>
		/// <value>
		/// <c>true</c> if object type; otherwise, <c>false</c>.
		/// </value>
		public bool objectType{
			get{ return getBitofFirstByte(5);}
			set{ setBitofFirstByte(5,(bool)value);}
		}
		/// <summary>
		/// The tlvClass is a value ranging from 0 to 3.
		/// 0 ... universal class
		/// 1 ... application class
		/// 2 ... context-specific class
		/// 3 ... private class
		/// </summary>
		/// <value>
		/// The tlv class.
		/// </value>
		public Int16 tlvClass{
			get{
				int val8=0;
				int val7=0;
				if(getBitofFirstByte(7))
					val8=1;
				if(getBitofFirstByte(6))
					val7=1;
				byte bit8=(byte)(val8<<1);
				byte bit7=(byte)(val7<<0);
				return (Int16)(bit8 | bit7);
			}
			set{
				// casting the int value to byte
				byte val=(byte)value;
				bool val8=false;
				bool val7=false;
				// checking if the 2nd bit is 1 or 0
				byte bitInByte = (byte)(1 << 1);
            	if ((val & bitInByte) != 0)
                	val8= true;
				// checking if the 1st bit is 1 or 0
            	bitInByte=(byte)(1 << 0);
				if((val & bitInByte) !=0)
					val7=true;
				// setting the bits
				setBitofFirstByte(7,val8);
				setBitofFirstByte(6,val7);
			}
		}
		
		
			
		public TLVTag ()
		{
		}
		
		public int Length{
			get{ return 1+furtherBytes.Length;}
		}
		/// <summary>
        /// Parses the parameter from the given buffer
        /// </summary>
        /// <param name="buffer"></param>
        public void ParseFromBytes(byte[] buffer, int offset){
		
		}

        /// <summary>
        /// Adds the serialized Parameter to the apdu byte stream
        /// </summary>
        /// <param name="buffer"></param>
        public void AddToBytes(List<byte> buffer){
		
		}
	}
}

