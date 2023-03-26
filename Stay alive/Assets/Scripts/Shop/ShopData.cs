using System;
using System.Collections.Generic;

[Serializable]
public class ShopData
{
    public string CurrentPlayer = "Player_1";
    public List<string> HavePlayers = new List<string> { "Player_1" };

    public string CurrentMap = "Map_1";
    public List<string> HaveMaps = new List<string> { "Map_1" };
}
