using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int timer;
    public int hurryTime;
    public int countdownTimer;
    public Text countdownTimerText;
    public int scorePoint;
    public Text scoreText;
    public GameObject plusScorePrefab;
    public GameObject timeUp;
    public Text summaryScoreText;

    Animator anim;
    void Start()
    {
        scorePoint = 0;
        scoreText.text = scorePoint.ToString();

        timeUp.SetActive(false);

        StartCoroutine(StartTimer());

        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Addscore(int score)
    {
        scorePoint += score;
        scoreText.text = scorePoint.ToString();

        GameObject plusScoreClone = Instantiate(plusScorePrefab, scoreText.gameObject.transform);
        plusScoreClone.GetComponentInChildren<Text>().text = "+" + score;
        Destroy(plusScoreClone, 3f);
    }

    IEnumerator StartTimer()
    {
        countdownTimer = timer;

        while (countdownTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTimer--;
            countdownTimerText.text = countdownTimer.ToString();

            if (countdownTimer <= hurryTime)
            {
                BGMusicManager.Instance.RemoveSound(BGMusicManager.Instance.mainmenuSource);
                BGMusicManager.Instance.PlaySound(BGMusicManager.Instance.gameplaySource, BGMusicManager.Instance.gameplayHurryClip);
            }
        }

        timeUp.SetActive(true);
        summaryScoreText.text = "Your Score is : " + scorePoint.ToString();

        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        playerController._playerStateContext.Transition(playerController._stopMovementState);

        yield return new WaitForSeconds(3f);

        anim.enabled = true;
    }
}
