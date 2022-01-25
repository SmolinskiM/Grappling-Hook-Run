using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select_Level : MonoBehaviour
{
    private Menu menu;
    
    private Level[] level;

    [SerializeField] private Image medal;

    [SerializeField] private Text timeBest;
    [SerializeField] private Text timeGold;
    [SerializeField] private Text timeSilver;

    private void Awake()
    {
        menu = GetComponent<Menu>();
        level = Resources.LoadAll<Level>("");
        //Debug.LogError(level[menu.level - 1].bestTime);
    }

    public void LevelChange()
    {  
        if(level[menu.level - 1].bestTime == 0)
        {
            timeBest.text = "--.-";
            medal.color = Color.white;
        }
        else
        {
            timeBest.text = level[menu.level - 1].bestTime.ToString("f1");
            if (level[menu.level - 1].bestTime <= level[menu.level - 1].medalGold)
            {
                medal.color = Color.yellow;
            }
            else if (level[menu.level - 1].bestTime <= level[menu.level - 1].medalSilver)
            {
                medal.color = new Color((float)192 / 255, (float)192 / 255, (float)192 / 255);
            }
            else
            {
                medal.color = new Color((float)150 / 255, (float)75 / 255, 0);
            }
        }

        timeGold.text = level[menu.level - 1].medalGold.ToString("f1");
        timeSilver.text = level[menu.level - 1].medalSilver.ToString("f1");

    }

    public void Start_Level()
    {
        if (menu.level == 1 || level[menu.level - 2].bestTime != 0)
        {
            SceneManager.LoadScene("Level_" + menu.level);
        }
    }
}
