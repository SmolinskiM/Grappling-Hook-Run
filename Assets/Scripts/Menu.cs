using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    int x_max = 12;
    int x_min = 0;
    
    int y_max = 0;
    int y_min = -12;
    public Transform selected;

    public int level = 1;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Left()
    {
        if(selected.transform.position.x != x_min)
        {
            level--;
            selected.transform.Translate(-3, 0, 0);
        }
    }

    public void Right()
    {
        if (selected.transform.position.x != x_max)
        {
            level++;
            selected.transform.Translate(3, 0, 0);
        }
    }

    public void Up()
    {
        if (selected.transform.position.y != y_max)
        {
            level -= 5;
            selected.transform.Translate(0, 3, 0);
        }
    }

    public void Down()
    {
        if(selected.transform.position.y != y_min)
        {
            level += 5;
            selected.transform.Translate(0, -3, 0);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, selected.position, 10 * Time.deltaTime);
    }
}
