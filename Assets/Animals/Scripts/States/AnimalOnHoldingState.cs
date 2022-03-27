using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalOnHoldingState : MonoBehaviour, IAnimalState
{
    private AnimalAI _animalAI;
    public void Handle(AnimalAI animalAI)
    {
        if (!_animalAI)
        {
            _animalAI = animalAI;
        }

        _animalAI.onHolding = true;
        _animalAI.navMeshAgent.enabled = false;

        _animalAI.col.enabled = false;
    }
}
