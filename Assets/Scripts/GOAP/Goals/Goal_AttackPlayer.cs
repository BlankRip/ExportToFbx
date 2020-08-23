using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_AttackPlayer : GOAP_Goal
{
    private void Start() {
        if(validityApprovalStates.states.Count == 0){
            validityApprovalStates.states.Add(GOAP_States.Awake);
            validityApprovalStates.states.Add(GOAP_States.PlayerInSite);
        }

        if(goalStates.states.Count == 0) {
            goalStates.states.Add(GOAP_States.DamageingPlayer);
        }
    }
}
