using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SoundEffectCatcher : MonoBehaviour
{
    //HasSoundEffect finds this script through the Audio Tag on the Sound Manager
    //HasSoundEffect adds the AudioSource component to the list in order to track all of the sound effects
    //This allows for the ability to also change the level of the effects as needed.


    //May be better handled through Event System.
    //public UnityEvent<float> VolumeAdusted; 

    private List<AudioSource> seAudioSources;
    private VolumeControl volumeControl;

    private void Awake() {
        seAudioSources = new List<AudioSource>();
        volumeControl = GetComponent<VolumeControl>();
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode) {
        seAudioSources = new List<AudioSource>();
    }

    public void AddSEAudioSource(AudioSource newAudioSource) {
        seAudioSources.Add(newAudioSource);
        newAudioSource.volume = ((float)volumeControl.SEVolume / 100f);
    }

    public void AdjustSEVolume(float newVolume) {
        foreach(AudioSource audioSource in seAudioSources) {
            if(audioSource != null) {
                audioSource.volume = ((float)newVolume) / 100f;
            }
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
