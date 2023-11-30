using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CourtroomGameManager : MonoBehaviour
{
    private enum MiniGame {Tangram, Memory}
    MiniGame m_game;

    [SerializeField]
    private Animator cameraAnimator;
    [SerializeField]
    private Animator uIAnimator;
    [SerializeField]
    private TMP_Text addedScoreText;
    [SerializeField]
    private TangramCatcher tangramCatcher;
    [SerializeField]
    private MemoryGame memoryGame;

    [SerializeField]
    private GameObject[] miniGames;

    private Timer timer;

    private void Awake() {
        timer = GetComponent<Timer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.MissionTally += 1;
        m_game = (MiniGame)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MiniGame)).Length);
        Debug.LogWarning("Return here to make this random selection for minigame!");
        miniGames[(int)m_game].SetActive(true);
        cameraAnimator.SetTrigger(m_game.ToString());
        uIAnimator.SetTrigger(m_game.ToString());
        timer.StartTimer(200-(15*ScoreManager.MissionTally));
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
