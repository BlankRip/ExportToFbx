using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_Agent : MonoBehaviour
{
    [SerializeField] GOAP_Goal goal;
    [SerializeField] GOAP_StatesList worldState;
    [SerializeField] List<GOAP_Action> actions;
    [SerializeField] int maxPlanDepth;
    public Queue<GOAP_Action> currentPlan;


    void Start() {
        actions = new List<GOAP_Action>(GetComponents<GOAP_Action>());
        currentPlan = new Queue<GOAP_Action>();
    }

    
    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            Plan();
            Debug.Log(currentPlan.Count);
            while(currentPlan.Count != 0) {
                currentPlan.Peek().ExicuitAction(this);
                currentPlan.Dequeue();
            }
        }
    }

    private void Plan() {
        currentPlan.Clear();
        SimulationStep sim = new SimulationStep();

        GOAP_Action[] simPlan = new GOAP_Action[maxPlanDepth];

        int minDepth = int.MaxValue;
        sim.worldStates.Push(new GOAP_StatesList(worldState));
        sim.depth.Push(0);
        sim.actions.Push(null);

        List<GOAP_States> targetStates = new List<GOAP_States>(goal.goalStates.states);

        while(sim.worldStates.Count != 0) {
            GOAP_StatesList currentSate = sim.worldStates.Pop();
            int currentDepth = sim.depth.Pop();
            GOAP_Action currentAction = sim.actions.Pop();
            simPlan[currentDepth] = currentAction;

            if(currentDepth > minDepth)
                continue;
            
            if(currentSate.CompareState(goal.goalStates) == 0 || currentDepth >= maxPlanDepth) {
                if(currentDepth < minDepth) {
                    currentPlan.Clear();
                    for (int i = 0; i <= currentDepth; i++) {
                        if(simPlan[i] != null) {
                            if(!currentPlan.Contains(simPlan[i]))
                                currentPlan.Enqueue(simPlan[i]);
                        }
                    }
                    minDepth = currentDepth;
                }
            } else {
                for (int i = 0; i < actions.Count; i++) {
                    if(currentSate.CompareState(actions[i].requiredStates) == 0 && currentSate.CompareState(actions[i].resultStates) > 0) {
                        GOAP_StatesList newSimState = new GOAP_StatesList(currentSate);
                        newSimState.AddStates(actions[i].resultStates);
                        sim.worldStates.Push(newSimState);
                        sim.depth.Push(currentDepth + 1);
                        sim.actions.Push(actions[i]);
                    }
                }
            }
        }
    }
}

public class SimulationStep
{
    public Stack<GOAP_StatesList> worldStates;
    public Stack<GOAP_Action> actions;
    public Stack<int> depth;

    public SimulationStep() {
        worldStates = new Stack<GOAP_StatesList>();
        actions = new Stack<GOAP_Action>();
        depth = new Stack<int>();
    }
}
