using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRest : GOAP_Action
{
    private void Start() {
        if(requiredStates.states.Count == 0) {
            requiredStates.states.Add(GOAP_States.Awake);
        }

        if(resultStates.states.Count == 0) {
            resultStates.states.Add(GOAP_States.Resting);
        }
    }

    public override void ExicuitAction(GOAP_Agent agent) {
        Debug.Log("<color=red> GO Rest </color>");
    }
}
