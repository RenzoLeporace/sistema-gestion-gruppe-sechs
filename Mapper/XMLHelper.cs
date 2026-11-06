using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Mapper
{
    public class XMLHelper
    {
        public static List<T> Leer<T>(string fileName)
        {
            return SqliteStore.Leer<T>(fileName);
        }
        public static void Guardar<T>(string fileName, List<T> data)
        {
            SqliteStore.Guardar(fileName, data);
        }
    }
}
