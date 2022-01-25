using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Menegment_UI : MonoBehaviour
{
    private bool gameStart;

    private Player player;
    private GetMedal getMedal;

    [SerializeField] private Canvas pauseScreen;
    [SerializeField] private Canvas finishScreen;
    [SerializeField] private Canvas startScreen;

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

        if ((Input.GetKeyDown(KeyCode.Escape) && !pauseScreen.isActiveAndEnabled && !finishScreen.isActiveAndEnabled) || player.isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.isActiveAndEnabled && !player.isDead)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
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
}
