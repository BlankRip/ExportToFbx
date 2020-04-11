using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequanceNode : Node
{
    bool check;

    public override void Exicute(TheAI ai)
    {
        for (int i = 0; i < referancesToChildern.Count; i++)
        {
            check = true;
            if (ai.skipForMe)
            {
                if (referancesToChildern[i].skipWhenRunning)
                {
                    check = false;
                }
            }

            if (check)
            {
                referancesToChildern[i].Exicute(ai);

                if (referancesToChildern[i].status == RetunWork.Fail || referancesToChildern[i].status == RetunWork.Running)
                {
                    Debug.Log("<color=yellow> RETURNED </color>");
                    status = referancesToChildern[i].status;
                    return;
                }
            }
            else if(i == referancesToChildern.Count -1)
            {
                Debug.Log("<color=pink> Siquance Fail </color>");
                status = RetunWork.Fail;
                return;
            }
        }
        status = RetunWork.Success;
    }
}
