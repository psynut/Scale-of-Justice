using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.buildIndex == 0) {
            StartCoroutine(LoadNextAfterSeconds(5));
        }
    }

    private IEnumerator LoadNextAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        LoadNextScene();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
