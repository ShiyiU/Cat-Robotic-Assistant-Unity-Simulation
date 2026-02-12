using UnityEngine;

public class CatMover : MonoBehaviour
{
    [SerializeField] private GameObject catWaypointParent;
    [SerializeField] private float travelSpeed; 
    private Transform[] catWaypoints;
    private Transform currentWaypoint;
    private int waypointIndex; 

    private void Start()
    {
        catWaypoints = catWaypointParent.GetComponentsInChildren<Transform>();
        waypointIndex = 0;
        currentWaypoint = catWaypoints[waypointIndex];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, travelSpeed * Time.deltaTime);
        
        if (transform.position == currentWaypoint.position)
        {
            ChangeWaypoint(); 
        }
    }

    private void ChangeWaypoint()
    {
        if (waypointIndex == catWaypoints.Length - 1)
        {
            waypointIndex = 0; 
        } else
        {
            waypointIndex += 1; 
        }

        currentWaypoint = catWaypoints[waypointIndex]; 
    }
}
