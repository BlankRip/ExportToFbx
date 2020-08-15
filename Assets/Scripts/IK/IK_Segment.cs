using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_Segment : MonoBehaviour
{
    [SerializeField] float length;
    [SerializeField] GameObject segmentObj;


    public void SetUpSegment(float length) {
        segmentObj.transform.parent = null;
        segmentObj.transform.localScale = new Vector3(segmentObj.transform.localScale.x, length, segmentObj.transform.localScale.z);
        segmentObj.transform.parent = this.transform;
        segmentObj.transform.localPosition = new Vector3(0, length, 0);
    }

    public void MoveToPosition(Vector3 position, float length) {
        transform.LookAt(position);
        Vector3 moveDir = (position - transform.position).normalized;
        transform.position = position + (-moveDir * (length * 2f));
    }

    public void MoveToPosition(IK_Segment segment, float length) {
        MoveToPosition(segment.transform.position, length);
    }
}
