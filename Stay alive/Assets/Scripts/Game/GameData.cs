using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public int GameFps = 60;
    public LightShadows LightShadow = LightShadows.Hard;
    public float SoundVolume = 1f;
    
    public Color UIColor = Color.white;
    public BombType BombType = BombType.Stone;
    
    public int WalletCoins;
    public int BestScore;
}
