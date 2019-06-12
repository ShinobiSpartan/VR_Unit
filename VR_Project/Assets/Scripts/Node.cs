using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isWalkable;
    public Vector3 worldPosition;

    public Node(bool walkable, Vector3 worldPos)
    {
        isWalkable = walkable;
        worldPosition = worldPos;
    }
}
