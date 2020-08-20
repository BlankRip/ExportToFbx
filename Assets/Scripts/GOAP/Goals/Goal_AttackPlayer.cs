using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_AttackPlayer : GOAP_Goal
{
    private void Start() {
        if(goalStates.Count == 0) {
            goalStates.Add(GOAP_States.DamageingPlayer);
        }
    }
}
