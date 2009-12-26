using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Wiffzack.Devices.CardTerminals.Commands
{
    /// <summary>
    /// Implemented by classes that provide protocol specific data in some way, see IAuthorizationData for further information
    /// Basically this interface just provides serialization mechanisms
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// Writes the data of the element to the given root node
        /// </summary>
        /// <param name="rootNode"></param>
        void WriteXml(XmlElement rootNode);

        /// <summary>
        /// Reads the data back from the given root node
        /// </summary>
        /// <param name="rootNode"></param>
        void ReadXml(XmlElement rootNode);

    }
}
