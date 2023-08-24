using System.IO;
using UnityEngine;

public class JsonSaveSystem : ISaveSystem
{ 
    private readonly string _filePath;

    public JsonSaveSystem(string filePath)
    {
        _filePath = Application.persistentDataPath + filePath;
    }

    public void Save<T>(T data)
    {
        string json = JsonUtility.ToJson(data);
        using (var writer = new StreamWriter(_filePath))
        {
            writer.WriteLine(json);
        }
    }
    
    public T Load<T>(T data)
    {
        if (File.Exists(_filePath) == false)
        {
            Save(data);
            return data;
        }
        
        string json = "";
        using (var reader = new StreamReader(_filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                json += line;
            }
        }
        
        return JsonUtility.FromJson<T>(json);
    }
}
