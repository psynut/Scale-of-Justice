using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateText : MonoBehaviour
{
    private
    TMP_Text text;

    private void Awake() {
        text = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = System.DateTime.Now.ToShortDateString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
