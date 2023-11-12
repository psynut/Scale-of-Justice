using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VolumeControl : MonoBehaviour
{
    const string MUSIC_VOLUME_KEY = "music_volume";
    const string SE_VOLUME_KEY = "se_volume";

    public float dialSpeed = 5f;

    private MusicPlayer musicPlayer;
    private SoundEffectCatcher soundEffectCatcher;

    private float sEAdjustValue;
    private float musicAdjustValue;

    private float musicVolume;
    public float MusicVolume {
        get {
            return musicVolume;
        }
        set {
            musicVolume = value;
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY,value);
            musicPlayer.AdjustMusicVolume(value);
            Debug.Log("MusicVolume adjusting to" + value);
        } 
    }

    private float sEVolume;
    public float SEVolume {
        get {
            return sEVolume;
        }
        set {
            sEVolume = value;
            PlayerPrefs.SetFloat(SE_VOLUME_KEY,value);
            soundEffectCatcher.AdjustSEVolume(value);
        }
    }

    private void Awake() {
        musicPlayer = GetComponent<MusicPlayer>();
        soundEffectCatcher = GetComponent<SoundEffectCatcher>();
    }

    private void Start() {
        if(Mathf.Clamp(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY),0f,100f) != PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY)) {
            MusicVolume = 50f;
        } else {
            MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
        }
        if(Mathf.Clamp(PlayerPrefs.GetFloat(SE_VOLUME_KEY),0f,100f) != PlayerPrefs.GetFloat(SE_VOLUME_KEY)) {
            SEVolume = 50f;
        } else {
            SEVolume = PlayerPrefs.GetFloat(SE_VOLUME_KEY);
        }
    }

    private void Update() {
        if(sEAdjustValue != 0) {
            SEVolume = Mathf.Clamp(SEVolume + sEAdjustValue * dialSpeed * Time.deltaTime, 0, 100f);
        }
        if(musicAdjustValue !=0) {
            MusicVolume = Mathf.Clamp(musicVolume + musicAdjustValue * dialSpeed * Time.deltaTime,0,100f);
        }
    }

    public void OnAdjustSEVolume(InputValue value) {
        sEAdjustValue = value.Get<float>();
    }

    public void OnAdjustMusicVolume(InputValue value) {
        musicAdjustValue = value.Get<float>();
    }
}
