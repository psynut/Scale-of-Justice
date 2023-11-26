using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CourtroomGameManager : MonoBehaviour
{
    private enum MiniGame {Tangram}
    MiniGame m_game;

    [SerializeField]
    private Animator cameraAnimator;
    [SerializeField]
    private Animator uIAnimator;
    [SerializeField]
    private TMP_Text addedScoreText;
    [SerializeField]
    private TangramCatcher tangramCatcher;

    private Timer timer;

    private void Awake() {
        timer = GetComponent<Timer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.MissionTally += 1;
        m_game = (MiniGame)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MiniGame)).Length);
        Debug.Log(m_game.ToString());
        cameraAnimator.SetTrigger(m_game.ToString());

        timer.StartTimer(200);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OutOfTime() {
        GetComponent<EndGameProcess>().LoadPanel();
    }

    public void OnMiniGameComplete() {
        float m_score = ScoreManager.MissionTally / 3f * timer.RemainingTime()*200f;
        timer.PauseTime(true);
        addedScoreText.text = $"{(int)m_score} pts.";
        uIAnimator.SetTrigger("Complete");
        ScoreManager.ChangeScore(m_score);
        StartCoroutine(ReturnToHallway());
    }

    private IEnumerator ReturnToHallway() {
        Scene_Manager sceneManager = FindObjectOfType<Scene_Manager>();
        yield return new WaitForSeconds(5f);
        sceneManager.LoadScene("Courthouse");
    }

}
