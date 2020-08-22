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
}
