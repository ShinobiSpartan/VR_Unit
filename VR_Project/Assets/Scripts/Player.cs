using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SeekerMovement seeker;
    public bool lightIsOn;
    private MazeCell currentCell;
    public Maze maze;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            seeker.SetLocation(maze.GetCell(maze.RandomCoordinates));
            SetHealth();
        }
        if (other.tag == "Exit")
        {
            //bring up gui
        }
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
