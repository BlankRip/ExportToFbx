using System.Threading;
using UnityEngine;

class PrintHp : Job
{
    public int MyId { get; set; }
    public Job.JobState State { get; private set; }

    public object Result { get; private set; }

    public Job.JobDoneEvent JobDone { get; set; }

    public PrintHp() {
        JobDone += (o) => { Debug.Log(o); };
    }

    public override void PerformJob() {
        State = Job.JobState.NotStarted;
        for (int i = 0; i < 100; i++) {
            State = Job.JobState.InProgress;
            Debug.Log("Performing Hp Job");
            Thread.Sleep(10);
        }

        Result = 69;
        JobDone(Result);
        State = Job.JobState.Done;
        while(true) {
            Job job;
            if(MyJobSystem.jobs.TryRemove(MyId, out job)) {
                break;
            }
        }
        Debug.Log("DONE");
    }
}
