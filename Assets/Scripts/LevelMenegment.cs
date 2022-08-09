using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenegment : MonoBehaviour
{

    [SerializeField] private Button goNextLevel;
    [SerializeField] private Button goHomePause;
    [SerializeField] private Button restartPause;
    [SerializeField] private Button goHomeFinish;
    [SerializeField] private Button restartFinish;
    
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        goNextLevel.onClick.AddListener(GoNextLevel);
        goHomePause.onClick.AddListener(GoHome);
        restartPause.onClick.AddListener(Restart);
        goHomeFinish.onClick.AddListener(GoHome);
        restartFinish.onClick.AddListener(Restart);
    }

    void Update()
    {
        if (Input.GetKey("r"))
        {
            Restart();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.isDead = true;
        }
    }

    public void Restart()
    {
        player.isFinished = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
