using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Coins { get; private set; }
    
    public event Action CoinsChanged;
    
    public void Initialize(GameData gameData)
    {
        Coins = gameData.WalletCoins;
        CoinsChanged?.Invoke();
    }
    
    public void AddCoin() 
    { 
        Coins++;
        CoinsChanged?.Invoke();
    }

    public void BuyForCoins(int price)
    { 
        Coins -= price;
        CoinsChanged?.Invoke();
    }
}
