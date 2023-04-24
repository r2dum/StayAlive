using UnityEngine;
using UnityEngine.UI;

public class GameUIColor : MonoBehaviour
{
    [SerializeField] private Image[] _images;

    public void Initialize(Color color)
    {
        foreach (var image in _images)
        {
            image.color = color;
        }
    }
}
