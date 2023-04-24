using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Wallet wallet))
        {
            gameObject.SetActive(false);
            wallet.AddCoin();
        }
    }
}
