using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalWanderState : MonoBehaviour, IAnimalState
{
    private AnimalAI _animalAI;
    public void Handle(AnimalAI animalAI)
    {
        if (!_animalAI)
        {
            _animalAI = animalAI;
        }

        _animalAI.nearByPlayer = false;
        _animalAI.playerNearBy = null;
        _animalAI.navMeshAgent.speed = _animalAI.normalSpeed;

        _animalAI.navMeshAgent.enabled = true;
        _animalAI.col.enabled = true;
    }
}
