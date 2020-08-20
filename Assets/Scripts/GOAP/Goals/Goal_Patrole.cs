using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Patrole : GOAP_Goal
{
    private void Start() {
        if(goalStates.Count == 0) {
            goalStates.Add(GOAP_States.Patrolling);
        }
    }
}
