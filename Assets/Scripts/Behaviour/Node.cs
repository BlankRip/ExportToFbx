using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public enum RetunWork { Fail = 0,Success = 1, Running = 2}

    public List<Node> referancesToChildern = new List<Node>();
    public RetunWork status;
    public bool skipWhenRunning = true;

    public abstract void Exicute(TheAI ai);
}
