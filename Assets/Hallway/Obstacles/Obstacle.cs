using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    AudioClip[] soundEffects;

    private ParticleSystem[] particleSystems;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerCollision() {
        audioSource.clip = soundEffects[Random.Range(0,soundEffects.Length)];
        audioSource.Play();
        foreach(ParticleSystem psystem in particleSystems) {
            psystem.Play();
        }
    }
}
