using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public TextMeshProUGUI timerText;
    public int score = 0;
    public int totalScore = 0;
    
    public int intTime = 100;

    void Start()
    {
        intTime = 100 - (int)Time.realtimeSinceStartup;
    }
    // Update is called once per frame
    void Update()
    {
        if(intTime == 0) 
        {
            Debug.Log("Time's up!");
            timerText.text = "Time's up! You Lose!";

        } 
        else 
        {
            intTime = 100 - (int)Time.realtimeSinceStartup;
            string timeStr = $"MARIO                      WORLD               Time: \n{totalScore}                     x{score}         1-1                     {intTime}";
            timerText.text = timeStr;

        }
    }
}
