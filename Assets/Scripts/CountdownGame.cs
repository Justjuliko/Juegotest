using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TMP_Text countdownTimer;
    public GameObject clickableObj;
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdownTimer.text = (timeRemaining.ToString()+" seconds");
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                Destroy(clickableObj);
                countdownTimer.text = ("Time has run out!");
            }
        }

    }
}
