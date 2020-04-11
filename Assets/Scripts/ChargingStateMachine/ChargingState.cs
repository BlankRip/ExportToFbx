using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingState : AIState
{
    public override void Exicute(AI ai)
    {
        ai.batteryHealth += Time.deltaTime * 20;
        if (ai.batteryHealth >= 100)
            ai.SwitchState(new PatrolState());
        base.Exicute(ai);
    }
}
