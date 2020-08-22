using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_Action : MonoBehaviour
{
    public GOAP_StatesList requiredStates;
    public GOAP_StatesList resultStates;

    public virtual void ExicuitAction(GOAP_Agent agent) {}
}
