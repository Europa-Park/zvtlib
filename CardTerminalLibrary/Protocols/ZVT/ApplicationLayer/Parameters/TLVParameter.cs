using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV;
namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters
{
	public class TLVParameter : IParameter
	{
		public bool enablePrefix{
			 get{ return enablePrefix;}
			 set{ enablePrefix = (bool)value;}
		}
		public byte prefix
        {
             get { return 0x06; }
        }
		public TLVLength tlvLength{
			get{ return new TLVLength(Length);}
		}
		public int prefixLen{
			get{return prefixLen;}
			set{prefixLen=(int)value;}
		}
				
		public int Length{
			 get{
				int len=0;
				if(subparams!=null){
					foreach (IParameter param in subparams){
						if(param!=null)
	                		len+=param.Length;
					}
				}
				if(enablePrefix){
					len++;
					if(prefixLen<=127){
						len++;
					}else{
						byte[] lenarr=ParameterByteHelper.convertLength(prefixLen);
						len=len+lenarr.Length+1;
					}
				}	
				return len;}
		}
		
		public List<IParameter> subparams{
			 get{ return subparams;}
			 set{ subparams=(List<IParameter>) value;}
		}
		
		public TLVParameter ()
		{
			enablePrefix=false;
			subparams=null;
		}
    	#region IParameter Members
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
			// Saves the position of the last byte for latter inserts
			int before=buffer.Count;
			// gets the TLV Length field and adds its bytes to the byte list
			tlvLength.AddToBytes(buffer);
			if(subparams!=null){
				foreach (IParameter param in subparams){
					if(param!=null)
                		param.AddToBytes(buffer);
				}
			}
			//to-do insert TLV-Tag
			
			// If prefix is enabled the bmp id and length for tlv will be added infront of every thing
			if(enablePrefix){
				buffer.Add(prefix);
				int len=prefixLen;
				byte[] lenarr=ParameterByteHelper.convertLength(len);
				/**
				 *According to TLV length a byte equals 0xxx xxxx is the length
				 *1000 0000 is a invalid length byte
				 *1000 0001 indicates that one length byte follows for lengths ranging from 128-254 
				 *1000 0010 indricates that two length bytes follow for lengths ranign from 255 onwards 
				 */
				if(len<=127){
					buffer.Insert(before,(byte)len);
				}else{
					for(int i=lenarr.Length-1;i>=0;i--){
						buffer.Insert(before,lenarr[i]);
					}
					if(lenarr.Length==2)
						buffer.Insert(before,(byte)130);
					if(lenarr.Length==1)
						buffer.Inster(before,(byte)129);
				}
				buffer.Insert(before,prefix);
			}
				
		}
		#endregion
	}
}

