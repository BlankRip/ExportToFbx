using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_AStar : MonoBehaviour
{
    public float myHCost;
    public int myIndex;
    public List<Node_AStar> connections;

    private void Awake()
    {
        connections = new List<Node_AStar>();
        myHCost = -1;


    }
}