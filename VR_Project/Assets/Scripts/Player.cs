using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool lightIsOn;
    private MazeCell currentCell;
    public int baseHealth = 3;
    public int currentHealth;
    public void Start()
    {
        currentHealth = baseHealth;
    }

    public void SetLocation (MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }
   public void SetHealth()
    {
        currentHealth = currentHealth - 1;
        if(currentHealth == 0)
        {
            //game over ui
        }
    }

   

}
