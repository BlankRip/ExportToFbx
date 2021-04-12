using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Collections.Concurrent;

public class VertexCaluationJob : Job
{
    private Transform myObj;
    private Vector3 rotateDir;
    public delegate void TheJob();
    private TheJob myJob;
    public bool terminate;
    public ConcurrentQueue<JellyObjJob> myJellyObjects;
    //public List<JellyObjJob> myJellyObjects;
    JellyObjJob currentjell;

    public VertexCaluationJob() {
        myJellyObjects = new ConcurrentQueue<JellyObjJob>();
    }

    public override void PerformJob() {
        State = Job.JobState.InProgress;
        while(myJellyObjects.Count > 0) {
            if(terminate)
                break;
            myJellyObjects.TryDequeue(out currentjell);
            currentjell.VerticesMath();
            myJellyObjects.Enqueue(currentjell);
            //Debug.Log("turminate");
            // for (int i = 0; i < myJellyObjects.Count; i++) {
            //     myJellyObjects[i].VerticesMath();
            //     myJellyObjects[i].EndMeshUpdate();
            // }
        }
        
        Job job;
        while (true) {
            if(terminate)
                break;
            if (MyJobSystem.jobs.TryRemove(MyId, out job)) {
                break;
            }
        }

        if(terminate) {
            while(myJellyObjects.Count > 0) {
                myJellyObjects.TryDequeue(out currentjell);
                currentjell.added = false;
            }
        }
        State = Job.JobState.Done;
    }
}
