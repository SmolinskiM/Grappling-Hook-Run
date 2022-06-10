using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    private Menu menu;
    
    private Level[] Level;

    private Image padlock;
    
    [SerializeField] private Image medal;

    [SerializeField] private Text timeBest;
    [SerializeField] private Text timeGold;
    [SerializeField] private Text timeSilver;

    [SerializeField] private Button selectLevel;

    private void Awake()
    {
        padlock = selectLevel.GetComponent<Image>();
        menu = GetComponent<Menu>();
        Level = Resources.LoadAll<Level>("");
        selectLevel.onClick.AddListener(StartLevel);
    }

    public void LevelChange()
    {
        if(menu.Level == 1 || Level[menu.Level - 2].bestTime != 0)
        {
            padlock.color = new Vector4(255, 255, 255, 0);
        }
        else
        {
            padlock.color = new Vector4(255, 255, 255, 255);
        }

        if(Level[menu.Level - 1].bestTime == 0)
        {
            timeBest.text = "--.-";
            medal.color = Color.white;
        }
        else
        {
            timeBest.text = Level[menu.Level - 1].bestTime.ToString("f1");
            if (Level[menu.Level - 1].bestTime <= Level[menu.Level - 1].medalGold)
            {
                medal.color = Color.yellow;
            }
            else if (Level[menu.Level - 1].bestTime <= Level[menu.Level - 1].medalSilver)
            {
                medal.color = new Color((float)192 / 255, (float)192 / 255, (float)192 / 255);
            }
            else
            {
                medal.color = new Color((float)150 / 255, (float)75 / 255, 0);
            }
        }

        timeGold.text = Level[menu.Level - 1].medalGold.ToString("f1");
        timeSilver.text = Level[menu.Level - 1].medalSilver.ToString("f1");

    }

    public void StartLevel()
    {
        if (menu.Level == 1 || Level[menu.Level - 2].bestTime != 0)
        {
            SceneManager.LoadScene("Level_" + menu.Level);
        }
    }
}
