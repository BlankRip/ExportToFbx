using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState 
{
    public Vector3 moveToPosition;

    public virtual void Initilize(AI ai) { }
    public virtual void Exicute(AI ai) { }
}
