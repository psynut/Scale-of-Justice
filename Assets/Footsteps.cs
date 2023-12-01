using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private float distanceCheck;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip footstep00, footstep01, footstep02;
    [SerializeField]
    private Vector3 lastPosition;

    private bool flipOddFootstep = true;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        StartCoroutine(CheckDistance());
    }

    private void SetNextFootstepClip() {
        if(audioSource.clip == null || (audioSource.clip == footstep01 && flipOddFootstep == true)) {
            audioSource.clip = footstep00;
            audioSource.panStereo = -.5f;
        } else if(audioSource.clip == footstep00 || audioSource.clip == footstep02) {
            audioSource.clip = footstep01;
            flipOddFootstep = !flipOddFootstep;
            audioSource.panStereo = .5f;
        } else {
            audioSource.clip = footstep02;
            audioSource.panStereo = -.5f;
        }
    }

    private IEnumerator CheckDistance() {
        yield return new WaitUntil(()=>Vector3.Distance(lastPosition,transform.position) >= distanceCheck);
        lastPosition = transform.position;
        SetNextFootstepClip();
        audioSource.Play();
        StartCoroutine(CheckDistance());
    }
}
