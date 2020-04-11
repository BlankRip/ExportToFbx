using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointNode : LeafNode
{
    float distance;

    public override void Exicute(TheAI ai)
    {
        distance = Vector3.Distance(ai.transform.position, ai.moveToPosition);
        if (distance <= 0.5f)
        {
            status = RetunWork.Success;
            return;
        }
        Debug.Log("<color=red> IN Moving </color>");
        status = RetunWork.Running;
        ai.transform.position = Vector3.MoveTowards(ai.transform.position, ai.moveToPosition, ai.movementSpeed);
        ai.battaryCharge -= Time.deltaTime * 10;
    }
}
