using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
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

                if (referancesToChildern[i].status == RetunWork.Success || referancesToChildern[i].status == RetunWork.Running)
                {
                    status = referancesToChildern[i].status;
                    return;
                }
            }
        }
        status = RetunWork.Fail;
    }
}
