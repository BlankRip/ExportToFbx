using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresentGraphIn2DArray_AStar : MonoBehaviour
{
    public static RepresentGraphIn2DArray_AStar instance;
    [SerializeField] string nodeTag = "PathFindNode";
    public LayerMask nonWalkableLayers;
    public  Node_AStar[] allNodes;
    public float[][] grapRepresentation;

    Vector3 iPos;
    Vector3 jPos;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
            instance = this;
        allNodes = FindObjectsOfType<Node_AStar>();
        Create2DArray(allNodes, ref grapRepresentation, nonWalkableLayers);
    }

    private void Create2DArray(Node_AStar[] nodes, ref float[][] arrayToFill, LayerMask objstacleLayers)
    {
        Vector3 direction;
        float distanceToNode;
        arrayToFill = new float[nodes.Length][];
        for (int i = 0; i < arrayToFill.Length; i++)
        {
            arrayToFill[i] = new float[nodes.Length];
        }

        Vector3 iPosition;
        Vector3 jPosition;

        for (int i = 0; i < nodes.Length; i++)
        {
            Node_AStar currentNode = nodes[i];
            currentNode.myIndex = i;
            for (int j = 0; j < nodes.Length; j++)
            {
                if(i != j)
                {
                    iPosition = nodes[i].gameObject.transform.position;
                    jPosition = nodes[j].gameObject.transform.position;
                    direction = (jPosition - iPosition).normalized;
                    distanceToNode = Vector3.Distance(iPosition, jPosition);
                    if(Physics.Raycast(iPosition, direction, distanceToNode, objstacleLayers))
                        arrayToFill[i][j] = -1;
                    else
                    {
                        arrayToFill[i][j] = distanceToNode;
                        currentNode.connections.Add(nodes[j]);
                        Debug.DrawLine(iPosition, jPosition, Color.green, Mathf.Infinity);
                    }
                }
                else
                {
                    arrayToFill[i][j] = -1;
                }
            }
        }
    }
}
