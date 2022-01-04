using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select_Level : MonoBehaviour
{
    public Menu menu;
    public Level[] level;

    public Image medal;
    public Text time_best;
    public Text time_gold;
    public Text time_silver;

    private void Start()
    {
        menu = GetComponent<Menu>();
        level = Resources.LoadAll<Level>("");
        //Debug.LogError(level[menu.level - 1].best_time);
    }

    private void Update()
    {  
        if(level[menu.level - 1].best_time == 0)
        {
            time_best.text = "--.-";
            medal.color = Color.white;
        }
        else
        {
            time_best.text = level[menu.level - 1].best_time.ToString("f1");
            if (level[menu.level - 1].best_time <= level[menu.level - 1].medal_gold)
            {
                medal.color = Color.yellow;
            }
            else if (level[menu.level - 1].best_time <= level[menu.level - 1].medal_silver)
            {
                medal.color = new Color((float)192 / 255, (float)192 / 255, (float)192 / 255);
            }
            else
            {
                medal.color = new Color((float)150 / 255, (float)75 / 255, 0);
            }
        }

        time_gold.text = level[menu.level - 1].medal_gold.ToString("f1");
        time_silver.text = level[menu.level - 1].medal_silver.ToString("f1");

    }

    public void Start_Level()
    {
        if (menu.level == 1 || level[menu.level - 2].best_time != 0)
        {
            SceneManager.LoadScene("Level_" + menu.level);
        }
    }
}
