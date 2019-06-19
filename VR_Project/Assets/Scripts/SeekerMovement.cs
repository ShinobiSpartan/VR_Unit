using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMovement : MonoBehaviour
{
    NodeGrid nodeGrid;

    public GameObject seeker;

    public float timeBetweenMoves = 0.15f;
    private float moveTimer;

    public float distanceOfMoves = 0.05f;

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
                if(seeker.transform.position != nodeGrid.path[0].worldPosition)
                    seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, nodeGrid.path[0].worldPosition, distanceOfMoves);

                moveTimer -= timeBetweenMoves;
            }
        }
    }
}
