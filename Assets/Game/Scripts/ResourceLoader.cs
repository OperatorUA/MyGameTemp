using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ResourceLoader
{
    public static T Load<T>(string dataFileName) where T : ScriptableObject
    {
        string folderName = "ScriptableObjects";
        string path = folderName + "/" + dataFileName;

        T loadedResource = Resources.Load<T>(path);
        if (loadedResource == null)
        {
            Debug.LogError($"Resource not found at path: {path}");
        }
        else
        {
            Debug.Log("Resource " + dataFileName + " loaded!");
        }
        return loadedResource;
    }

    public static List<T> LoadAll<T>(string path) where T : ScriptableObject
    {
        List<T> loadedResources = new List<T>();

        T[] resourcesArray = Resources.LoadAll<T>(path);
        if (resourcesArray != null)
        {
            loadedResources.AddRange(resourcesArray);
        }
        else
        {
            Debug.LogError($"No resources of type {typeof(T).Name} found in folder: ScriptableObjects");
        }

        return loadedResources;
    }
    public static T Clone<T>(T obj)
    {
        return default(T);
    }
    public static T DeepCopy<T>(T obj)
    {
        if (obj == null) return default(T);

        Type type = obj.GetType();
        T cloneObj;

        if (obj is ScriptableObject)
        {
            cloneObj = (T)(object)ScriptableObject.CreateInstance(type);
        }
        else if (obj is GameObject || obj is Sprite)
        {
            return obj;
        }
        else
        {
            if (!type.IsValueType && type.GetConstructor(Type.EmptyTypes) == null)
            {
                Debug.LogWarning("The type must have a parameterless constructor.");
                return default(T);
            }

            cloneObj = (T)Activator.CreateInstance(type);
        }

        if(obj is IList)
        {
            return (T)DeepCopy((IList)obj);
        }
        else if (obj is IDictionary)
        {
            return (T)DeepCopy((IDictionary)obj);
        }
        else
        {
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                object value = fieldInfo.GetValue(obj);

                if (!isPrimitive(value))
                {
                    value = DeepCopy(value);
                }

                fieldInfo.SetValue(cloneObj, value);
            }
        }

        if (obj is AttributeParameter)
        {
            AttributeParameter parameter = cloneObj as AttributeParameter;
            parameter.Init();
        }

        return cloneObj;
    }

    public static IList DeepCopy(IList originalList)
    {
        Type type = originalList.GetType();

        Type listType = type.GetGenericArguments()[0];
        if (!listType.IsValueType)
        {
            Debug.LogWarning("the list cannot contain reference types");
        }

        IList clonedList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listType));

        foreach (object item in originalList)
        {
            object value;

            if (!isPrimitive(item)) value = DeepCopy(item);
            else value = item;

            clonedList.Add(value);
        }

        return clonedList;
    }

    public static IDictionary DeepCopy(IDictionary obj)
    {
        if (obj == null)
            return null;

        Type dictType = obj.GetType();

        IDictionary clonedDict = (IDictionary)Activator.CreateInstance(dictType);

        foreach (object item in obj.Keys)
        {
            object key = item;
            object value = obj[key];

            if (!isPrimitive(key)) key = DeepCopy(key);

            if (isPrimitive(value)) clonedDict[key] = value;
            else clonedDict[key] = DeepCopy(value);
        }

        return clonedDict;
    }

    public static bool isPrimitive(object obj)
    {
        Type type = obj.GetType();
        if (type.IsPrimitive || type == typeof(string) || type.IsEnum) return true;
        return false;
    }
}
