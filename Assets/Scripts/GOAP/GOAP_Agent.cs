using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_Agent : MonoBehaviour
{
    //2:02 W9
    [SerializeField] int maxPlanDepth;
    [SerializeField] float gapBtwPlanns = 3;
    [SerializeField] LayerMask rayLayers;
    public float moveSpeed;
    public float pEnergyChangeSpeed = 3;
    public Transform restingPlace;
    public Transform meelePostion;
    public Transform rangePosition;
    public Transform[] waypoints;
    public ParticleSystem hitEffect;
    public Material lowEnergyMat;
    public Material chargedMat;
    [HideInInspector] public Renderer renderer;
    public GOAP_StatesList worldState;
    [SerializeField] List<GOAP_Action> actions;
    [SerializeField] List<GOAP_Goal> goals;
    private GOAP_Goal goal;
    private Queue<GOAP_Action> currentPlan;

    //Sensor stuff
    RaycastHit hitInfo;
    bool meeleAround;
    public GameObject meeleWeapon;
    bool rangeAround;
    public GameObject rangeWeapon;
    bool playerAround;
    public GameObject player;
    public float patroleEnergy;
    [HideInInspector] public Vector3 movePosition;

    void Start() {
        actions = new List<GOAP_Action>(GetComponents<GOAP_Action>());
        goals = new List<GOAP_Goal>(GetComponents<GOAP_Goal>());
        currentPlan = new Queue<GOAP_Action>();
        renderer = GetComponent<Renderer>();

        meeleAround = rangeAround = playerAround = false;
        patroleEnergy = 50;

        StartCoroutine(Planner());
    }

    
    void Update() {
        SensorsUpdate();

        if(currentPlan.Count != 0)
            currentPlan.Peek().ExicuitAction(this);
    }

#region Sensors
    private void SensorsUpdate() {
        PlayerSightSensor();
        RangSightSensor();
        MeeleSightSensor();
        PatrolCapabilitySensor();
    }

    private void PlayerSightSensor() {
        if(playerAround) {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            if(Physics.Raycast(transform.position, direction, out hitInfo, Mathf.Infinity, rayLayers)) {
                if(hitInfo.collider.CompareTag("Player"))
                    worldState.AddStates(GOAP_States.PlayerInSite);
                else
                    worldState.RemoveState(GOAP_States.PlayerInSite);
            }
        }
    }

    private void RangSightSensor() {
        if(rangeAround) {
            Vector3 direction = (rangeWeapon.transform.position - transform.position).normalized;
            if(Physics.Raycast(transform.position, direction, out hitInfo, Mathf.Infinity, rayLayers)) {
                if(hitInfo.collider.CompareTag("RangeWeapon"))
                    worldState.AddStates(GOAP_States.RangeInSite);
                else
                    worldState.RemoveState(GOAP_States.RangeInSite);
            }
        }
    }

    private void MeeleSightSensor() {
        if(meeleAround) {
            Vector3 direction = (meeleWeapon.transform.position - transform.position).normalized;
            if(Physics.Raycast(transform.position, direction, out hitInfo, Mathf.Infinity, rayLayers)) {
                if(hitInfo.collider.CompareTag("MeeleWeapon"))
                    worldState.AddStates(GOAP_States.MeeleInSite);
                else
                    worldState.RemoveState(GOAP_States.MeeleInSite);
            }
        }
    }

    private void PatrolCapabilitySensor() {
        if(patroleEnergy <= 0) {
            renderer.material = lowEnergyMat;
            worldState.RemoveState(GOAP_States.HasPatrolEnergy);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("MeeleWeapon")) {
            meeleAround = true;
            meeleWeapon = other.gameObject;
        }
        if(other.gameObject.CompareTag("RangeWeapon")) {
            rangeAround = true;
            rangeWeapon = other.gameObject;
        }
        if(other.gameObject.CompareTag("Player")) {
            playerAround = true;
            if(player == null)
                player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("MeeleWeapon")) {
            meeleAround = false;
            worldState.RemoveState(GOAP_States.MeeleInSite);
        }
        if(other.gameObject.CompareTag("RangeWeapon")) {
            rangeAround = false;
            worldState.RemoveState(GOAP_States.RangeInSite);
        }
        if(other.gameObject.CompareTag("Player")) {
            playerAround = false;
            worldState.RemoveState(GOAP_States.PlayerInSite);
        }
    }
#endregion

    private void Plan() {
        for (int i = 0; i < goals.Count; i++) {
            if(goals[i].isValid(this)) {
                goal = goals[i];
                break;
            }
        }

        if(worldState.CompareState(goal.goalStates) == 0)
            return;

        currentPlan.Clear();
        Stack<SimulationStep> sim = new Stack<SimulationStep>();

        GOAP_Action[] simPlan = new GOAP_Action[maxPlanDepth];

        int minDepth = int.MaxValue;
        sim.Push(new SimulationStep(new GOAP_StatesList(worldState), null, 0));

        List<GOAP_States> targetStates = new List<GOAP_States>(goal.goalStates.states);

        while(sim.Count != 0) {
            SimulationStep currentSimData = sim.Pop();
            simPlan[currentSimData.depth] = currentSimData.action;

            if(currentSimData.depth > minDepth)
                continue;
            
            if(currentSimData.worldState.CompareState(goal.goalStates) == 0 || currentSimData.depth >= maxPlanDepth) {
                if(currentSimData.depth < minDepth) {
                    currentPlan.Clear();
                    for (int i = 0; i <= currentSimData.depth; i++) {
                        if(simPlan[i] != null) {
                            if(!currentPlan.Contains(simPlan[i]))
                                currentPlan.Enqueue(simPlan[i]);
                        }
                    }
                    minDepth = currentSimData.depth;
                }
            } else {
                for (int i = 0; i < actions.Count; i++) {
                    if(currentSimData.worldState.CompareState(actions[i].requiredStates) == 0 && currentSimData.worldState.CompareState(actions[i].resultStates) > 0) {
                        GOAP_StatesList newSimState = new GOAP_StatesList(currentSimData.worldState);
                        newSimState.AddStates(actions[i].resultStates);
                        sim.Push(new SimulationStep(newSimState, actions[i], (currentSimData.depth + 1)));
                    }
                }
            }
        }
    }

    public void PopAction() {
        currentPlan.Dequeue();
        if(currentPlan.Count != 0)
            currentPlan.Peek().InitializeAction(this);
    }

    IEnumerator Planner() {
        Plan();
        Debug.Log(currentPlan.Count);
        if(currentPlan.Count != 0)
            currentPlan.Peek().InitializeAction(this);

        yield return new WaitForSeconds(gapBtwPlanns);
        StartCoroutine(Planner());
    }
}

public class SimulationStep
{
    public GOAP_StatesList worldState;
    public GOAP_Action action;
    public int depth;

    public SimulationStep(GOAP_StatesList worldState, GOAP_Action action, int depth) {
        this.worldState = worldState;
        this.action = action;
        this.depth = depth;
    }
}
