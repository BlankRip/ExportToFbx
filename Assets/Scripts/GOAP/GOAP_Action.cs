using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_Action : MonoBehaviour
{
    public GOAP_StatesList requiredStates;
    public GOAP_StatesList resultStates;

    public virtual bool isValid(GOAP_Agent agent) {
        if(agent.currentSimData.worldState.CompareState(requiredStates) == 0 && 
        agent.currentSimData.worldState.CompareState(resultStates) > 0)
            return true;
        else
            return false;
    }
    
    public virtual void InitializeAction(GOAP_Agent agent) {}
    public virtual void ExicuitAction(GOAP_Agent agent) {}
}
