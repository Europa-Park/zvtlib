using System;
using System.Collections.Generic;
using System.Text;
namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters.TLV
{
	public class TLVTag:IParameter
	{
		
		public TLVTag ()
		{
		}
		
		public int Length{
			get{ return Length;}
			set{Length=(int)value;}
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

