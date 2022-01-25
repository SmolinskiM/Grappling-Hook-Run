using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int level { get; private set;}

    private readonly int levelInRow = 5;
    private readonly int levelInCollum = 2;

    private Select_Level select_Level;

    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private Button buttonUp;
    [SerializeField] private Button buttonDown;

    [SerializeField] private Transform selected;

    private void Start()
    {
        select_Level = GetComponent<Select_Level>();
        level = 1;
        Time.timeScale = 1;
        select_Level.LevelChange();
        buttonLeft.onClick.AddListener(Left);
        buttonRight.onClick.AddListener(Right);
        buttonUp.onClick.AddListener(Up);
        buttonDown.onClick.AddListener(Down);
    }

    public void Left()
    {
        if(level % levelInRow != 1)
        {
            level--;
            selected.transform.Translate(-3, 0, 0);
            select_Level.LevelChange();
        }
    }

    public void Right()
    {
        if (level % levelInRow != 0)
        {
            level++;
            selected.transform.Translate(3, 0, 0);
            select_Level.LevelChange();
        }
    }

    public void Up()
    {
        if (level > levelInRow)
        {
            level -= 5;
            selected.transform.Translate(0, 3, 0);
            select_Level.LevelChange();
        }
    }

    public void Down()
    {
        if(level + levelInRow <= levelInRow * levelInCollum)
        {
            level += 5;
            selected.transform.Translate(0, -3, 0);
            select_Level.LevelChange();
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, selected.position, 10 * Time.deltaTime);
    }
}
