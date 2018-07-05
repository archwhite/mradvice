using ArxOne.MrAdvice.Advice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace AOP
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class MrAdviceAttribute : Attribute, IMethodAdvice
    {
        public static Dictionary<string, long> MemoryMap = new Dictionary<string, long>();
        public long MemThreshold = 4100;
        public MrAdviceAttribute() { }

        public long PrintPropertiesSizeof(MethodAdviceContext context)
        {
            Type type = context.TargetType;
            long totalsize = 0;
            foreach (var p in type.GetProperties())
            {
                try
                {
                    var v = p.GetValue(context.Target);
                    var varsize = Memory.Sizeof(v);
                    totalsize += varsize;
                    Debug.WriteLine($"VAR {p.Name} = {JSON.to(v)} size = {varsize}");
                    //MemoryMap[fname + tname + p.Name] = varsize;
                }
                catch { };
            }
            return totalsize;
        }

        public void Advise(MethodAdviceContext context)
        {
            try
            {
                var tname = context.TargetName;
                var fname = context.TargetType.FullName;
                System.Diagnostics.Debug.WriteLine($"ASPECT INFO: {fname + tname}");
                context.Proceed();
                if (tname == "Get")
                {
                    long totalsize = PrintPropertiesSizeof(context);
                    Debug.WriteLine($"total size = {totalsize}");
                    if (totalsize > MemThreshold)
                    {
                        Debug.Write(fname + tname + context.Target.ToString());
                        Debug.WriteLine(totalsize + "is more than threshold, that's " + MemThreshold);
                        Debug.WriteLine("INFO:");
                        PrintPropertiesSizeof(context);
                        Debugger.Break();
                    }

                    var result = context.ReturnValue;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

    }
}
