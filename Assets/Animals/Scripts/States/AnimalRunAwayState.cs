using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRunAwayState : MonoBehaviour, IAnimalState
{
    private AnimalAI _animalAI;
    public void Handle(AnimalAI animalAI)
    {
        if (!_animalAI)
        {
            _animalAI = animalAI;
        }

        _animalAI.nearByPlayer = true;

        _animalAI.onHolding = false;
        _animalAI.navMeshAgent.enabled = true;
        _animalAI.col.enabled = true;

        _animalAI.navMeshAgent.speed = _animalAI.nearByPlayerSpeed;
    }

    void Update()
    {
        if (_animalAI && _animalAI.playerNearBy != null && !_animalAI.onHolding)
        {
            Vector3 direction = (gameObject.transform.position - _animalAI.playerNearBy.transform.position).normalized;
            Vector3 pos = direction * _animalAI.checkRadius + gameObject.transform.position;

            _animalAI.navMeshAgent.SetDestination(pos);

            float distance = Vector3.Distance(transform.position, _animalAI.playerNearBy.transform.position);

            if (distance > _animalAI.checkRadius + 1)
            {
                _animalAI._animalStateContext.Transition(_animalAI._wanderState);
            }
        }
    }
}
