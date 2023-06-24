using UnityEngine;
using UnityEngine.UI;

public class WalletSoundWithUIView : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private AudioSource _coinSound;
    
    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.CoinsChanged += SetAmount;
        _wallet.CoinsChanged += PlayCoinSound;
        SetAmount();
    }
    
    private void OnDisable()
    {
        _wallet.CoinsChanged -= SetAmount;
        _wallet.CoinsChanged -= PlayCoinSound;
    }

    private void SetAmount()
    {
        _coinsText.text = $"{_wallet.Coins}";
    }

    private void PlayCoinSound()
    {
        _coinSound.Play();
    }
}
