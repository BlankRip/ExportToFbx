using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_AStar : MonoBehaviour
{
    public int myIndex;
    public List<int> connections;

    private void Awake()
    {
        connections = new List<int>();
    }
}
