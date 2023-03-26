using System;
using UnityEngine;

public class Wallet
{
    public int Coins
    { 
        get => PlayerPrefs.GetInt("Cash", default);
        private set => PlayerPrefs.SetInt("Cash", value);
    }

    public event Action CoinsChanged;

    public void AddCoin() 
    { 
        Coins++;
        CoinsChanged?.Invoke();
    }

    public void BuyWithCoins(int price)
    { 
        Coins -= price;
        CoinsChanged?.Invoke();
    }
}
