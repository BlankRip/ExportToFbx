using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_Action : MonoBehaviour
{
    public List<GOAP_States> requiredStates;
    public List<GOAP_States> resultStates;

    public virtual void ExicuitAction(GOAP_Agent agent) {}
}
