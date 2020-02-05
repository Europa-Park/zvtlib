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
			get{
				int len=0;
				if(subparams!=null){
					foreach (IParameter param in subparams){
						if(param!=null)
	                		len+=param.Length;
					}
				}
				return new TLVLength(len);
			}
		}
		/// <summary>
		/// Gets the length field of the prefix.
		/// </summary>
		/// <value>
		/// The length of the tlv container including the tag and length field.
		/// if prefix is not enabled it is equals to Length
		/// </value>
		public int prefixLen{
			get{int len=0;
				len=tlvLength.Length+tlvLength.tlvLength;
				// add tlv tag length len+=TLVTag.Length;
				return len;
			}
		}
				
		public int Length{
			 get{
				int len=0;
				len=prefixLen;
				if(enablePrefix){
					// Current len variable is equals to the length field of the prefix
					if(len<=127){
						len++;
					}else{
						byte[] lenarr=ParameterByteHelper.convertLength(len);
						len=len+lenarr.Length+1;
					}
					len++;
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
				for(int i=lenarr.Length-1;i>=0;i--){
					buffer.Insert(before,lenarr[i]);
				}
				if(lenarr.Length>=2)
					buffer.Insert(before,0xFF);
				buffer.Insert(before,prefix);
			}
				
		}
		#endregion
	}
}

