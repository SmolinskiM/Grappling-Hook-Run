using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Menegment : MonoBehaviour
{
    public Level level;
    public Canvas pause_screen;
    public Canvas finish_screen;
    public Canvas start_screen;
    public Player player;
    public Text time;
    public Text time_end;
    public Text time_gold;
    public Text time_silver;
    public Image medal;
    float start_time;
    float time_current;
    bool game_start;
    bool dead;

    void Start()
    {
        game_start = false;
        start_time = Time.time;
        Time.timeScale = 0;
        time_gold.text = level.medal_gold.ToString();
        time_silver.text = level.medal_silver.ToString();
    }

    void Update()
    {
        if(!game_start)
        {
            Start_Level();
        }

        time_current = Time.time - start_time;

        time.text = time_current.ToString("f1");

        if (Input.GetKey("r"))
        {
            Restart();
        }

        if(player.finish)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            End_Game();
            finish_screen.gameObject.SetActive(true);
        }

        if((Input.GetKeyDown(KeyCode.Escape) && !pause_screen.isActiveAndEnabled && !finish_screen.isActiveAndEnabled) || player.dead)
        {
            Cursor.lockState = CursorLockMode.None;
            pause_screen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pause_screen.isActiveAndEnabled && !dead && !player.dead)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pause_screen.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            dead = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pause_screen.gameObject.SetActive(true);
        }
    }

    void Start_Level()
    {
        if(Input.anyKeyDown)
        {
            start_screen.gameObject.SetActive(false);
            game_start = true;
            
            if(!Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
            }
        }
    }

    public void Restart()
    {
        player.finish = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void End_Game()
    {
        if(time_current < level.medal_gold)
        {
            medal.color = Color.yellow;
        }
        else if(time_current < level.medal_silver)
        {
            medal.color = new Color((float)192 / 255, (float)192 / 255, (float)192 / 255);
        }
        else if(time_current > level.medal_silver)
        {
            medal.color = new Color((float)150/ 255, (float)75 / 255, 0);
        }

        time_end.text = time_current.ToString("f1");

        if(time_current < level.best_time || level.best_time == 0)
        {
            level.best_time = time_current;
            Debug.LogError(level.best_time);
            Debug.LogError(time_current);
        }
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Next_Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
