using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private PlayerPrefsSystem _playerPrefsSystem;
    
    public int Coins { get; private set; }
    
    public event Action CoinsChanged;
    
    public void Initialize(PlayerPrefsSystem playerPrefsSystem)
    {
        _playerPrefsSystem = playerPrefsSystem;
        
        Coins = _playerPrefsSystem.Load(Constants.CASH);
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
        _playerPrefsSystem.Save(Constants.CASH, Coins);
        CoinsChanged?.Invoke();
    }
}
