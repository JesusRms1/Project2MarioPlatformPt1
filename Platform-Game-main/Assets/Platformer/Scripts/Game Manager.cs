using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
  ///  public TextMeshProUGUI worldText;
  //  public TextMeshProUGUI cointText;
  //  public TextMeshProUGUI marioText;

   public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frrame
    void Update()
    {
        int intTime = 400 - (int)Time.realtimeSinceStartup;
        string timeStr = $"MARIO                      WORLD               Time: \n000000           x{score}         1-1                     {intTime}";
        timerText.text = timeStr;

    }
}
