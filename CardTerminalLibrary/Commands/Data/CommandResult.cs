using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.Commands
{
    public class CommandResult
    {
        /// <summary>
        /// Indicates if the Operation was successful
        /// </summary>
        protected bool _success;

        /// <summary>
        /// Error code for error tracement, in terms of the used provider
        /// </summary>
        protected int? _protocolSpecificErrorCode;


        /// <summary>
        /// Error description for simpler error tracement
        /// </summary>
        protected string _protocolSpecificErrorDescription;


        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

        public int? ProtocolSpecificErrorCode
        {
            get { return _protocolSpecificErrorCode; }
            set { _protocolSpecificErrorCode = value; }
        }

        public string ProtocolSpecificErrorDescription
        {
            get { return _protocolSpecificErrorDescription; }
            set { _protocolSpecificErrorDescription = value; }
        }

    }
}
