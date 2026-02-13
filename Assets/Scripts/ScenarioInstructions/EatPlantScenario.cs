using UnityEngine;

public class EatPlantScenario : CatScenario
{
    private Transform[] wayPoints;
    private int[] plant1WaypointOrder;
    private int[] plant2WaypointOrder;
    private int[] chosenWaypointOrder; 
    private int targetIndex;

    public override void InitializeScenario()
    {
        toggleID = 0; 
        loopIndefinitely = false;

        plant1WaypointOrder = new int[] { 0, 1, 2, 13, 12 };
        plant2WaypointOrder = new int[] { }; 

        int plantChoice = Random.Range(0, 1);

        if (plantChoice == 0)
        {
            chosenWaypointOrder = plant1WaypointOrder;
        }
        else
        {
            chosenWaypointOrder = plant2WaypointOrder;
        }
    }

    public override void SetupScenario(CatMovement cat)
    {
        // get the shortest waypointDistance
        wayPoints = cat.GetSpawnWaypoints();

        float startingDistance = Vector3.Distance(cat.transform.position, wayPoints[chosenWaypointOrder[0]].position);
        targetIndex = 0;

        for (int i = 1; i < chosenWaypointOrder.Length; i++)
        {
            Transform currentWaypoint = wayPoints[chosenWaypointOrder[i]];

            if (Vector3.Distance(cat.transform.position, currentWaypoint.position) < startingDistance)
            {
                targetIndex = i;
            }
        }

        Transform plant = GameObject.Find("devil_ivy").transform;

        Transform[] extendedWaypoints = new Transform[plant1WaypointOrder.Length + 1];

        for (int i = 0; i < extendedWaypoints.Length - 1; i++)
        {
            extendedWaypoints[i] = wayPoints[plant1WaypointOrder[i]];
        }

        extendedWaypoints[extendedWaypoints.Length - 1] = plant;

        wayPoints = extendedWaypoints;

        Debug.Log(wayPoints[wayPoints.Length - 1]);

        catMovement = cat; 
    }

    public override void UpdateScenario()
    {
        Debug.Log(wayPoints[targetIndex]);

        if (Vector3.Distance(catMovement.transform.position, wayPoints[targetIndex].position) < 0.7f)
        {
            if (targetIndex == wayPoints.Length - 1)
            {
                catMovement.Anim.Play("cat_eat");
                return;
            }
            else
            {
                targetIndex += 1;
            }
        }
        else
        {
            catMovement.Anim.SetBool("isWalking", true);
        }

        Vector3 direction = (wayPoints[targetIndex].position - catMovement.transform.position);
        direction.y = 0;

        // Rotate toward waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.Slerp(catMovement.RB.rotation, targetRotation, catMovement.TurnSpeed * Time.fixedDeltaTime);
        catMovement.RB.MoveRotation(newRotation);

        // Move forward
        Vector3 forwardMove = catMovement.transform.forward * catMovement.WalkSpeed;
        catMovement.RB.MovePosition(catMovement.RB.position + forwardMove * Time.fixedDeltaTime);
    }
}
