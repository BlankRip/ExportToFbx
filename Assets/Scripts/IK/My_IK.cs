using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_IK : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject segmentPrefab;
    [SerializeField] Vector3 basePostion;
    [SerializeField] bool fixedBase;

    [SerializeField] int numberOfSegments = 3;
    [SerializeField] float segmentLength = 1;
    
    IK_Segment[] segments;

    private void Start() {
        SetUpTenticle();
        TenticlePostionUpdate();
    }

    private void SetUpTenticle() {
        segments = new IK_Segment[numberOfSegments];
        segments[0] = Instantiate(segmentPrefab).GetComponent<IK_Segment>();
        segments[0].SetUpSegment(segmentLength);
        segments[0].transform.position = Vector3.zero;

        for (int i = 1; i < segments.Length; i++) {
            segments[i] = Instantiate(segmentPrefab).GetComponent<IK_Segment>();
            segments[i].SetUpSegment(segmentLength);
            segments[i].transform.position = segments[i-1].transform.position + (segments[i-1].transform.up * segmentLength * 2);
        }
    }

    private void Update() {
        TenticlePostionUpdate();
    }

    private void TenticlePostionUpdate() {
        segments[segments.Length - 1].MoveToPosition(target.transform.position, segmentLength);

        for (int i = segments.Length - 2; i >= 0; i--)
            segments[i].MoveToPosition(segments[i + 1], segmentLength);
        if(fixedBase) {
            segments[0].transform.position = basePostion;
            for (int i = 1; i < segments.Length; i++)
                segments[i].transform.position = segments[i-1].transform.position + (segments[i-1].transform.forward * segmentLength * 2);
        }
    }
}
