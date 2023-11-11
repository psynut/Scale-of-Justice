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

    private CourtRoomDoor[] courtRoomDoors;
    private CourtRoomDoor goal;

    const string characters = "ABCDEF1234567890";

    // Start is called before the first frame update
    void Start()
    {
        courtRoomDoors = FindObjectsOfType<CourtRoomDoor>();
        StartMission();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMission() {
        goal = courtRoomDoors[Random.Range(0,courtRoomDoors.Length)];
        string m_string = string.Empty;
        for(int i = 0; i < 8; i++) {
            m_string += characters[Random.Range(0,characters.Length)];
        }
        caseDisplay.text = m_string;
        goal.Highlight(true);
        goalDisplay.text = goal.name.Remove(0,17);
        
    }
}
