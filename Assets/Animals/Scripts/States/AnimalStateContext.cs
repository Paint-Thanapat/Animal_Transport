using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStateContext : MonoBehaviour
{
    public IAnimalState currentState { get; set; }
    private readonly AnimalAI _animalAI;

    public AnimalStateContext(AnimalAI animalAI)
    {
        _animalAI = animalAI;
    }

    public void Transition()
    {
        currentState.Handle(_animalAI);
    }

    public void Transition(IAnimalState state)
    {
        currentState = state;
        currentState.Handle(_animalAI);
    }
}