using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState
{
    float distance;
    int waypointTracker;

    public override void Initilize(AI ai)
    {
        moveToPosition = ai.waypoints[0].position;
        waypointTracker = 0;
    }

    public override void Exicute(AI ai)
    {
        ai.transform.position = Vector3.MoveTowards(ai.transform.position, moveToPosition, ai.speed);
        ai.batteryHealth -= Time.deltaTime * 2;

        distance = Vector3.Distance(ai.transform.position, moveToPosition);

        if (distance <= 0.5f)
        {
            if (waypointTracker < ai.waypoints.Length)
                waypointTracker++;
            else
                waypointTracker = 0;
            moveToPosition = ai.waypoints[waypointTracker].position;
        }

        if (ai.batteryHealth < 0)
            ai.SwitchState(new GoToRechargeState());
        base.Exicute(ai);
    }
}
