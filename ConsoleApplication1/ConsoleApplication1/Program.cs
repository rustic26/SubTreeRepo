using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public struct KV<T1, T2>
    {
        public KV(T1 key, T2 value) : this()
        {
            this.Key = key;
            this.Value = value;
        }

        public T1 Key { get; set; }
        public T2 Value { get; set; }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is KV<T1, T2>)
            {
                return ((KV<T1, T2>)obj).Key.Equals(this.Key);
            }

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var xs = new XmlSerializer(typeof(HashSet<KV<String, String>>));

            var set = new HashSet<KV<String, String>>() { new KV<String, String>("A", "A"), new KV<String, String>("A", "B") };

            using (var ms = new MemoryStream())
            {
                using (var sr = new StreamReader(ms))
                {
                    xs.Serialize(ms, set);

                    ms.Seek(0, SeekOrigin.Begin);

                    Console.WriteLine(sr.ReadToEnd());
                }
            }
        }
    }
}
