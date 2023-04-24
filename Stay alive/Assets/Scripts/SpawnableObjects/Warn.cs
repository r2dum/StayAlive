using UnityEngine;

public class Warn : MonoBehaviour, ISpawnable
{
    //It is used in Animation Event, to disable the object after the animation has finished
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
