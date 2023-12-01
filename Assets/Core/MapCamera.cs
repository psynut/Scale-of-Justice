using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MapCamera : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private int level = 1;

    private Camera m_camera;

    private void Awake() {
        m_camera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeMapLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.y < 2f) {

            if(level != 1) {
                level = 1;
                ChangeMapLevel();
            }
        } else if(player.position.y < 4.6f) {

            if(level != 2) {
                level = 2;
                ChangeMapLevel();
            }

        } else{
            if(level != 3) {
                Debug.Log($"Player position @ {player.position.y} and level was {level} now it's 3");
                level = 3;
                ChangeMapLevel();
            }
        }
    }

    private void ChangeMapLevel() {
        switch(level) {
            case 1:
                m_camera.nearClipPlane = 28.1f;
                m_camera.farClipPlane = 29f;
                break;
            case 2:
                m_camera.nearClipPlane = 25f;
                m_camera.farClipPlane = 27f;
                break;
            case 3:
                m_camera.nearClipPlane = 22f;
                m_camera.farClipPlane = 25f;
                break;
            default:
                Debug.Log("Implemented default");
                m_camera.nearClipPlane = 22f;
                m_camera.farClipPlane = 29f;
                break;

        }
    }
}
