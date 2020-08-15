using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    public Vector2 start;
    public float length;
    public float angle;
    public float selfAngle;
    public Vector2 end;
    public Segment parent;

#region Constructors
    public Segment(float x, float y, float segmentLength, float initialAngle) {
        start = new Vector2(x, y);
        length = segmentLength;
        angle = selfAngle = initialAngle;
        parent = null;
        CalculateSegmentEnd();
    }

    public Segment(Vector2 startPos, float segmentLength, float initialAngle) {
        start = startPos;
        length = segmentLength;
        angle = selfAngle = initialAngle;
        parent = null;
        CalculateSegmentEnd();
    }

    public Segment(Segment parent, float segmentLength, float initialAngle) {
        start = parent.end;
        length = segmentLength;
        angle = selfAngle = initialAngle;
        this.parent = parent;
        CalculateSegmentEnd();
    }
#endregion

    public void CalculateSegmentEnd() {
        float dx = length * Mathf.Cos(angle);
        float dy = length * Mathf.Sin(angle);

        end = new Vector2(start.x + dx, start.y + dy);
    }

    public void UpdateSegment() {
        angle = selfAngle;
        if(parent != null) {
            start = parent.end;
            angle += parent.angle;
        }
        CalculateSegmentEnd();
    }

    public void ChangeSegmentAngle(float changeInAngle) {
        selfAngle += changeInAngle;
    }

    public void DrawSegment() {
        Debug.DrawLine(new Vector3(start.x, start.y, 0), new Vector3(end.x, end.y, 0), Color.green);
    }

}
