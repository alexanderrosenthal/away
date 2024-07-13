using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("UpdateTime", 0f, 1f);
    }

    // Update is called once per frame
    private void UpdateTime()
    {
        time++;
        timerText.text = "Time: " + time;
    }
    public void StartUpdateTime()
    {
        InvokeRepeating("UpdateTime", 0f, 1f);
    }
    
    public void StopUpdateTime()
    {
        CancelInvoke("UpdateTime");
    }
}
