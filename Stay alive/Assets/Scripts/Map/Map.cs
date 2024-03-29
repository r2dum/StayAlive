using DG.Tweening;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;
    
    public void DestroyPlayButton(bool animated)
    {
        if (animated == false)
        {
            Destroy(_playButton);
            return;
        }
        
        DOTween.Sequence()
            .Append(_playButton.transform.DOMove(new Vector3(0f, -14f, -11f), 1f))
            .SetEase(Ease.InSine)
            .OnComplete(() => Destroy(_playButton));
    }
}
