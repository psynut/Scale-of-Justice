using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripControl : MonoBehaviour
{
    [SerializeField]
    private float movementPauseTime = 2.5f;

    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Start() {

    }

    public void Trip() {
        Debug.Log("Trip called");
        if(playerMovement != null) {            //Sometimes this is called when the End of Game process has already happened and destroys playerMovement.
            playerMovement.enabled = false;
        }
        animator.SetTrigger("Trip");
        Invoke(nameof(ReenablePlayerMovement),movementPauseTime);
    }



    public void ReenablePlayerMovement() {
        if(playerMovement != null) {            //Sometimes this is called when the End of Game process has already happened and destroys playerMovement.
            playerMovement.enabled = true;
        }
    }

}
