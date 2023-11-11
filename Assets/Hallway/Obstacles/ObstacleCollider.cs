using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<TripControl>() != null) {
            SendMessageUpwards("PlayerCollision");
            other.GetComponent<TripControl>().Trip();
        }
    }
}
