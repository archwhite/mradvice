using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    public class JSON
    {
        public static string to(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
