using System;
using System.Threading;
using System.Collections.Concurrent;
using UnityEngine;


public class MyJobSystem
{
    public static int jobId = 0;
    public static ConcurrentDictionary<int, Job> jobs = new ConcurrentDictionary<int, Job>();

    public static int AddnPerfromJob(Job job) {
        int added = AddJob(job, 10);
        if (added >= 0)
            PerformJob(added);
        return added;
    }

    public static int AddJob(Job job, int tries) {
        while (tries >= 0) {
            if (jobs.TryAdd(jobId, job)) {
                job.MyId = jobId;
                jobId++;
                return job.MyId;
            }

            tries--;
            Debug.Log("Failed To add Will Try Again");
            Thread.Sleep(25);
        }
        Debug.Log("Failed To Add Job");
        return -1;
    }

    public static void PerformAllJobs() {
        for (int i = 0; i < jobId; i++) {
            Job job;
            if (jobs.TryGetValue(i, out job)) {
                if (job.State == Job.JobState.NotStarted)
                    ThreadPool.QueueUserWorkItem(o => job.PerformJob());
            }
        }
    }

    public static void PerformJob(int id) {
        Job job;
        if (jobs.TryGetValue(id, out job)) {
            if (job.State == Job.JobState.NotStarted)
                ThreadPool.QueueUserWorkItem(o => job.PerformJob());
        }
    }
}
