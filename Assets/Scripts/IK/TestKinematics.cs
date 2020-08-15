using UnityEngine;

public class TestKinematics : MonoBehaviour
{
    Segment testSeg1;
    Segment testSeg2;
    Segment[] testSegments;
    int clicks = 0;

    private void Start() {
        // testSeg1 = new Segment(0,0,3, 30 * Mathf.Deg2Rad);
        // testSeg2 = new Segment(testSeg1 ,3, 30 * Mathf.Deg2Rad);
        testSegments = new Segment[20];
        testSegments[0] = new Segment(0,-15, 5, 30 * Mathf.Deg2Rad);
        for (int i = 1; i < testSegments.Length; i++)
            testSegments[i] = new Segment(testSegments[i-1] ,1, 30 * Mathf.Deg2Rad);
    }

    private void Update() {
        //testSeg1.ChangeSegmentAngle(1 * Mathf.Deg2Rad);
        // testSeg1.UpdateSegment();
        // testSeg1.DrawSegment();
        //testSeg2.ChangeSegmentAngle(3 * Mathf.Deg2Rad);
        // testSeg2.UpdateSegment();
        // testSeg2.DrawSegment();

        // if(Input.GetKey(KeyCode.W)) {
        //     clicks++;
        //     Debug.Log(clicks);
        //     testSeg1.ChangeSegmentAngle(1 * Mathf.Deg2Rad);
        // }

        for (int i = 0; i < testSegments.Length; i++) {
            testSegments[i].UpdateSegment();
            float randomAngelChange = Random.Range(0.1f, 0.2f);
            testSegments[i].ChangeSegmentAngle(randomAngelChange * Mathf.Deg2Rad);
            testSegments[i].DrawSegment();
        }
    }
}
