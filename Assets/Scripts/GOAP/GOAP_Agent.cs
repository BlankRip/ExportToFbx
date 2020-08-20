using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_Agent : MonoBehaviour
{
    [SerializeField] GOAP_Goal goal;
    [SerializeField] HashSet<GOAP_States> worldState;
    [SerializeField] HashSet<GOAP_Action> actions;
    public Queue<GOAP_Action> currentPlan;


    void Start() {
        actions = new HashSet<GOAP_Action>(GetComponents<GOAP_Action>());
        currentPlan = new Queue<GOAP_Action>();
    }

    
    void Update() {
        
    }

    private void Plan() {

    }
}
