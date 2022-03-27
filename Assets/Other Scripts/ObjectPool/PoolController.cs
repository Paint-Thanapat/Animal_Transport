using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolController : MonoBehaviour
{
    public int maxPoolSize = 6;
    public int stackDefaultCapacity = 6;

    [HideInInspector] public GameObject objectToPool;
    private IObjectPool<AnimalPool> objectPool;

    public IObjectPool<AnimalPool> Pool
    {
        get
        {
            if (objectPool == null)
            {
                objectPool = new ObjectPool<AnimalPool>(CreatedPoolItem,
                                                  OnTakeFormPool,
                                                  OnReturnToPool,
                                                  OnDestroyPool,
                                                  true,
                                                  stackDefaultCapacity,
                                                  maxPoolSize);
            }

            return objectPool;
        }
        set
        {

        }
    }



    private AnimalPool CreatedPoolItem()
    {
        GameObject go = Instantiate(objectToPool);

        AnimalPool obj = go.GetComponent<AnimalPool>();

        obj.objectPool = Pool;

        return obj;
    }

    void OnTakeFormPool(AnimalPool obj)
    {
        obj.gameObject.SetActive(true);
    }

    void OnReturnToPool(AnimalPool obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPool(AnimalPool obj)
    {
        Destroy(obj.gameObject);
    }

    public void Spawn(Transform[] spawnPoint)
    {
        var amount = UnityEngine.Random.Range(1, 10);

        for (int i = 0; i < amount; i++)
        {
            var obj = Pool.Get();

            obj.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;
        }
    }

    public void Spawn(Transform[] spawnPoint,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var obj = Pool.Get();

            obj.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;

            obj.transform.SetParent(GameObject.Find("---- Character ----").transform);
        }
    }
}
