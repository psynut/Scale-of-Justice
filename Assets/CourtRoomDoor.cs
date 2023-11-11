using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtRoomDoor : MonoBehaviour
{
    [SerializeField]
    private Transform highlighterSphere;
    [SerializeField]
    private float highlightSpinSpeed = 4f;

    private bool highlighted = false;

    // Start is called before the first frame update
    void Start()
    {
        highlighterSphere.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(highlighted) {
            highlighterSphere.Rotate(Vector3.up * highlightSpinSpeed * Time.deltaTime);
        }   
    }

    public void Highlight(bool m_bool) {
        highlighted = m_bool;
        highlighterSphere.gameObject.SetActive(m_bool);
    }
}
