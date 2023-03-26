using UnityEngine;
using UnityEngine.UI;

public class WalletSoundWithUIView : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private AudioSource _pickCoinSound;
    
    public void SetAmount(int amount)
    {
        _coinsText.text = $"{amount}";
    }

    public void PlaySound()
    {
        _pickCoinSound.Play();
    }
}
