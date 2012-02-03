using System;
using System.Collections.Generic;
namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV
{
	public class TLVLength:IParameter
	{
		
		public TLVLength (int len)
		{
			
		}
		
		public int Length{
			get{ return Length;}
			set{ Length=(int)value;}
		}
		/// <summary>
        /// Parses the parameter from the given buffer
        /// </summary>
        /// <param name="buffer"></param>
        public void ParseFromBytes(byte[] buffer, int offset){
			int firstByte=(int)buffer[offset];
			/*
			 * If the first byte is smaller or equals 127 the byte represents the
			 * TLVs length.
			 */ 
			if(firstByte<=127){
				Length=1;
				tlvLength=buffer[offset];
				return;
			}
			if(firstByte==128){
			}
			
		}

        /// <summary>
        /// Adds the serialized Parameter to the apdu byte stream
        /// </summary>
        /// <param name="buffer"></param>
        public void AddToBytes(List<byte> buffer){
		
		}
		
		public int tlvLength{
			get{ return tlvLength;}
			set{ tlvLength=(int)value;}
		}
	}
}

