using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    AIState current;
    public Transform[] waypoints;
    public Transform chargingPoint;
    public float batteryHealth = 100;

    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        current = new PatrolState();
        current.Initilize(this);
    }

    // Update is called once per frame
    void Update()
    {
        current.Exicute(this);
    }

    public void SwitchState(AIState state)
    {
        current = state;
        current.Initilize(this);
    }
}
