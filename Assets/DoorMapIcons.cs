using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMapIcons : MonoBehaviour
{
    [SerializeField]
    private Transform[] aboveIcons;
    [SerializeField]
    private Transform[] belowIcons;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform icon in aboveIcons) {
            icon.rotation = Quaternion.Euler(new Vector3(90f,0,0));    
        }
        foreach(Transform icon in belowIcons) {
            icon.rotation = Quaternion.Euler(new Vector3(270f,0,0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
