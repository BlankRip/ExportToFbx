using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    Stack<int> finalPath;
    [SerializeField] Transform nearStart;
    [SerializeField] Transform nearEnd;
    int start;
    int target;
    Vector3 noedI;
    Vector3 nodeJ;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePathStartNTarget(nearStart, nearEnd, ref start, ref target);
        Debug.Log(start + " , " + target);
        finalPath = DijkstraAlgorithim.Dijkstra(RepresentGraph2D_Array.instance.grapRepresentation, start, target);

        noedI = RepresentGraph2D_Array.instance.allNodes[finalPath.Peek()].transform.position;
        Debug.Log(finalPath.Peek());
        finalPath.Pop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && finalPath.Count == 0)
        {
            UpdatePathStartNTarget(nearStart, nearEnd, ref start, ref target);
            Debug.Log(start + " , " + target);
            finalPath = DijkstraAlgorithim.Dijkstra(RepresentGraph2D_Array.instance.grapRepresentation, start, target);

            noedI = RepresentGraph2D_Array.instance.allNodes[finalPath.Peek()].transform.position;
            Debug.Log(finalPath.Peek());
            finalPath.Pop();
        }

        while (finalPath.Count > 0)
        {
            nodeJ = RepresentGraph2D_Array.instance.allNodes[finalPath.Peek()].transform.position;
            Debug.DrawLine(noedI, nodeJ, Color.blue, Mathf.Infinity);
            noedI = nodeJ;
            Debug.Log(finalPath.Peek());
            finalPath.Pop();
        }
    }

    private void UpdatePathStartNTarget(Transform startPos, Transform targetPos, ref int start, ref int target)
    {
        float closestDistance = int.MaxValue;
        float distance;
        Vector3 direction;
        Vector3 startPosition = startPos.position;
        Vector3 endPosition;
        for (int i = 0; i < RepresentGraph2D_Array.instance.allNodes.Length; i++)
        {
            endPosition = RepresentGraph2D_Array.instance.allNodes[i].transform.position;
            direction = (endPosition - startPosition).normalized;
            distance = Vector3.Distance(startPosition, endPosition);
            if(Physics.Raycast(startPosition, direction, distance, RepresentGraph2D_Array.instance.nonWalkableLayers));
            else
            {
                if(distance < closestDistance)
                {
                    start = i;
                    closestDistance = distance;
                }
            }
        }

        startPosition = targetPos.position;
        closestDistance = int.MaxValue;
        for (int i = 0; i < RepresentGraph2D_Array.instance.allNodes.Length; i++)
        {
            endPosition = RepresentGraph2D_Array.instance.allNodes[i].transform.position;
            direction = (endPosition - startPosition).normalized;
            distance = Vector3.Distance(startPosition, endPosition);
            if(Physics.Raycast(startPosition, direction, distance, RepresentGraph2D_Array.instance.nonWalkableLayers));
            else
            {
                if(distance < closestDistance)
                {
                    target = i;
                    closestDistance = distance;
                }
            }
        }
    }
}
