using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRest : GOAP_Action
{
    bool recovering;
    private void Start() {
        if(requiredStates.states.Count == 0) {
            requiredStates.states.Add(GOAP_States.Awake);
        }

        if(resultStates.states.Count == 0) {
            resultStates.states.Add(GOAP_States.Resting);
        }
    }

    public override void InitializeAction(GOAP_Agent agent) {
        recovering = false;
        agent.movePosition = agent.restingPlace.position;
    }

    public override void ExicuitAction(GOAP_Agent agent) {
        Debug.Log("<color=red> GO Rest </color>");

        if((agent.transform.position - agent.movePosition).sqrMagnitude >= 0.1f * 0.1f)
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.movePosition, agent.moveSpeed);
        else {
            recovering = true;
            agent.worldState.RemoveState(GOAP_States.Awake);
            agent.worldState.AddStates(GOAP_States.Resting);
        }
        
        if(recovering) {
            agent.patroleEnergy += Time.deltaTime;
            if(agent.patroleEnergy >= 100) {
                agent.worldState.RemoveState(GOAP_States.Resting);
                agent.worldState.AddStates(GOAP_States.Awake);
                agent.worldState.AddStates(GOAP_States.HasPatrolEnergy);
                agent.PopAction();
            }
        }
    }
}
