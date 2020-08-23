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
    }

    public override void InitializeAction(GOAP_Agent agent) {
        agent.movePosition = agent.waypoints[0].position;
        waypointTracker = 0;
    }

    public override void ExicuitAction(GOAP_Agent agent) {
        Debug.Log("<color=red> Patrole </color>");
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.movePosition, agent.moveSpeed);
        agent.patroleEnergy -= Time.deltaTime * agent.pEnergyChangeSpeed;

        float distance = Vector3.Distance(agent.transform.position, agent.movePosition);

        if (distance <= 0.3f) {
            if (waypointTracker < agent.waypoints.Length - 1)
                waypointTracker++;
            else
                waypointTracker = 0;
            agent.movePosition = agent.waypoints[waypointTracker].position;
        }

        if(agent.patroleEnergy <= 0) 
            agent.PopAction();
    }
}
