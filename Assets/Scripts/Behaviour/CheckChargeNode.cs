using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChargeNode : LeafNode
{
    public override void Exicute(TheAI ai)
    {
        if (ai.battaryCharge < 0)
        {
            ai.moveToPosition = ai.chargingPoint.position;
            status = RetunWork.Fail;
            return;
        }
        else
        {
            status = RetunWork.Success;
            return;
        }
    }
}
