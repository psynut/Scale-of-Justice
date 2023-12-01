using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HasSoundEffect))]
public class AudioPlayer : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] audioClips;
    
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySound(int auidoIndex) {
        audioSource.clip = audioClips[auidoIndex];
        audioSource.Play();
    }
}
