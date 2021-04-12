using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Job : MonoBehaviour
{
    public enum JobState { NotStarted, InProgress, Done };
    public JobState State { get; protected set;}

    public object Result { get; protected set;}
    public int MyId { get; set; }

    public abstract void PerformJob();

    public delegate void JobDoneEvent(object result);
    public JobDoneEvent JobDone {get; set;}
}
