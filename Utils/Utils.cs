using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class JSON
    {
        public static string to(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }

    public class Memory
    {
        public static long Sizeof(object o)
        {
            long size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, o);
                size = s.Length;
                return size;
            }
        }
    }
}
