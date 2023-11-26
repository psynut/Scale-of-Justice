using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBoundary : MonoBehaviour
{
    private List<string> tilesInside;

    private void Awake() {
        tilesInside = new List<string>();
    }

    public bool IsEmpty() {
        bool m_bool = false;
        if(tilesInside.Count==0) {
            m_bool = true;
        }
        return m_bool;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Tangram") {
            if(!tilesInside.Contains(other.name)) {
                tilesInside.Add(other.name);
            }
        }
        SendMessageUpwards("OnChange");
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Tangram") {
            if(tilesInside.Contains(other.name)) {
                tilesInside.Remove(other.name);
            }
        }
        SendMessageUpwards("OnChange");
    }
}
