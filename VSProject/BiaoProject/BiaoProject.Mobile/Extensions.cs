using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace BiaoProject.Mobile
{
    public static class Extensions
    {
        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            return new SortedDictionary<TKey, TValue>(dictionary);
        }

        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            return new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        public enum SerializerEnum
        {
            DataContractJsonSerializer,
            JavaScriptJsonSerializer
        }
        public static string ToJson<T>(this T obj, SerializerEnum serializer = SerializerEnum.DataContractJsonSerializer)
        {
            if (serializer == SerializerEnum.DataContractJsonSerializer)
            {
                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream stream = new MemoryStream())
                {
                    s.WriteObject(stream, obj);
                    return Encoding.Default.GetString(stream.ToArray());
                }
            }
            else
            {
                JavaScriptSerializer s = new JavaScriptSerializer();
                return s.Serialize(obj);
            }
        }
    }
}