using System;

namespace Wiffzack.Devices.CardTerminals
{
	public class TLVLength
	{
		public int length{
			get{ return length;}
			set{length=(int)value;}
		}
		public TLVLength (int len)
		{
			
		}
		 public void AddToBytes(List<byte> buffer)
        {
            
			buffer.Add(_myByte);
        }
		
	}
}

