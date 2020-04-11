using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWaypointNode : LeafNode
{
    public override void Exicute(TheAI ai)
    {
        if (ai.waypointTracker < ai.waypoints.Length - 1)
            ai.waypointTracker++;
        else
            ai.waypointTracker = 0;
        ai.moveToPosition = ai.waypoints[ai.waypointTracker].position;
        status = RetunWork.Success;
    }
}
