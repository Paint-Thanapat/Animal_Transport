using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission Requirement", menuName = "Mission Requirement")]
public class MissionReqiurement : ScriptableObject
{
    [Range(1, 3)]
    public int missionCount = 1;

    public Sprite[] missionRequirementSprite;

    public float missionDuration = 30f;

    public int scorePoint = 20;

    void OnValidate()
    {
        if (missionRequirementSprite.Length != missionCount)
        {
            missionRequirementSprite = new Sprite[missionCount];
        }
    }
}
