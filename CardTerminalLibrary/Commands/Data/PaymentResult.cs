using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Commands
{
    public class PaymentResult : CommandResult
    {
        /// <summary>
        /// Identifier of the payment process on the terminal
        /// </summary>
        private IPaymentData _data;

        public PaymentResult(bool success, int? protocolSpecificErrorCode, string protocolSpecificDescription, IPaymentData data)
        {
            _success = success;
            _protocolSpecificErrorCode = protocolSpecificErrorCode;
            _protocolSpecificErrorDescription = protocolSpecificDescription;

            _data = data;
        }

    }
}
