using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRange : GOAP_Action
{
    private void Start() {
        if(requiredStates.Count == 0) {
            requiredStates.Add(GOAP_States.Awake);
            requiredStates.Add(GOAP_States.RangeInSite);
        }

        if(resultStates.Count == 0) {
            resultStates.Add(GOAP_States.HasRange);
        }
    }
}
