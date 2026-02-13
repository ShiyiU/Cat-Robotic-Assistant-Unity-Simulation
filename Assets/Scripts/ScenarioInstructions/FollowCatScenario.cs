using UnityEngine;

public class FollowCatScenario : CatScenario
{
    private Transform[] wayPoints; 
    private int[] waypointOrder;
    private int targetIndex;

    public override void InitializeScenario()
    {
        toggleID = -1; 
        loopIndefinitely = true; 

        waypointOrder = new int[] {2, 3, 5, 6, 7, 8, 10, 11, 12, 16}; 
    }

    public override void SetupScenario(CatMovement cat)
    {
        // get the shortest waypointDistance
        wayPoints = cat.GetSpawnWaypoints();

        float startingDistance = Vector3.Distance(cat.transform.position, wayPoints[waypointOrder[0]].position);
        targetIndex = 0; 
        
        for(int i = 1; i < waypointOrder.Length; i++)
        {
            Transform currentWaypoint = wayPoints[waypointOrder[i]]; 

            if (Vector3.Distance(cat.transform.position, currentWaypoint.position) < startingDistance)
            {
                targetIndex = i; 
            }
        }

        catMovement = cat; 
    }

    public override void UpdateScenario()
    {
        Vector3 direction = (wayPoints[waypointOrder[targetIndex]].position - catMovement.transform.position);
        direction.y = 0;

        // Rotate toward waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.Slerp(catMovement.RB.rotation, targetRotation, catMovement.TurnSpeed * Time.fixedDeltaTime);
        catMovement.RB.MoveRotation(newRotation);

        // Move forward
        Vector3 forwardMove = catMovement.transform.forward * catMovement.WalkSpeed;
        catMovement.RB.MovePosition(catMovement.RB.position + forwardMove * Time.fixedDeltaTime);

        if (Vector3.Distance(catMovement.transform.position, wayPoints[waypointOrder[targetIndex]].position) < 0.5f)
        {
            if (targetIndex == waypointOrder.Length - 1)
            {
                targetIndex = 0;
            }
            else
            {
                targetIndex++;
            }
        }

        catMovement.Anim.SetBool("isWalking", true);
    }
}
