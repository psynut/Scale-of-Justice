using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomLabel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        if(this.name.Length > 17) {
            string roomNumber = "Rm." + this.name.Remove(0,17);
            text.text = roomNumber;
        }
    }
}
