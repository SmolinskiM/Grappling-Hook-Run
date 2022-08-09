using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenegmentUI : MonoBehaviour
{

    [SerializeField] private Canvas pauseScreen;
    [SerializeField] private Canvas finishScreen;
    [SerializeField] private Canvas startScreen;
    
    private bool gameStart;

    private Player player;
    private GetMedal getMedal;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        getMedal = GetComponent<GetMedal>();
        gameStart = false;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (!gameStart)
        {
            StartLevel();
        }

        if (player.isFinished)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            getMedal.EndGame();
            finishScreen.gameObject.SetActive(true);
        }
        if(pauseScreen.isActiveAndEnabled)
        {
            SetPauseScreenOff();
        }
        else
        {
            SetPauseScreenOn();
        }
    }

    void StartLevel()
    {
        if (Input.anyKeyDown)
        {
            startScreen.gameObject.SetActive(false);
            gameStart = true;

            if (!Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
            }
        }
    }

    void SetPauseScreenOn()
    {
        if(player.isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
            return;
        }

        if(!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }

        if(finishScreen.isActiveAndEnabled)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.None;
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void SetPauseScreenOff()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }

        if(player.isDead)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
