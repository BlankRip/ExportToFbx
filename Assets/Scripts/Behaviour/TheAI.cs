using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAI : MonoBehaviour
{
    public float movementSpeed;
    public float battaryCharge = 100;

    public Transform chargingPoint;
    public Transform[] waypoints;
    public int waypointTracker;
    public Vector3 moveToPosition;

    public bool skipForMe;
    Node root;

    // Start is called before the first frame update
    void Start()
    {
        waypointTracker = 0;
        moveToPosition = waypoints[waypointTracker].position;

        root = new SelectorNode();
        Node patroleSequence = new SequanceNode();
        Node charingSequance = new SequanceNode();
        charingSequance.skipWhenRunning = false;

        root.referancesToChildern.Add(patroleSequence);
        patroleSequence.referancesToChildern.Add(new CheckChargeNode());
        patroleSequence.referancesToChildern.Add(new MoveToPointNode());
        patroleSequence.referancesToChildern.Add(new PickWaypointNode());

        root.referancesToChildern.Add(charingSequance);
        charingSequance.referancesToChildern.Add(new MoveToPointNode());
        charingSequance.referancesToChildern.Add(new ChargingNode());
        charingSequance.referancesToChildern[1].skipWhenRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        root.Exicute(this);
    }
}
