using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobTest : MonoBehaviour
{
    private void Start() {
        int hpJobId = MyJobSystem.AddnPerfromJob(new PrintHp());
        int sheildJobId = MyJobSystem.AddnPerfromJob(new PrintSheild());
        //int hpJobId = JobSystem.AddJob(new PrintHp(), 10);
        //JobSystem.PerformJob(hpJobId);
        //JobSystem.PerformAllJobs();

        //while(true) {
        //    Job job;
        //    if (JobSystem.jobs.TryGetValue(hpJobId, out job)) {
        //        if (job.State == Job.JobState.Done) {
        //            Console.WriteLine(job.Result);
        //            break;
        //        }
        //    }
        //}
    }
}
