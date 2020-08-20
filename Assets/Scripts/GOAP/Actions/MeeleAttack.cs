using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttack : GOAP_Action
{
    private void Start() {
        if(requiredStates.Count == 0) {
            requiredStates.Add(GOAP_States.InMeeleRange);
            requiredStates.Add(GOAP_States.HasMeele);
        }

        if(resultStates.Count == 0) {
            resultStates.Add(GOAP_States.DamageingPlayer);
        }
    }
}
