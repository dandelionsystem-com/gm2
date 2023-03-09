using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using System.Runtime.Serialization.Formatters.Binary;

namespace System.gm
{
    public class ObjectBytesConverter
    {
        public static byte[] ConvertToBytes(Object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T ConvertToObject<T>(byte[] bytes)
        {
            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                //memStream.Write(bytes, 0, bytes.Length);
                //memStream.Seek(0, SeekOrigin.Begin);
                var obj = (T)bf.Deserialize(memStream);
                return obj;
            }
        }
    }
}
