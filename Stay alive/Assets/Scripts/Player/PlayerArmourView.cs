using UnityEngine;
using UnityEngine.UI;

public class PlayerArmourView : MonoBehaviour
{
    [SerializeField] private Image _armourTimerImage;
    [SerializeField] private Text _armourTimerText;
    
    private PlayerArmour _playerArmour;
    
    public void Initialize(PlayerArmour playerArmour)
    {
        _playerArmour = playerArmour;
        
        _playerArmour.Activated += OnActivated;
        _playerArmour.TimeChanged += OnTimeChanged;
    }
    
    private void OnDisable()
    {
        _playerArmour.Activated -= OnActivated;
        _playerArmour.TimeChanged -= OnTimeChanged;
    }
    
    private void OnActivated(bool value)
    {
        _armourTimerImage.gameObject.SetActive(value);
    }
    
    private void OnTimeChanged(int value)
    {
        _armourTimerText.text = $"{value}";
    }
}
