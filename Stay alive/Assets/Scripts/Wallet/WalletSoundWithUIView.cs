using UnityEngine;
using UnityEngine.UI;

public class WalletSoundWithUIView : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private AudioSource _pickCoinSound;
    
    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.CoinsChanged += SetAmount;
        SetAmount(_wallet.Coins);
    }
    
    private void OnDisable()
    {
        _wallet.CoinsChanged -= SetAmount;
    }

    private void SetAmount(int amount)
    {
        _coinsText.text = $"{amount}";
    }

    private void PlaySound()
    {
        _pickCoinSound.Play();
    }
}
