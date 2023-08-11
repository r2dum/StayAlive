using UnityEngine;
using UnityEngine.UI;

public class ScoreImage : MonoBehaviour
{
    private Image _image;
    
    public void Initialize(Sprite sprite)
    {
        _image = GetComponent<Image>();
        _image.sprite = sprite;
    }
}
