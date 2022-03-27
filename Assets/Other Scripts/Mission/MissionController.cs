using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MissionController : MonoBehaviour
{
    [Header("Setting Before Start")]
    public GameObject missionBoard;
    public GameObject missionSlot;
    List<MissionSlot> scriptMissionSlots = new List<MissionSlot>();
    public MissionReqiurement[] allMissionRequirement;

    [Header("Setting On Runtime")]
    public int maxMission;
    public int currentMission = 0;
    public Vector2 minMaxRandomNewMission;


    void Start()
    {
        StartCoroutine(RandomNewMission());
    }


    public void AssignMissionSlot()
    {
        int randomRequirement = Random.Range(0, allMissionRequirement.Length);

        GameObject slotClone = Instantiate(missionSlot, missionBoard.transform);

        slotClone.GetComponent<MissionSlot>().missionReqiurement = allMissionRequirement[randomRequirement];

        scriptMissionSlots.Add(slotClone.GetComponent<MissionSlot>());
    }

    public void CheckSendAnimal(Sprite animalIcon)
    {
        for (int i = 0; i < (int)scriptMissionSlots.Count; i++)
        {
            if (scriptMissionSlots[i] == null)
                continue;

            //do stuff
            for (int j = 0; j < scriptMissionSlots[i].missionReqiurement.missionRequirementSprite.Length; j++)
            {
                if (animalIcon == scriptMissionSlots[i].imageMissionSlot[j].sprite)
                {
                    scriptMissionSlots[i].imageMissionSlot[j].sprite = GameManager.Instance.ChangeAnimalIconToComplete(animalIcon);

                    scriptMissionSlots[i].isAllComplete[j] = true;
                    scriptMissionSlots[i].CheckAllComplete();
                    return;
                }
            }
            //load screen
        }
    }

    public IEnumerator RandomNewMission()
    {
        while (true)
        {
            if (currentMission < maxMission)
            {
                AssignMissionSlot();
                currentMission++;
            }

            yield return new WaitForSeconds(Random.Range(minMaxRandomNewMission.x, minMaxRandomNewMission.y));

        }
    }
}
