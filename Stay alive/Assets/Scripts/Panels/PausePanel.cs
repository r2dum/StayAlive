using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}