using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATest : MonoBehaviour
{   
    Stack<int> finalPath;
    [SerializeField] Transform nearStart, nearEnd;
    Node_AStar start, target;
    Vector3 noedI, nodeJ;

    void Start()
    {
        UpdatePathStartNTarget(nearStart, nearEnd, ref start, ref target);
        Debug.Log(start + " , " + target);
        finalPath = AStar.AStarPath(RepresentGraphIn2DArray_AStar.instance.grapRepresentation, start, target);

        noedI = RepresentGraphIn2DArray_AStar.instance.allNodes[finalPath.Peek()].transform.position;
        Debug.Log(finalPath.Peek());
        finalPath.Pop();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && finalPath.Count == 0)
        {
            UpdatePathStartNTarget(nearStart, nearEnd, ref start, ref target);
            Debug.Log(start + " , " + target);
            finalPath = AStar.AStarPath(RepresentGraphIn2DArray_AStar.instance.grapRepresentation, start, target);

            noedI = RepresentGraphIn2DArray_AStar.instance.allNodes[finalPath.Peek()].transform.position;
            Debug.Log(finalPath.Peek());
            finalPath.Pop();
        }

        while (finalPath.Count > 0)
        {
            nodeJ = RepresentGraphIn2DArray_AStar.instance.allNodes[finalPath.Peek()].transform.position;
            Debug.DrawLine(noedI, nodeJ, Color.blue, Mathf.Infinity);
            noedI = nodeJ;
            Debug.Log(finalPath.Peek());
            finalPath.Pop();
        }
    }

    private void UpdatePathStartNTarget(Transform startPos, Transform targetPos, ref Node_AStar start, ref Node_AStar target)
    {
        float closestDistance = int.MaxValue;
        float distance;
        Vector3 direction;
        Vector3 startPosition = startPos.position;
        Vector3 endPosition;
        for (int i = 0; i < RepresentGraphIn2DArray_AStar.instance.allNodes.Length; i++)
        {
            endPosition = RepresentGraphIn2DArray_AStar.instance.allNodes[i].transform.position;
            direction = (endPosition - startPosition).normalized;
            distance = Vector3.Distance(startPosition, endPosition);
            if(Physics.Raycast(startPosition, direction, distance, RepresentGraphIn2DArray_AStar.instance.nonWalkableLayers));
            else
            {
                if(distance < closestDistance)
                {
                    start = RepresentGraphIn2DArray_AStar.instance.allNodes[i];
                    closestDistance = distance;
                }
            }
        }

        startPosition = targetPos.position;
        closestDistance = int.MaxValue;
        for (int i = 0; i < RepresentGraphIn2DArray_AStar.instance.allNodes.Length; i++)
        {
            endPosition = RepresentGraphIn2DArray_AStar.instance.allNodes[i].transform.position;
            direction = (endPosition - startPosition).normalized;
            distance = Vector3.Distance(startPosition, endPosition);
            if(Physics.Raycast(startPosition, direction, distance, RepresentGraphIn2DArray_AStar.instance.nonWalkableLayers));
            else
            {
                if(distance < closestDistance)
                {
                    target = RepresentGraphIn2DArray_AStar.instance.allNodes[i];
                    closestDistance = distance;
                }
            }
        }
    }
}
