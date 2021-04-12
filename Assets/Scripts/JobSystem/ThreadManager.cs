using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
    public static ThreadManager instance;
    private VertexCaluationJob thread;

    private void Awake() {
        if(instance == null)
            instance = this;
        
        if(UseJobSystem.yes)
            CreateThread();
    }

    public void CreateThread() {
        thread = new VertexCaluationJob();
        MyJobSystem.AddnPerfromJob(thread);
    }

    public void AddJelly(JellyObjJob jelly) {
        thread.myJellyObjects.Enqueue(jelly);
    }

    public void TerminateThreads() {
        thread.terminate = true;
        thread = null;
    }

    private void OnDestroy() {
        TerminateThreads();
    }
}
