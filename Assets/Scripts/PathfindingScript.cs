using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class PathfindingScript : MonoBehaviour {

    PathRequestManagerScript requestManager;
    GridScript worldGrid;

    //public Transform seeker, target;

    void Awake()
    {
        worldGrid = GetComponent<GridScript>();
        requestManager = GetComponent<PathRequestManagerScript>();
    }

    void Update()
    {
        //findPath(seeker.position, target.position);
    }

    public void startFindPath(Vector3 startPos, Vector3 endPos)
    {
        StartCoroutine(findPath(startPos, endPos));
    }

	IEnumerator findPath(Vector3 startPos, Vector3 endPos)
    {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        NodeScript startNode = worldGrid.nodeFromWorldPos(startPos);
        NodeScript endNode = worldGrid.nodeFromWorldPos(endPos);

        if (startNode.walkable && endNode.walkable)
        {
           

            Heap<NodeScript> openList = new Heap<NodeScript>(worldGrid.maxSize);
            HashSet<NodeScript> closedList = new HashSet<NodeScript>();

            openList.add(startNode);
            while (openList.count > 0)
            {
                NodeScript currentNode = openList.pop();
                closedList.Add(currentNode);
                if (currentNode == endNode)
                {
                    pathSuccess = true;

                    break;
                }

                foreach (NodeScript neighborNode in worldGrid.getNodeNeighbors(currentNode))
                {
                    if (!neighborNode.walkable || closedList.Contains(neighborNode))
                    {
                        continue;
                    }
                    int newMoveCostToNeigh = currentNode.gCost + getDistance(currentNode, neighborNode);
                    if (newMoveCostToNeigh < neighborNode.gCost || !openList.contains(neighborNode))
                    {
                        neighborNode.gCost = newMoveCostToNeigh;
                        neighborNode.hCost = getDistance(neighborNode, endNode);
                        neighborNode.parent = currentNode;

                        if (!openList.contains(neighborNode))
                        {
                            openList.add(neighborNode);
                        }
                        else
                        {
                            openList.updateEntry(neighborNode);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = backtrackPath(startNode, endNode);
        }
        requestManager.finishedProcessingPath(waypoints, pathSuccess);
    }

    int getDistance(NodeScript a , NodeScript b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);

    }

    Vector3[] backtrackPath(NodeScript startNode, NodeScript endNode)
    {
        List<NodeScript> path = new List<NodeScript>();
        NodeScript currNode = endNode;
        //UnityEngine.Debug.Log("EndNode: "+endNode.gridX+" : "+endNode.gridY+" Starting Node: "+ startNode.gridX + " : " + startNode.gridY);
        //generates path for if the start and end nodes are the same
        if (startNode == endNode)
        {
            UnityEngine.Debug.Log("Same node");
            path.Add(startNode);
        }
        while (currNode!= startNode)
        {
            path.Add(currNode);
            currNode = currNode.parent;
        }
        Vector3[] waypoints = simplifyPath(path);
        Array.Reverse(waypoints);
        //UnityEngine.Debug.Log("Waypoint: " + waypoints.Length);
        for (int i = 0; i < waypoints.Length; i++) {
            //UnityEngine.Debug.Log("Waypoint: "+waypoints[i]);
        }
        
        return waypoints;

        //worldGrid.path = path;

    }

    Vector3[] simplifyPath(List<NodeScript> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        if (path.Count == 1)
        {
            waypoints.Add(path[0].worldPos);
        }
        else {
            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i - 1].worldPos);
                }
                directionOld = directionNew;
            }
        }
        return waypoints.ToArray();
    }
}
