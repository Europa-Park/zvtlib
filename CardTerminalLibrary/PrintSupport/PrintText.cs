using System;
using System.Collections.Generic;
using System.Text;

namespace Wiffzack.Devices.CardTerminals.PrintSupport
{
    public class PrintText : IPrintText
    {

        private string _text;
        private bool _bold;
        private bool _doubleHeight;
        private bool _doubleWidth;
        private bool _center;
        private int? _alignRight;

        #region IPrintText Members

        public string Text
        {
            get { return _text; }
        }

        public bool Bold
        {
            get { return _bold; }
        }

        public bool DoubleHeight
        {
            get { return _doubleHeight; }
        }

        public bool DoubleWidth
        {
            get { return _doubleWidth; }
        }

        public bool Center
        {
            get { return _center; }
        }

        public int? AlignRight
        {
            get { return _alignRight; }
        }

        #endregion


        public PrintText(string text, bool bold, bool doubleHeight, bool doubleWidth, bool center, int? alignRight)
        {
            _text = text;
            _bold = bold;
            _doubleHeight = doubleHeight;
            _doubleWidth = doubleWidth;
            _center = center;
            _alignRight = alignRight;
        }
    }
}
