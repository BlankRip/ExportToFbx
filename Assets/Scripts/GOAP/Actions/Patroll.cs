using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroll : GOAP_Action
{
    int waypointTracker;

    private void Start() {
        if(requiredStates.states.Count == 0) {
            requiredStates.states.Add(GOAP_States.Awake);
            requiredStates.states.Add(GOAP_States.HasPatrolEnergy);
        }

        if(resultStates.states.Count == 0) {
            resultStates.states.Add(GOAP_States.Patrolling);
        }
        waypointTracker = 0;
    }

    public override void InitializeAction(GOAP_Agent agent) {
        Vector3 desigredTarget = new Vector3(agent.waypoints[waypointTracker].position.x, agent.transform.position.y, agent.waypoints[waypointTracker].position.z);
        Vector3 dir = desigredTarget - agent.transform.position;
        if(Physics.Raycast(agent.transform.position, dir.normalized, dir.magnitude + 1))
            agent.movePosition = new Vector3(agent.waypoints[0].position.x, agent.transform.position.y, agent.waypoints[0].position.z);
        else
            agent.movePosition = desigredTarget;
    }

    public override void ExicuitAction(GOAP_Agent agent) {
        Debug.Log("<color=red> Patrole </color>");
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.movePosition, agent.moveSpeed);
        agent.patroleEnergy -= Time.deltaTime * agent.pEnergyChangeSpeed;

        float distance = Vector3.Distance(agent.transform.position, agent.movePosition);

        if (distance <= 0.2f) {
            if (waypointTracker < agent.waypoints.Length - 1)
                waypointTracker++;
            else
                waypointTracker = 0;
            agent.movePosition = new Vector3(agent.waypoints[waypointTracker].position.x, agent.transform.position.y, agent.waypoints[waypointTracker].position.z);
            Debug.Log(agent.waypoints[waypointTracker].name);
        }

        if(agent.patroleEnergy <= 0) 
            agent.PopAction();
    }
}
