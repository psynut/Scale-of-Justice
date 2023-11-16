using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HallManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text goalDisplay;
    [SerializeField]
    private TMP_Text caseDisplay;
    [SerializeField]
    private TMP_Text scoreDisplay;
    [SerializeField]
    Animator canvasAnimator;


    [SerializeField]
    [Tooltip("Time for each mission will be calculated by distance * MissionTimeFactor / # of missions")]
    private float missionTimeFactor = 1f;
    [SerializeField]
    private int chanceForMiniGame = 0;

    private PlayerMovement player;
    private Timer timer;
    private CourtRoomDoor[] courtRoomDoors;
    private CourtRoomDoor goal;
    private Vector3 previousGoalVec3;

    const string characters = "ABCDEF1234567890";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        timer = GetComponent<Timer>();
        if(ScoreManager.Score == 0 || ScoreManager.Score == null) {
            scoreDisplay.text = "000";
        } else {
            scoreDisplay.text = ((int)(ScoreManager.Score)).ToString();
        }
        courtRoomDoors = FindObjectsOfType<CourtRoomDoor>();
        foreach(CourtRoomDoor courtRoomDoor in courtRoomDoors) {
            courtRoomDoor.MissionComplete.AddListener(MissionComplete);
        }
        StartMission();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMission() {
        ScoreManager.MissionTally += 1;
        if(ScoreManager.MissionTally == 1) {
            previousGoalVec3 = Vector3.zero;
        } else {
            previousGoalVec3 = goal.transform.position;
        }
        goal = courtRoomDoors[Random.Range(0,courtRoomDoors.Length)];
        float missionDistance = Vector3.Distance(goal.transform.position,previousGoalVec3);
        float countdownTime = (missionDistance * missionTimeFactor / ScoreManager.MissionTally);
        timer.StartTimer(countdownTime);
        string m_string = string.Empty;
        for(int i = 0; i < 8; i++) {
            m_string += characters[Random.Range(0,characters.Length)];
        }
        goal.Highlight(true);
        if(ScoreManager.MissionTally > 1) {
            canvasAnimator.SetTrigger("NextCase");

        } else {
            canvasAnimator.SetTrigger("FirstNextCase");
        }

        caseDisplay.text = m_string;
        goalDisplay.text = goal.name.Remove(0,17);  
    }

    public void MissionComplete() {
        TallyScore();
        int rnd = Random.Range(0,chanceForMiniGame);
        if(rnd == 1) {
            StartMiniGame();
        } else {
            StartMission();
        }
    }

    public void StartMiniGame() {
        //TODO
    }

    public void TallyScore() {
        float distance = Vector3.Distance(goal.transform.position, previousGoalVec3);
        ScoreManager.ChangeScore((timer.RemainingTime() + distance) * ScoreManager.MissionTally / 3);
        scoreDisplay.text = ((int)(ScoreManager.Score)).ToString();
    }

    public void OutOfTime() {
        Destroy(player);
        GetComponent<EndGameProcess>().LoadPanel();
    }
}