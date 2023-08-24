using UnityEngine;

public class ShopItemMap : ShopItem
{
    [SerializeField] private BombType _bombType;
    [SerializeField] private Color _color;
    
    public BombType BombType => _bombType;
    public Color Color => _color;
}
