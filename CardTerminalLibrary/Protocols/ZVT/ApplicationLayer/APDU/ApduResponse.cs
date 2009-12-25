using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU
{
    /// <summary>
    /// Interface for all available Apdu Responses
    /// </summary>
    public class ApduResponse : IZvtApdu
    {

        public static ApduResponse Create(byte[] rawApduData)
        {
            if (rawApduData == null)
                return null;

            return new ApduResponse(rawApduData);
                    
        }

        private byte[] _rawApduData;

        public ApduResponse(byte[] rawApduData)
        {
            _rawApduData = rawApduData;
        }

        #region IZvtApdu Members

        public byte[] GetRawApduData()
        {
            return _rawApduData;
        }

        public bool SendsCompletionPacket
        {
            get { return false; }
        }

        public ControlField ControlField
        {
            get { return new ControlField(_rawApduData[0], _rawApduData[1]); }
        }

        #endregion
    }
}
