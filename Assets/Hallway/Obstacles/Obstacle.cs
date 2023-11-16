using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private ParticleSystem[] particleSystems;

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
        foreach(ParticleSystem psystem in particleSystems) {
            psystem.Play();
        }
    }
}
