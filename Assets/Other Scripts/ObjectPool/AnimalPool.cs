using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class AnimalPool : MonoBehaviour
{
    public IObjectPool<AnimalPool> objectPool { get; set; }

    private AnimalAI _animalAI;

    public void SentAnimalToTruck()
    {
        ReturnToPool();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SentAnimalToTruck();
        }
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        //reset the object back to its initial state before returning it to the pool
        ResetObject();
    }

    void ResetObject()
    {
        //reset health

        //reset animation

        //reset target = null

        //reset

    }

    public void ReturnToPool()
    {
        objectPool.Release(this);
    }


}
