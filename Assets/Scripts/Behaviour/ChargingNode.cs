using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingNode : LeafNode
{
    public override void Exicute(TheAI ai)
    {
        if (ai.battaryCharge >= 100)
        {
            status = RetunWork.Success;
            ai.skipForMe = false;
            return;
        }
        Debug.Log("<color=blue> IN Charging </color>");
        status = RetunWork.Running;
        ai.skipForMe = true;
        ai.battaryCharge += Time.deltaTime * 20;
    }
}
