using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    public MusicLevelCommand[] musicLevelCommands;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    void OnSceneLoad(Scene scene,LoadSceneMode mode) {
        int buildIndex = scene.buildIndex;
        switch((int)musicLevelCommands[buildIndex].command){
            case 0:
                Stop();
                break;
            case 1:
                PlayOnce(musicLevelCommands[buildIndex].songs[0]);
                break;
            case 2:
                PlayLoop(musicLevelCommands[buildIndex].songs[0]);
                break;
            case 3:
                PlayFirstLoopSecond(musicLevelCommands[buildIndex].songs[0],musicLevelCommands[buildIndex].songs[1]);
                break;
            default:
                Debug.LogWarning("Undefined command passed to OnSceneLoad Switch statement");
                break;
                
        }
    }

    public void AdjustMusicVolume(float newVolume) {
        audioSource.volume = ((float)newVolume / 100f);
    }

    private void Stop() {
        audioSource.clip = null;
        audioSource.Stop();
    }


    private void PlayOnce(AudioClip song) {
        audioSource.loop = false;
        audioSource.clip = song;
        audioSource.Play();
    }
    private void PlayLoop(AudioClip song) {
        audioSource.loop = true;
        audioSource.clip = song;
        audioSource.Play();
    }

    //Will play the first track, then loop 2nd track
    //Make the second track the following item in the startTrack array

    private void PlayFirstLoopSecond(AudioClip song0,AudioClip song1) {
        PlayOnce(song0);
        StartCoroutine(LoopPlayDelay(song1,song0.length));
    }

    IEnumerator LoopPlayDelay(AudioClip m_song,float delay) {
        yield return new WaitForSeconds(delay);
        PlayLoop(m_song);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

}
