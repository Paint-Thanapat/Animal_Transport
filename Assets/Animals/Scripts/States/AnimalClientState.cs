using UnityEngine;

public class AnimalClientState : MonoBehaviour
{
    private AnimalAI _animalAI;
    void Start()
    {
        //need to keep data "PlayerController" then we use "(PlayerController)" for this
        _animalAI = (AnimalAI)FindObjectOfType(typeof(AnimalAI));
    }
}
