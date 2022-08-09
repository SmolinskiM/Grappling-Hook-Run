using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMedal : MonoBehaviour
{
    
    [SerializeField] private Image medal;

    [SerializeField] private Level level;

    [SerializeField] private Text time;
    [SerializeField] private Text timeEnd;
    [SerializeField] private Text timeGold;
    [SerializeField] private Text timeSilver;

    private float startTime;
    private float timeCurrent;

    void Start()
    {
        startTime = Time.time;
        timeGold.text = level.medalGold.ToString();
        timeSilver.text = level.medalSilver.ToString();
    }

    void Update()
    {
        timeCurrent = Time.time - startTime;
        time.text = timeCurrent.ToString("f1");
    }

    public void EndGame()
    {
        if (timeCurrent < level.medalGold)
        {
            medal.color = Color.yellow;
        }
        else if (timeCurrent < level.medalSilver)
        {
            medal.color = new Color((float)192 / 255, (float)192 / 255, (float)192 / 255);
        }
        else if (timeCurrent > level.medalSilver)
        {
            medal.color = new Color((float)150 / 255, (float)75 / 255, 0);
        }

        timeEnd.text = timeCurrent.ToString("f1");

        if (timeCurrent < BestTimeSaveManager.Instance.bestTime.bestTime[level.levelNr-1] || BestTimeSaveManager.Instance.bestTime.bestTime[level.levelNr-1] == 0)
        {
            BestTimeSaveManager.Instance.SaveBestTime(timeCurrent, level.levelNr-1);
        }
    }
}
