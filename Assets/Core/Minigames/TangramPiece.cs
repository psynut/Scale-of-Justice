using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangramPiece : MonoBehaviour
{
    private bool touchingPiece = false;

    public bool IsTouchingPiece {
        get {
            return touchingPiece;
        }
    }

    public void Update() {
    }

    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Tangram") {
            touchingPiece = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Tangram") {
            touchingPiece = false;
        }
    }
}
