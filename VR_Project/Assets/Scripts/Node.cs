using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isWalkable;
    public Vector3 worldPosition;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    public Node parent;

    public Node(bool walkable, Vector3 worldPos, int _gridX, int _gridY)
    {
        isWalkable = walkable;
        worldPosition = worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
}
