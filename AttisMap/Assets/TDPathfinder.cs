using UnityEngine;
using System.Collections;
using Pathfinding;

public class TDPathfinder : MonoBehaviour {

   public Transform endPoint;

    Seeker seeker;
    Path path;
    int currentWaypoint;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, endPoint.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate() {
        if (path == null) {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        transform.position = path.vectorPath[currentWaypoint];
        currentWaypoint++;
    }

	
}
