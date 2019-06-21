using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Pathfinding enemy;
    public Maze mazePrefab;
    public Player playerPrefab;
    private Player playerInstance;
    private Maze mazeInstance;
    public bool DebugGeneration;
    public List<MazeCellEdge> debugOutSideWalls;

    void Start()
    {
        debugOutSideWalls = new List<MazeCellEdge>();
        DebugGeneration = false;
        BeginGame();
    }


    void Update()
    {
        
        //Changes the generation for instant or debug
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!mazePrefab.isGenerating)
            {
                if (DebugGeneration == false)
                {
                    DebugGeneration = true;
                    return;
                }
                else if (DebugGeneration == true)
                {
                    DebugGeneration = false;
                    return;
                }
            }
        }
        //Restarts the game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
                RestartGame();
           
        }
        
    }

    private void BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        if (DebugGeneration)
        {
            StartCoroutine(mazeInstance.DeBugGenerate());
        }
        else
        {
            mazeInstance.Generate();
        }
        if (!mazeInstance.isGenerating)
        {
            IntVector2 SpawnCoordinates;
            SpawnCoordinates = mazeInstance.RandomCoordinates;
            
            
            if (Vector2.Distance(mazeInstance.GetCell(SpawnCoordinates).transform.localPosition, mazeInstance.exitPrefab.transform.localPosition) < 5)
            {
                while (Vector2.Distance(mazeInstance.GetCell(SpawnCoordinates).transform.localPosition, mazeInstance.exitPrefab.transform.localPosition) < 5)
                {
                    SpawnCoordinates = mazeInstance.RandomCoordinates;
                }
                
            }
            playerInstance = Instantiate(playerPrefab) as Player;
            playerInstance.SetLocation(mazeInstance.GetCell(SpawnCoordinates));
            enemy = Instantiate(enemy) as Pathfinding;
            
        }
    }
    private void RestartGame()
    {
        if (DebugGeneration)
        {
            StopAllCoroutines();
        }
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        BeginGame();
    }
}
