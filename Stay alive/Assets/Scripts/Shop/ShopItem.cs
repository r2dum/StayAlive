using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _price;
    public int Price => _price;
}
