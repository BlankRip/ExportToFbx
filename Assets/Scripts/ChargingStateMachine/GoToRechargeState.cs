using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRechargeState : AIState
{
    float distance;

    public override void Initilize(AI ai)
    {
        moveToPosition = ai.chargingPoint.position;
    }
    public override void Exicute(AI ai)
    {
        ai.transform.position = Vector3.MoveTowards(ai.transform.position, moveToPosition, ai.speed);
        distance = Vector3.Distance(ai.transform.position, moveToPosition);
        if (distance <= 0.5f)
            ai.SwitchState(new ChargingState());
        base.Exicute(ai);
    }
}
