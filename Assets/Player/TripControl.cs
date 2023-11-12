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
        playerMovement.enabled = false;
        animator.SetTrigger("Trip");
        Debug.Log("Tripped");
    }

    public void ReenablePlayerMovement() {
        playerMovement.enabled = true;
        Debug.Log("Player Standing");
        animator.enabled = false;
    }

}
