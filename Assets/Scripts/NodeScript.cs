using UnityEngine;

using System.Collections;

public class NodeScript : IHeapEntry<NodeScript>{
    public bool walkable;
    public Vector3 worldPos;

    public int gCost;
    public int hCost;

    public int gridX;
    public int gridY;

    public NodeScript parent;
    int nodeHeapIndex;

    public NodeScript(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPos = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int fCost { get { return gCost + hCost; } }

    public int heapIndex
    {
        get { return nodeHeapIndex; }
        set {
            nodeHeapIndex = value; }
    }

    public int CompareTo(NodeScript nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
