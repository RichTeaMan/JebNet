using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JebNet.Server
{
    public class JsonHelper
    {
        public static string arrayToJson<T>(T[] array)
        {
            string json = string.Format("[ {0} ]", string.Join(", ", array.Select(e => JsonUtility.ToJson(e)).ToArray()));
            return json;
        }
    }
}
