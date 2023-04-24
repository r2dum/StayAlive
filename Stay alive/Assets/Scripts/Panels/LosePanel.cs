using UnityEngine;

public class LosePanel : MonoBehaviour
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
