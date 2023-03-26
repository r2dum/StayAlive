using System.Collections;
using UnityEngine;

public class Warn : MonoBehaviour, ISpawnable
{
    private void Start()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(1.3f);
        gameObject.SetActive(false);
    }
}
