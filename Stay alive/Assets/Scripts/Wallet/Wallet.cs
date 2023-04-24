using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Coins
    { 
        get => PlayerPrefs.GetInt("Cash", default);
        private set => PlayerPrefs.SetInt("Cash", value);
    }

    public event Action<int> CoinsChanged;

    public void AddCoin() 
    { 
        Coins++;
        CoinsChanged?.Invoke(Coins);
    }

    public void BuyWithCoins(int price)
    { 
        Coins -= price;
        CoinsChanged?.Invoke(Coins);
    }
}
