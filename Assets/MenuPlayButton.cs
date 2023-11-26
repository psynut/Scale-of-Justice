using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayButton : MonoBehaviour
{
    public void OnClick() {
        FindObjectOfType<Scene_Manager>().LoadNextScene();
    }
}
