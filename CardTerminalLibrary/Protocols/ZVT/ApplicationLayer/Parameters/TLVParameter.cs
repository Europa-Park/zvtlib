using System;
using System.Collections.Generic;
using System.Text;
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
		public int Length{
			 get{
				int len=0;
				foreach (IParameter param in subparams){
					if(param!=null)
                		len+=param.Length;
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
				int len=buffer.Count-before;
				byte[] lenarr=ParameterByteHelper.convertLength(len);
				for(int i=lenarr.Length-1;i>=0;i--){
					buffer.Insert(before,lenarr[i]);
				}
				buffer.Insert(before,prefix);
			}
				
		}
		#endregion
	}
}

