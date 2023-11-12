using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasSoundEffect : MonoBehaviour
{
    private SoundEffectCatcher soundEffectCatcher;
    private AudioSource audioSource;

    private void Awake() {
        if(!TryGetComponent<AudioSource>(out audioSource)) {
            Debug.LogWarning($"HasSoundEffect component added to {this}, but no audiosource was found");
        }
    }

    private void Start() {
        GameObject m_GameObject = GameObject.FindWithTag("Audio");
        if(m_GameObject != null) {
            if(m_GameObject.TryGetComponent<SoundEffectCatcher>(out soundEffectCatcher)) {
                if(audioSource) {
                    soundEffectCatcher.AddSEAudioSource(audioSource);
            } else {
                Debug.LogWarning($"{this} has HasSoundeffect component attached, but no AudioSource was found to send to SoundEffectCatcher");
            }
        } else {
                Debug.LogWarning("No SoundEffectCatcher Found");
            }
        } else {
            Debug.LogWarning("No SoundEffectCatcher Found");
        }


        //May be more efficient, less time costly, etc., but remain skitish about using static instance
        //soundEffectCatcher = SoundManager.Instance.GetComponent<SoundEffectCatcher>();

        //May be better handled through Event System.
    }

}
