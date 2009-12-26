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
        protected int _protocolSpecificErrorCode;


        /// <summary>
        /// Error description for simpler error tracement
        /// </summary>
        protected string _protocolSpecificErrorDescription;


        public bool Success
        {
            get { return _success; }
        }

    }
}
