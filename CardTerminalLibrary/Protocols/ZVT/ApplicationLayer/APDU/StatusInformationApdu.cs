using System;
using System.Collections.Generic;
using System.Text;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;

namespace Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU
{
    /// <summary>
    /// Wrapper around the ApduResponse to easily parse status informations
    /// </summary>
    /// <remarks>
    /// The special thing about status informations
    /// </remarks>
    public abstract class StatusInformationApdu : ApduResponse
    {

        /// <summary>
        /// Saves the parsed paramters from the Apdu
        /// </summary>
        private List<IParameter> _parameters = new List<IParameter>();

        public StatusInformationApdu(byte[] rawApduData)
            : base(rawApduData)
        {
        }


        /// <summary>
        /// Creates an empty Parameter Object wich is then filled by ParseFromBytes
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        protected virtual IParameter CreateParameterForBMP(byte bmp)
        {
            switch (bmp)
            {
                //Result Code
                case 0x27:
                    return new PrefixedParameter<StatusInformationResultCode>(bmp, new StatusInformationResultCode());

                //Amount 6 Bytes
                case 0x04:
                    return new PrefixedParameter<BCDNumberParameter>(bmp, new BCDNumberParameter(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));

                //Trace Nr: 3 Bytes
                case 0x0B:
                    return new PrefixedParameter<BCDNumberParameter>(bmp, new BCDNumberParameter(0, 0, 0, 0, 0, 0));

                //Original Trace Nr (Only for reversal): 3 Bytes
                case 0x37:
                    return new PrefixedParameter<BCDNumberParameter>(bmp, new BCDNumberParameter(0, 0, 0, 0, 0, 0));

                //time: 3bytes
                case 0x0C:
                    return new PrefixedParameter<StatusTimeParameter>(bmp, new StatusTimeParameter(0, 0, 0));

                //date: 2bytes
                case 0x0D:
                    return new PrefixedParameter<StatusDateParameter>(bmp, new StatusDateParameter(0, 0));

                //expiry date: 2bytes
                case 0x0E:
                    return new PrefixedParameter<StatusExpDateParameter>(bmp, new StatusExpDateParameter(0, 0));

                //Sequence Number: 2Bytes
                case 0x17:
                    return new PrefixedParameter<BCDNumberParameter>(bmp, new BCDNumberParameter(0, 0, 0, 0));

                //Payment Type: 1Byte
                case 0x19:
                    return new PrefixedParameter<StatusPaymentTypeParam>(bmp, new StatusPaymentTypeParam());

                //Terminal ID: 4Bytes
                case 0x29:
                    return new PrefixedParameter<BCDNumberParameter>(bmp, new BCDNumberParameter(0, 0, 0, 0, 0, 0, 0, 0));

                default:
                    return null;

            }
        }

    }
}
