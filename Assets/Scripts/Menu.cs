using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private Button buttonUp;
    [SerializeField] private Button buttonDown;

    private int level;
    private readonly int levelInRow = 5;
    private readonly int levelInCollum = 2;

    [SerializeField] private Transform _camera;
    private SelectLevel selectLevel;


    [SerializeField] private Transform selected;
    
    public int Level { get { return level; } }

    private void Start()
    {
        selectLevel = GetComponent<SelectLevel>();
        level = 1;
        Time.timeScale = 1;
        selectLevel.LevelChange();
        buttonLeft.onClick.AddListener(Left);
        buttonRight.onClick.AddListener(Right);
        buttonUp.onClick.AddListener(Up);
        buttonDown.onClick.AddListener(Down);
    }

    public void Left()
    {
        if(Level % levelInRow != 1)
        {
            level--;
            selected.transform.Translate(-3, 0, 0);
            selectLevel.LevelChange();
        }
    }

    public void Right()
    {
        if (Level % levelInRow != 0)
        {
            level++;
            selected.transform.Translate(3, 0, 0);
            selectLevel.LevelChange();
        }
    }

    public void Up()
    {
        if (level > levelInRow)
        {
            level -= 5;
            selected.transform.Translate(0, 3, 0);
            selectLevel.LevelChange();
        }
    }

    public void Down()
    {
        if(level + levelInRow <= levelInRow * levelInCollum)
        {
            level += 5;
            selected.transform.Translate(0, -3, 0);
            selectLevel.LevelChange();
        }
    }

    private void Update()
    {
        _camera.position = Vector3.MoveTowards(_camera.transform.position, selected.transform.position, 10 * Time.deltaTime);
    }
}
