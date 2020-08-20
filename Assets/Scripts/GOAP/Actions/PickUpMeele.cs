using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMeele : GOAP_Action
{
    private void Start() {
        if(requiredStates.Count == 0) {
            requiredStates.Add(GOAP_States.Awake);
            requiredStates.Add(GOAP_States.MeeleInSite);
        }

        if(resultStates.Count == 0) {
            resultStates.Add(GOAP_States.HasMeele);
        }
    }
}
