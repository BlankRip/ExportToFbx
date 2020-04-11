using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State: MonoBehaviour
{
    public List<State> connections;
    public virtual void Initialize()
    {
        gameObject.SetActive(true);
    }

    public virtual void Exicute(TheScript manageScript)
    {

    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
