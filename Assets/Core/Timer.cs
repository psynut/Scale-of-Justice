using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    public UnityEvent TimeUp;

    [SerializeField]
    private TMP_Text text;

    private float countDown = 0; //Will use miliseconds;
    private float startTime;

    private void Awake() {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float remainingTime = RemainingTime();
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        int miliseconds = (int)((remainingTime % 1) * 100f);

        if(remainingTime > 20) {
            if(text.color != Color.white) {
                text.color = Color.white;
            }

                text.text = $"{minutes}:{seconds}";
        } else if(remainingTime > 0) {
            if(text.color != Color.yellow) {
                text.color = Color.yellow;
            }
            text.text = $"{seconds}.{miliseconds}";
        }
        if(remainingTime < 0 && countDown !=0) {
            countDown = 0;
            text.text = "0.00";
            text.color = Color.red;
            TimeUp.Invoke();
        }
    }

    public void StartTimer(float m_time) {
        startTime = Time.time;
        countDown = m_time;
    }

    public float RemainingTime() {
        return startTime + countDown - Time.time;
    }

}
