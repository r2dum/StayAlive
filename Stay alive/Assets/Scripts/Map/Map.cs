using DG.Tweening;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;
    [SerializeField] private Sprite _bombSprite;
    [SerializeField] private BombType _bombType;
    [SerializeField] private Color _color;
    
    public Sprite BombSprite => _bombSprite;
    public BombType BombType => _bombType;
    public Color Color => _color;

    public void DestroyPlayButton()
    {
        DOTween.Sequence()
            .Append(_playButton.transform.DOMove(new Vector3(0f, -14f, -11f), 1f))
            .SetEase(Ease.InSine)
            .OnComplete(() => Destroy(_playButton));
    }
}
