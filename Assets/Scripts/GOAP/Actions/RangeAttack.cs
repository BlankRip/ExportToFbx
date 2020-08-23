using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : GOAP_Action
{
    private void Start() {
        if(requiredStates.states.Count == 0) {
            requiredStates.states.Add(GOAP_States.Awake);
            requiredStates.states.Add(GOAP_States.HasRange);
            requiredStates.states.Add(GOAP_States.PlayerInSite);
        }

        if(resultStates.states.Count == 0) {
            resultStates.states.Add(GOAP_States.DamageingPlayer);
        }
    }

    public override void InitializeAction(GOAP_Agent agent) {
        Debug.Log("<color=red> Range Attack </color>");
        agent.PopAction();   
    }
}
