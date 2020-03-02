using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class JsonTools
{
    public static string serializeObjToJson(object obj)
    {
        string result = JsonConvert.SerializeObject(obj);
        return result;
    }
    public static T deserializeJsonToObj<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
    public static JObject deserializeJsonToObj(string json)
    {
        JObject jObject = JObject.Parse(json);
        return jObject;
    }
    public static object deserializeJsonToObj(string json, Type t)
    {
        return JsonConvert.DeserializeObject(json, t);
    }
    public static Boolean isJsonFormat(string json)
    {
        return false;
    }
}

