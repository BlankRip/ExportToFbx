using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_Goal : MonoBehaviour
{
    public GOAP_StatesList validityApprovalStates;
    public GOAP_StatesList goalStates;

    public virtual bool isValid(GOAP_Agent agent) {
        if(agent.worldState.CompareState(validityApprovalStates) == 0)
            return true;
        else
            return false;
    }
}
