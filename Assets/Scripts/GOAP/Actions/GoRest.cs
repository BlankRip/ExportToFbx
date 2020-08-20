using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRest : GOAP_Action
{
    private void Start() {
        if(requiredStates.Count == 0) {
            requiredStates.Add(GOAP_States.Awake);
        }

        if(resultStates.Count == 0) {
            resultStates.Add(GOAP_States.Resting);
        }
    }
}
