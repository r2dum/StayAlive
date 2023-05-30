using System;
using UnityEngine;

public class PlayerPrefsSystem
{
    public void Save(string key, int value)
    {
        switch (key)
        {
            case Constants.CASH:
                PlayerPrefs.SetInt(Constants.CASH, value);
                break;
            case Constants.RECORD:
                PlayerPrefs.SetInt(Constants.RECORD, value);
                break;
            default:
                throw new Exception($"Unable to save {key}");
        }
    }
    
    public int Load(string key)
    {
        switch (key)
        {
            case Constants.CASH:
                return PlayerPrefs.GetInt(Constants.CASH);
            case Constants.RECORD:
                return PlayerPrefs.GetInt(Constants.RECORD);
            default:
                throw new Exception($"Unable to load {key}");
        }
    }
}
