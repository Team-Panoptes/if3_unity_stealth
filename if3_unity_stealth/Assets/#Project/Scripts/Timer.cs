using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{

    private TMP_Text timerText; 
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        timerText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.SetText($"Time : {timer:f}");
    }
}
