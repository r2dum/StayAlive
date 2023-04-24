using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private BombType _bombType;
    [SerializeField] private Color _color;

    public BombType BombType => _bombType;
    public Color Color => _color;
}
