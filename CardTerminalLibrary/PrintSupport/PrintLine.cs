using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.PrintSupport
{
    public class PrintLine : List<IPrintText>, IPrintLine
    {
        #region IPrintLine Members

        public IPrintText[] Commands
        {
            get { return this.ToArray(); }
        }

        #endregion
    }
}
