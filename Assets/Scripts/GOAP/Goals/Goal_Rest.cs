using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Rest : GOAP_Goal
{
    private void Start() {
        if(validityApprovalStates.states.Count == 0){
            validityApprovalStates.states.Add(GOAP_States.Awake);
        }

        if(goalStates.states.Count == 0) {
            goalStates.states.Add(GOAP_States.Resting);
        }
    }

    public override bool isValid(GOAP_Agent agent) {
        return true;
    }
}
