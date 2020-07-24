using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATest : MonoBehaviour
{   
    Stack<int> finalPath;
    [SerializeField] Transform nearStart, nearEnd;
    [SerializeField] LayerMask targetObjLayer;
    [SerializeField] float gapBtwPathChecks = 1;
    [SerializeField] float arriveRadius;
    [SerializeField] float maxSeekVelocity;
    [SerializeField] float maxSeekForce;
    Node_AStar start, target;
    int previousTIndex;
    Vector3 noedI, nodeJ;

    Rigidbody myRb;
    bool touchDown;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        touchDown = false;
        previousTIndex = -1;

        finalPath = new Stack<int>();
        StartCoroutine(FindPathAfter());
    }

    void Update()
    {
        if(touchDown)
        {
            if (finalPath.Count > 0)
            {
                nodeJ = RepresentGraphIn2DArray_AStar.instance.allNodes[finalPath.Peek()].transform.position;
                if(noedI != nodeJ)
                {
                    nodeJ.y = transform.position.y;
                    Debug.DrawLine(noedI, nodeJ, Color.blue, 3f);
                    noedI = nodeJ;
                }
                
                //Debug.Log(finalPath.Peek());
                if(Vector3.Distance(transform.position, nodeJ) < arriveRadius)
                    finalPath.Pop();

                myRb.velocity += Arrive(nodeJ);
            }
            else
            {
                myRb.velocity += Arrive(nearEnd.transform.position);
            }

            if(myRb.velocity.magnitude > maxSeekVelocity)
                    myRb.velocity = myRb.velocity.normalized * maxSeekVelocity;
        }
    }

    IEnumerator FindPathAfter()
    {
        float distance = Vector3.Distance(transform.position, nearEnd.position);
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, (nearEnd.position - transform.position).normalized, out hitInfo, distance, targetObjLayer) && hitInfo.transform.CompareTag("Player"))
        {
            Debug.Log("Nope");
            while(finalPath.Count > 0)
                finalPath.Pop();
            Debug.DrawRay(transform.position, (nearEnd.position - transform.position).normalized, Color.red, 3f);
            
        }
        else
        {
            Debug.Log("entered");
            UpdatePathStartNTarget(nearStart, nearEnd, ref start, ref target);
            if(previousTIndex != target.myIndex)
            {
                previousTIndex = target.myIndex;
                Debug.Log(start + " , " + target);
                finalPath = AStar.AStarPath(RepresentGraphIn2DArray_AStar.instance.grapRepresentation, RepresentGraphIn2DArray_AStar.instance.allNodes, start, target);

                noedI = RepresentGraphIn2DArray_AStar.instance.allNodes[finalPath.Peek()].transform.position;
                noedI.y = transform.position.y;
                Debug.Log(finalPath.Peek());
                finalPath.Pop();
            }
        }

        yield return new WaitForSeconds(gapBtwPathChecks);
        StartCoroutine(FindPathAfter());
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

    public Vector3 Arrive(Vector3 position)
    {
        Vector3 desigeredVelocity = (position - myRb.transform.position).normalized * maxSeekVelocity;

        float distance = Vector3.Distance(myRb.transform.position, position);
        if (distance < arriveRadius)
            desigeredVelocity = desigeredVelocity * (distance / arriveRadius);

        Vector3 steering = (desigeredVelocity - myRb.velocity);
        if (steering.magnitude > maxSeekForce)
            steering = steering.normalized * maxSeekForce;
            
        return steering;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Ground"))
            touchDown = true;
    }
}
