using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU
{
	public class AbortApdu : ApduBase
	{
		public AbortApdu ()
		{
			_parameters=new List<IParameter>();
		}
		public override bool SendsCompletionPacket
        {
            get { return false; }
        }

         protected override byte[] ByteControlField
        {
            get { return new byte[] { 0x06, 0xB0 }; }
        }
		public override byte[] GetRawApduData()
        {
            List<byte> buffer = new List<byte>();

            foreach (IParameter param in _parameters)
                param.AddToBytes(buffer);
//			buffer.Add(0x06);
//			buffer.Add(0x00);
			int len=buffer.Count;
			byte[] lenarr=ParameterByteHelper.convertLength(len);
			for(int i=lenarr.Length-1;i>=0;i--){
				buffer.Insert(0,lenarr[i]);
			}
			if(lenarr.Length>=2)
				buffer.Insert(0,0xFF);
            buffer.InsertRange(0, ByteControlField);
            //return buffer.ToArray();
			byte[] test=new byte[] {0x06, 0x20, 0x03, 0x00,0x00,0x00};
			return test;
        }
	}
}

