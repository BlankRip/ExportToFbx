using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingNode : LeafNode
{
    float distance;

    public override void Exicute(TheAI ai)
    {
        if (ai.battaryCharge < 0)
        {
            status = RetunWork.Fail;
            return;
        }

        distance = Vector3.Distance(ai.transform.position, ai.moveToPosition);
        if (Vector3.Distance(ai.transform.position, ai.moveToPosition) <= 0.5f)
        {
            if (ai.waypointTracker < ai.waypoints.Length)
                ai.waypointTracker++;
            else
                ai.waypointTracker = 0;
            ai.moveToPosition = ai.waypoints[ai.waypointTracker].position;
        }

        status = RetunWork.Running;
        ai.transform.position = Vector3.MoveTowards(ai.transform.position, ai.moveToPosition, ai.movementSpeed);
        ai.battaryCharge -= Time.deltaTime * 2;
    }
}
