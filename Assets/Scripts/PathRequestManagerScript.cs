using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManagerScript : MonoBehaviour {

    Queue<PathRequest> pathReqQueue = new Queue<PathRequest>();
    PathRequest currPathReq;

    static PathRequestManagerScript instance;
    PathfindingScript pathFinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathfindingScript>();

    }

	public static void requestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newReq = new PathRequest(pathStart, pathEnd, callback);
        instance.pathReqQueue.Enqueue(newReq);
        instance.tryProcessNext();
    }

    void tryProcessNext()
    {
        //Debug.Log(pathReqQueue.Count);
        if (!isProcessingPath && pathReqQueue.Count > 0)
        {
            currPathReq = pathReqQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.startFindPath(currPathReq.pathStart, currPathReq.pathEnd);
        }
    }

    public void finishedProcessingPath(Vector3[] path,bool success)
    {
        currPathReq.callback(path, success);
        isProcessingPath = false;
        tryProcessNext();
    }

    

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            pathStart = start;
            pathEnd = end;
            this.callback = callback;
        }
    }
}
