using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMovement : MonoBehaviour
{
    NodeGrid nodeGrid;
    Pathfinding pF;

    private MazeCell currentCell;
    public Maze maze;

    public float timeBetweenMoves = 0.15f;
    private float moveTimer;

    public float distanceOfMoves = 0.05f;

    public float speedTrigger = 5f;

    private void Awake()
    {
        nodeGrid = GetComponent<NodeGrid>();

        moveTimer = timeBetweenMoves;
        
        
    }

    private void Update()
    {
        if (nodeGrid.path == null)
            return;

        if (nodeGrid.path.Count > 0)
        {
            moveTimer += Time.deltaTime;
            if(moveTimer >= timeBetweenMoves)
            {
                if(pF.seeker.transform.position != nodeGrid.path[0].worldPosition)
                    pF.seeker.transform.position = Vector3.MoveTowards(pF.seeker.transform.position, nodeGrid.path[0].worldPosition, distanceOfMoves);

                float distToTarget = Vector3.Distance(pF.seeker.transform.position, pF.target.transform.position);
                if (distToTarget < speedTrigger)
                    timeBetweenMoves = timeBetweenMoves * 0.5f;

                //Added in an else to reset the time between moves - Liam
                else
                    timeBetweenMoves = 0.05f;

                moveTimer -= timeBetweenMoves;
            }
        }
    }

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        pF.seeker.transform.localPosition = cell.transform.localPosition;
    }
}
