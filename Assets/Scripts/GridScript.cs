using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridScript : MonoBehaviour {

    //public bool onlyPathGizmo;
    public bool displayGridGizmos;
    public Vector2 gridWorldSize;
    NodeScript[,] worldGrid;
    public float nodeRadius;
    public LayerMask walkableMask;
    //public Transform player;

    float nodeDiameter;
    int gridMaxX, gridMaxY;

	// Use this for initialization
	void Awake () {
        nodeDiameter = nodeRadius * 2;
        gridMaxX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridMaxY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        makeGrid();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void makeGrid()
    {
        worldGrid = new NodeScript[gridMaxX, gridMaxY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x = 0; x < gridMaxX; x++)
        {
            for (int y = 0; y < gridMaxY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,walkableMask));
                worldGrid[x, y] = new NodeScript(walkable, worldPoint,x,y);
            }
        }
    }

    public int maxSize
    {
        get { return gridMaxX * gridMaxY; }
    }

    public NodeScript nodeFromWorldPos(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridMaxX - 1) * percentX);
        int y = Mathf.RoundToInt((gridMaxY - 1) * percentY);
        return worldGrid[x, y];
    }

    public List<NodeScript> getNodeNeighbors(NodeScript node)
    {
        List<NodeScript> neighborList = new List<NodeScript>();
        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x==0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX>=0 && checkX<gridMaxX && checkY >= 0 && checkY < gridMaxY)
                {
                    neighborList.Add(worldGrid[checkX, checkY]);
                }
            }
        }
        return neighborList;
    }

    //public List<NodeScript> path;

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        /*if (onlyPathGizmo)
        {
            if (path != null)
            {
                foreach (NodeScript n in path)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
                    }
                }
            }
        }*/
        //else {
            if (worldGrid != null && displayGridGizmos)
            {
                //NodeScript playerNode = nodeFromWorldPos(player.position);
                foreach (NodeScript n in worldGrid)
                {
                    if (n.walkable)
                    {
                        Gizmos.color = Color.white;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    /*if (path != null)
                    {
                        if (path.Contains(n))
                        {
                            Gizmos.color = Color.black;
                        }
                    }*/
                    /*if(playerNode == n)
                    {
                        Gizmos.color = Color.cyan;
                    }*/
                    Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
                }
            }
        //}
    }
}
