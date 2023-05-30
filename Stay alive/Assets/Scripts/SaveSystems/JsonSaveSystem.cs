using System.IO;
using UnityEngine;

public class JsonSaveSystem
{ 
    private readonly string _filePath;

    public JsonSaveSystem()
    {
        _filePath = Application.persistentDataPath + "/ShopData.json";
    }

    public void Save(ShopData shopData)
    {
        string json = JsonUtility.ToJson(shopData);
        using (var writer = new StreamWriter(_filePath))
        {
            writer.WriteLine(json);
        }
    }
    
    public ShopData Load(ShopData shopData)
    {
        if (File.Exists(_filePath) == false)
        {
            Save(shopData);
            return shopData;
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
        
        return JsonUtility.FromJson<ShopData>(json);
    }
}
