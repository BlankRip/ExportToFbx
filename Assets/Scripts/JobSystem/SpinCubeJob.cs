using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpinCubeJob : Job
{
    private Transform myObj;
    private Vector3 rotateDir;
    public int MyId { get; set; }
    public Job.JobState State { get; private set; }

    public object Result { get; private set; }

    public Job.JobDoneEvent JobDone { get; set; }
    public delegate void TheJob(Vector3 dir);
    TheJob myJob;

    public SpinCubeJob(Transform trs, Vector3 dir, TheJob thisToDo) {
        JobDone += (o) => { Debug.Log($"Rotated by: {o}"); };
        myJob += thisToDo;
        myObj = trs;
        rotateDir = dir;
    }

    public override void PerformJob() {
        State = Job.JobState.InProgress;
        while (true) {
            myJob(rotateDir);
            JobDone(Result);
        }
        
        // State = Job.JobState.Done;
        // while (true) {
        //     Job job;
        //     if (MyJobSystem.jobs.TryRemove(MyId, out job)) {
        //         break;
        //     }
        // }
        Debug.Log("DONE");
    }
}
