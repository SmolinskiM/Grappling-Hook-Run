using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int Level { get; private set;}

    private readonly int levelInRow = 5;
    private readonly int levelInCollum = 2;

    private SelectLevel selectLevel;

    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private Button buttonUp;
    [SerializeField] private Button buttonDown;

    [SerializeField] private Transform selected;

    private void Start()
    {
        selectLevel = GetComponent<SelectLevel>();
        Level = 1;
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
            Level--;
            selected.transform.Translate(-3, 0, 0);
            selectLevel.LevelChange();
        }
    }

    public void Right()
    {
        if (Level % levelInRow != 0)
        {
            Level++;
            selected.transform.Translate(3, 0, 0);
            selectLevel.LevelChange();
        }
    }

    public void Up()
    {
        if (Level > levelInRow)
        {
            Level -= 5;
            selected.transform.Translate(0, 3, 0);
            selectLevel.LevelChange();
        }
    }

    public void Down()
    {
        if(Level + levelInRow <= levelInRow * levelInCollum)
        {
            Level += 5;
            selected.transform.Translate(0, -3, 0);
            selectLevel.LevelChange();
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, selected.position, 10 * Time.deltaTime);
    }
}
