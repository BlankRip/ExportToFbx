using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRange : GOAP_Action
{
    private void Start() {
        if(requiredStates.states.Count == 0) {
            requiredStates.states.Add(GOAP_States.Awake);
            requiredStates.states.Add(GOAP_States.RangeInSite);
        }

        if(resultStates.states.Count == 0) {
            resultStates.states.Add(GOAP_States.HasRange);
        }
    }

    public override void InitializeAction(GOAP_Agent agent) {
        agent.movePosition = new Vector3(agent.rangeWeapon.transform.position.x, agent.transform.position.y, agent.rangeWeapon.transform.position.z);
    }

    public override void ExicuitAction(GOAP_Agent agent) {
        Debug.Log("<color=red> Pick-up Range </color>");
        
        if((agent.transform.position - agent.movePosition).sqrMagnitude >= 1f * 1f)
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.movePosition, agent.moveSpeed);
        else {
            agent.rangeWeapon.transform.parent = agent.rangePosition;
            agent.rangeWeapon.transform.localPosition = Vector3.zero;
            agent.worldState.AddStates(GOAP_States.HasRange);
            agent.PopAction();
        }
    }
}
