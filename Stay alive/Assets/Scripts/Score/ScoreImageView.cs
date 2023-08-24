using UnityEngine;
using UnityEngine.UI;

public class ScoreImageView : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private Sprite[] _bombSprites;
    
    public void Initialize(BombType bombType)
    {
        var sprite = _bombSprites[(int)bombType];
        
        foreach (var image in _images)
            image.sprite = sprite;
    }
}
