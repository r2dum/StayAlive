using UnityEngine;

public class PlayerLose : MonoBehaviour
{
    [SerializeField] private SwipeManager _swipeManager;
    [SerializeField] private GameObject _losePanel;

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }
    
    private void OnEnable()
    {
        _player.Died += Lose;
    }

    private void OnDisable()
    {
        _player.Died -= Lose;
    }

    private void Lose()
    {
        _swipeManager.enabled = false;
        _losePanel.SetActive(true);
    }
}
