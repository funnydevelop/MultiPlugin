using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public class GetObjInfo
{
    static BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    public static PropertyInfo[] getObjProperties(object obj)
    {
        Type types = obj.GetType();
        PropertyInfo[] propertyInfos = types.GetProperties();
        return propertyInfos;
    }

    public static PropertyInfo findPropertyFromObj(object obj,string name) {
        Type type = obj.GetType();
        PropertyInfo property = type.GetProperty(name);
        while (type != null)
        {
            property = type.GetProperty(name);
            if (property != null)
            {
                break;
            }

            type = type.BaseType;
        }

        if (property == null)
        {
            Debug.Log("can not find this property from this object");
            return null;
        }
        else
        {
            return property;
        }
    }

}

