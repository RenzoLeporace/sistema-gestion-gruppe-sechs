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
            try
            {
                if (!File.Exists(fileName) || new FileInfo(fileName).Length == 0)
                    return new List<T>();
                using (var stream = File.OpenRead(fileName))
                {
                    var serializer = new XmlSerializer(typeof(List<T>));
                    var obj = serializer.Deserialize(stream) as List<T>;
                    return obj ?? new List<T>();
                }
            }
            catch
            {
                try
                {
                    var corruptBackup = fileName + ".corrupt_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (File.Exists(fileName))
                        File.Move(fileName, corruptBackup);
                }
                catch {}

                return new List<T>();
            }
        }
        public static void Guardar<T>(string fileName, List<T> data)
        {
            var dir = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (var stream = File.Create(fileName))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(stream, data ?? new List<T>());
            }
        }
    }
}
