using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresentGraph2D_Array : MonoBehaviour
{
    public static RepresentGraph2D_Array instance;
    [SerializeField] string nodeTag = "PathFindNode";
    public LayerMask nonWalkableLayers;
    public  GameObject[] allNodes;
    public float[][] grapRepresentation;

    Vector3 iPos;
    Vector3 jPos;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
            instance = this;
        allNodes = GameObject.FindGameObjectsWithTag(nodeTag);
        Create2DArray(allNodes, ref grapRepresentation, nonWalkableLayers);
    }

    // private void Update() 
    // {
    //     for (int i = 0; i < grapRepresentation.Length; i++)
    //     {
    //         for (int j = 0; j < grapRepresentation[i].Length; j++)
    //         {
    //             if(grapRepresentation[i][j] > 0)
    //             {
    //                 iPos = allNodes[i].transform.position;
    //                 jPos = allNodes[j].transform.position;
    //                 Debug.DrawLine(iPos, jPos, Color.green);
    //             }
    //         }
    //     }
    // }

    private void Create2DArray(GameObject[] nodes, ref float[][] arrayToFill, LayerMask objstacleLayers)
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
            for (int j = 0; j < nodes.Length; j++)
            {
                if(i != j)
                {
                    iPosition = nodes[i].transform.position;
                    jPosition = nodes[j].transform.position;
                    direction = (jPosition - iPosition).normalized;
                    distanceToNode = Vector3.Distance(iPosition, jPosition);
                    if(Physics.Raycast(iPosition, direction, distanceToNode, objstacleLayers))
                        arrayToFill[i][j] = -1;
                    else
                        arrayToFill[i][j] = distanceToNode;
                }
                else
                {
                    arrayToFill[i][j] = -1;
                }
            }
        }
    }
}
