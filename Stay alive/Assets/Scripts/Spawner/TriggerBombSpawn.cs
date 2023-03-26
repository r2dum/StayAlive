using UnityEngine;

public class TriggerBombSpawn : MonoBehaviour
{
    [SerializeField] private Collider[] _trigger;
    [SerializeField] private int _id;

    private IFactory _factory;

    private void Start()
    {
        _factory = GetComponent<IFactory>();
    }

    private void OnTriggerEnter(Collider block)
    {
        if (_trigger[_id].TryGetComponent(out Player player))
        {
            //_factory.Spawn(_id);
        }
    }
}
