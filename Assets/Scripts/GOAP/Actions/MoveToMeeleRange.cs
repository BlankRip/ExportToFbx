using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMeeleRange : GOAP_Action
{
    private void Start() {
        if(requiredStates.Count == 0) {
            requiredStates.Add(GOAP_States.Awake);
            resultStates.Add(GOAP_States.PlayerInSite);
            requiredStates.Add(GOAP_States.HasMeele);
        }

        if(resultStates.Count == 0) {
            resultStates.Add(GOAP_States.InMeeleRange);
        }
    }
}
