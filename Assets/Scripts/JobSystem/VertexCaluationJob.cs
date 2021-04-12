using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VertexCaluationJob : Job
{
    private Transform myObj;
    private Vector3 rotateDir;
    public delegate void TheJob();
    TheJob myJob;

    public VertexCaluationJob(TheJob thisToDo, JobDoneEvent done) {
        JobDone += done;
        myJob += thisToDo;
    }

    public override void PerformJob() {
        State = Job.JobState.InProgress;
        myJob();
        JobDone(Result);
        
        Job job;
        while (true) {
            if (MyJobSystem.jobs.TryRemove(MyId, out job)) {
                break;
            }
        }
        State = Job.JobState.Done;
        Debug.Log("DONE");
    }
}
