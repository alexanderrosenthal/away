using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public int timeInSeconds = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsInvoking("UpdateTime"))
        {
            InvokeRepeating("UpdateTime", 0f, 1f);
        }
    }

    // Update is called once per frame
    private void UpdateTime()
    {
        timeInSeconds++;

        // Berechne Minuten und Sekunden
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Formatiere die Zeit im Format "M:SS"
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);

    }
    public void StartUpdateTime()
    {
        if (!IsInvoking("UpdateTime"))
        {
            InvokeRepeating("UpdateTime", 0f, 1f);
        }
    }

    public void StopUpdateTime()
    {
        CancelInvoke("UpdateTime");
    }
}
