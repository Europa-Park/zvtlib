using System;
using System.Collections.Generic;
namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV
{
	public class TLVLength:IParameter
	{
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV.TLVLength"/> class.
		/// </summary>
		/// <param name='len'>
		/// Length.
		/// </param>
		public TLVLength (int len)
		{
			tlvLength=len;
			if(len<=127){
				Length=1;
			}else{
				if(len<=255){
					Length=2;
				}else{
					Length=3;
				}
			}
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
			// If the first byte is 129 then 1 length byte follows
			if(firstByte==129){
				Length=2;
				tlvLength=(int)buffer[offset+1];
				return;
			}
			/*
			 * If first byte is 130 then 2 length bytes follow 
			 * 2nd byte = high byte
			 * 3rd byte = low byte
			 */ 
			if(firstByte==130){
				Length=3;
				byte[] lenbuffer=new byte[2];
				lenbuffer[0]=buffer[offset+1];
				lenbuffer[1]=buffer[offset+2];
				tlvLength=ParameterByteHelper.byteToInt16(lenbuffer,true);
				return;
			}
			// Throw Exception for any other firstByte
			
			
		}

        /// <summary>
        /// Adds the serialized Parameter to the apdu byte stream
        /// </summary>
        /// <param name="buffer"></param>
        public void AddToBytes(List<byte> buffer){
			int len=tlvLength;
			byte[] lenarr=ParameterByteHelper.convertLength(len);
			/**
				 *According to TLV length a byte equals 0xxx xxxx is the length
				 *1000 0000 is a invalid length byte
				 *1000 0001 indicates that one length byte follows for lengths ranging from 128-254 
				 *1000 0010 indricates that two length bytes follow for lengths ranign from 255 onwards 
				 */
			if(len<=127){
				buffer.Add((byte)len);
			}else{
				int before=buffer.Count;
				for(int i=0;i<lenarr.Length;i++){
					buffer.Insert(before,lenarr[i]);
				}
				if(lenarr.Length==2)
					buffer.Insert(before,(byte)130);
				if(lenarr.Length==1)
					buffer.Insert(before,(byte)129);
			}
		
		}
		
		public int tlvLength{
			get{ return tlvLength;}
			set{ tlvLength=(int)value;}
		}
	}
}

