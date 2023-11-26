using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripControl : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void Start() {

    }

    public void Trip() {
        animator.enabled = true;
        if(playerMovement != null) {            //Sometimes this is called when the End of Game process has already happened and destroys playerMovement.
            playerMovement.enabled = false;
        }
        animator.SetTrigger("Trip");
    }

    public void ReenablePlayerMovement() {
        if(playerMovement != null) {            //Sometimes this is called when the End of Game process has already happened and destroys playerMovement.
            playerMovement.enabled = true;
        }
        animator.enabled = false;
    }

}
