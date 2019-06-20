using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    [Header("Maze parts")]
    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    public MazeExit exitPrefab;

    public List<MazeCell> activeCells;
    public List<MazeCell> outsideCells;
    public List<MazeCellEdge> createdWalls;
    public List<MazeCellEdge> outSideWalls;
    public List<MazeCell> createdCells;


    [Header("Maze settings")]
    private MazeCell[,] cells;
    public float generationDelay;

    public IntVector2 size;
    public bool ExitCreated;
    public bool isGenerating;

    public MazeCell GetCell(IntVector2 coordinates)
    {
        //Version 5, i give up back to the original

        return cells[coordinates.x, coordinates.z];
        
    }

    //If in Debug Mode
    public IEnumerator DeBugGenerate()
    {
        
        size.x = 10;
        size.z = 10;
        
        isGenerating = true;
        ExitCreated = false;
        WaitForSeconds delay = new WaitForSeconds(generationDelay);
        cells = new MazeCell[size.x, size.z];
        activeCells = new List<MazeCell>();
        createdWalls = new List<MazeCellEdge>();
        createdCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells, createdWalls);
            
        }
        while (!ExitCreated)
        {
            CreateExit(createdWalls,createdCells);
            
        }
        isGenerating = false;
    }
    //If not in debug mode
    public void Generate()
    {
        size.x = 10;
        size.z = 10; ;
        
        isGenerating = true;
        ExitCreated = false;
        cells = new MazeCell[size.x, size.z];
        activeCells = new List<MazeCell>();
        createdWalls = new List<MazeCellEdge>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenerationStep(activeCells, createdWalls);
        }
        while (!ExitCreated)
        {
            CreateExit(createdWalls, createdCells);

        }
        isGenerating = false;
    }

    private void DoFirstGenerationStep (List<MazeCell> activeCells)
    {
        MazeCell startingCell = CreateCell(RandomCoordinates,false);
        activeCells.Add(startingCell);
        createdCells.Add(startingCell);

    }

    private void DoNextGenerationStep(List<MazeCell> activeCells, List<MazeCellEdge> createdWalls)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];

        if(currentCell.IsFullyInitialized)
        {
            
            activeCells.RemoveAt(currentIndex);
            return;
        }

        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {


            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates,false);
                CreatePassage(currentCell, neighbor, direction,false);
                activeCells.Add(neighbor);
                createdCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction, false, createdWalls,false);
            }
            
        }
        else
        {
            CreateWall(currentCell, null, direction, false, createdWalls,false);
        }
        
    }


    private void CreateExit(List<MazeCellEdge> createdWalls, List<MazeCell> cells)
    {
        /* Version 3
        
        */
        //Debug statement, if the function is called but the exit has been created already
        if (ExitCreated)
        {
            return;
        }

        outsideCells = new List<MazeCell>();
        foreach (MazeCell cell in cells)
        {

            if (cell.coordinates.x == 0 && cell.coordinates.z < size.z ||
                cell.coordinates.x < size.x && cell.coordinates.z == 0 ||
                cell.coordinates.x == size.x - 1 && cell.coordinates.z < size.z ||
                cell.coordinates.x < size.x && cell.coordinates.z == size.z - 1)
            {
                outsideCells.Add(cell);
            }

        }
        while (!ExitCreated)
        {
            int cellIndex = Random.Range(0, outsideCells.Count - 1);
            MazeCell selectedCell = outsideCells[cellIndex];
            int wallIndex = 0;
            //wall is child
            foreach (MazeCellEdge wall in createdWalls)
            {
                if (selectedCell.coordinates.x == 0 && selectedCell.coordinates.z < size.z && wall.direction == MazeDirection.West ||
                    selectedCell.coordinates.x < size.x && selectedCell.coordinates.z == 0 && wall.direction == MazeDirection.South ||
                    selectedCell.coordinates.x == size.x - 1 && selectedCell.coordinates.z < size.z && wall.direction == MazeDirection.East ||
                    selectedCell.coordinates.x < size.x && selectedCell.coordinates.z == size.z - 1 && wall.direction == MazeDirection.North)
                {
                    Destroy(selectedCell.GetComponentInChildren<MazeCell>().GetEdge(wall.direction).gameObject);
                    
                    MazeExit exitWall = Instantiate(exitPrefab) as MazeExit;
                    exitWall.Initialize(selectedCell, null, wall.direction);
                    ExitCreated = true;
                    break;
                   
                }
                wallIndex++;
            }
            
        }
        
       
    }

    private MazeCell CreateCell(IntVector2 coordinates, bool isExit)
    {

        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3((coordinates.x - size.x) * (1f +1f), 0f, (coordinates.z - size.z) * (1f + 1f));
        
        return newCell;
    }
    
    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction, bool isExit)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction, bool exit, List<MazeCellEdge> createdWalls, bool isExit)
    {

        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
        createdWalls.Add(wall);
        
        
    }
    
        
    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates (IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

}
