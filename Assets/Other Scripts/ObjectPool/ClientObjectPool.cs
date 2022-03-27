using UnityEngine;

public class ClientObjectPool : MonoBehaviour
{
    private PoolController _pool;
    public GameObject objectToPool;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private GameObject[] currentObjectToPool;
    void Start()
    {
        _pool = gameObject.AddComponent<PoolController>();
        _pool.objectToPool = objectToPool;

        spawnPoint = new Transform[gameObject.transform.childCount];
        spawnPoint = GetComponentsInChildren<Transform>();

        Spawn(_pool.maxPoolSize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            Spawn(_pool.maxPoolSize);
        }
    }

    public void Spawn(int count)
    {
        _pool.Spawn(spawnPoint, count);
    }
}
