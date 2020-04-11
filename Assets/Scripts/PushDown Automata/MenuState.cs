using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{

    public override void Exicute(TheScript managerScript)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            managerScript.SetState(connections[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            managerScript.SetState(connections[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            managerScript.SetState(connections[2]);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            managerScript.SetState(connections[1]);
        }
    }
}
