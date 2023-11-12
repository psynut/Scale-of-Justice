using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static float Score {get; private set;}
    public static int MissionTally { get; set;}

    public static ScoreManager Instance {get; private set;}
    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public static void ChangeScore(float value) {
        Score += value;
    }
}
