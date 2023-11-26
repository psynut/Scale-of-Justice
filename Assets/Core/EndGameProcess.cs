using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndGameProcess : MonoBehaviour {
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Image signature;

    public void LoadPanel() {
        gameOverPanel.SetActive(true);
        scoreText.text = ((int)ScoreManager.Score).ToString();
    }


    public void ReturnToMenu() {
        ScoreManager.MissionTally = 0;
        StartCoroutine(DelayForMenu());
    }

    private IEnumerator DelayForMenu() {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
}
