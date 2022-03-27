using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("Prefabs")]
    public GameObject Straw;

    [Header("Particle")]
    public GameObject stunParticle;

    [Header("Animal Not Complete Icon")]
    public Sprite[] allAnimalIcon;

    [Header("Animal Complete Icon")]
    public Sprite[] allAnimalCompleteIcon;
    public Sprite ChangeAnimalIconToComplete(Sprite animalIcon)
    {
        for (int i = 0; i < allAnimalIcon.Length; i++)
        {
            if (animalIcon == allAnimalIcon[i])
            {
                return allAnimalCompleteIcon[i];
            }
        }
        return null;
    }
}
