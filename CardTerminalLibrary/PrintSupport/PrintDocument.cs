using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.PrintSupport
{
    public class PrintDocument:List<IPrintLine>, IPrintDocument
    {
        #region IPrintDocument Members

        public IPrintLine[] PrintLines
        {
            get { return this.ToArray(); }
        }

        #endregion
    }
}
