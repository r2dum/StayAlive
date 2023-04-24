using UnityEngine;

public class GameLose : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
        _player.Died += ShowLosePanel;
    }

    private void OnDisable()
    {
        _player.Died -= ShowLosePanel;
    }

    private void ShowLosePanel()
    {
        _losePanel.Show();
    }
}
