using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_AStar : MonoBehaviour
{
    public bool visited;
    public Node_AStar fromNodeIndex;
    public float gcost;
    public float hCost;
    public float fCost;

    public int myIndex;
    public List<Node_AStar> connections;

    public Node_AStar()
    {
        connections = new List<Node_AStar>();
    }
}