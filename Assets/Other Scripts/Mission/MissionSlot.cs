using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionSlot : MonoBehaviour
{
    public MissionReqiurement missionReqiurement;

    public Image[] imageMissionSlot;
    public bool[] isAllComplete;
    public Slider showTimeSlider;
    private float missionTime;

    void Start()
    {
        SetShowRequirement();

        missionTime = missionReqiurement.missionDuration;
    }

    void Update()
    {

        if (missionTime > 0)
        {
            missionTime -= Time.deltaTime;
            showTimeSlider.value = missionTime / missionReqiurement.missionDuration;
        }
        else
        {
            GetComponentInParent<MissionController>().currentMission--;
            Destroy(gameObject);
        }
    }

    public void SetShowRequirement()
    {
        for (int i = 0; i < imageMissionSlot.Length; i++)
        {
            if (i >= missionReqiurement.missionRequirementSprite.Length)
            {
                imageMissionSlot[i].enabled = false;
                continue;
            }

            imageMissionSlot[i].sprite = missionReqiurement.missionRequirementSprite[i];
        }

        isAllComplete = new bool[missionReqiurement.missionRequirementSprite.Length];
    }

    public void CheckAllComplete()
    {
        int check = 0;

        for (int i = 0; i < isAllComplete.Length; i++)
        {
            if (isAllComplete[i])
            {
                check++;
            }
        }

        if (check == isAllComplete.Length)
        {
            //add score
            GameObject.Find("Score Manager").GetComponent<ScoreManager>().Addscore(missionReqiurement.scorePoint);
            //
            GetComponentInParent<MissionController>().currentMission--;

            //
            Destroy(gameObject);
        }
    }
}
