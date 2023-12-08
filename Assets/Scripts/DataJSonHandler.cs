using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataJSonHandler : MonoBehaviour
{
    public bool SaveData<T>(string relativePath, T data) 
    {
        string path = Application.persistentDataPath + relativePath;

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }
        catch (Exception e)
        {
            Debug.LogError("Saving data fail: " + e.Message);
            return false;
        }

        return true;
    }

    public T LoadData<T>(string relativePath) 
    {
        string path = Application.persistentDataPath + relativePath;

        if (!File.Exists(path))
        {
            Debug.LogError("Loading data fail: source file does not exist: " + path );
        }

        try 
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError("Loading data fail: " + e.Message);
            throw e;
        }
    }
}
