using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rectangle
{
    public float x,y, width,hight;
    public Rectangle(float x, float y, float halfWidth, float halfHight)
    {
        this.x = x;
        this.y = y;
        width = halfWidth;
        hight = halfHight;
    }

    public bool InMe(Vector2 point) {
        if(point.x > x - width && point.x < x + width && point.y > y - hight && point.y < y + hight)
            return true;
        else
            return false;
    }
}

public class Quad
{
    Rectangle boundary;
    Quad[] subDividedQuads;
    List<object> objectsInQuad;
    int capacity;
    bool divided;

    public Quad(Rectangle boundary, int capacity) {
        this.boundary = boundary;
        this.capacity = capacity;
        objectsInQuad = new List<object>();
        divided = false;
    }

    public bool AndObject(object obj) {
        Transform currentObj = (Transform) obj;
        if(!boundary.InMe(new Vector2(currentObj.position.x, currentObj.position.z)))
            return false;

        if(objectsInQuad.Count < capacity) {
            objectsInQuad.Add(obj);
            return true;
        } else {
            if(!divided){
                SubDevide();
            }
            
            for (int i = 0; i < subDividedQuads.Length; i++) {

                if(subDividedQuads[i].AndObject(obj))
                    return true;
            }
            return false;
        }
    }

    private void SubDevide() {
        subDividedQuads = new Quad[4];
        float xCenter = boundary.x;
        float yCenter = boundary.y;
        float xShift = boundary.width/2;
        float yShift = boundary.hight/2;

        Rectangle quadrant0 = new Rectangle(xCenter + xShift, yCenter + yShift, xShift, yShift);
        subDividedQuads[0] = new Quad(quadrant0, capacity);

        Rectangle quadrant1 = new Rectangle(xCenter - xShift, yCenter + yShift, xShift, yShift);
        subDividedQuads[1] = new Quad(quadrant1, capacity);

        Rectangle quadrant2 = new Rectangle(xCenter - xShift, yCenter - yShift, xShift, yShift);
        subDividedQuads[2] = new Quad(quadrant2, capacity);

        Rectangle quadrant3 = new Rectangle(xCenter + xShift, yCenter - yShift, xShift, yShift);
        subDividedQuads[3] = new Quad(quadrant3, capacity);

        divided = true;
    }

    public void DebugLines()
    {
        Vector3 leftTopCorner = new Vector3(boundary.x - boundary.width, 0, boundary.y + boundary.hight);
        Vector3 rightTopCorner = new Vector3(boundary.x + boundary.width, 0, boundary.y + boundary.hight);
        Vector3 leftBottomCorner = new Vector3(boundary.x - boundary.width, 0, boundary.y - boundary.hight);
        Vector3 rightBottomCorner = new Vector3(boundary.x + boundary.width, 0, boundary.y - boundary.hight);
        Debug.DrawLine(leftTopCorner, leftBottomCorner, Color.blue, Mathf.Infinity);
        Debug.DrawLine(leftBottomCorner, rightBottomCorner, Color.blue, Mathf.Infinity);
        Debug.DrawLine(rightBottomCorner, rightTopCorner, Color.blue, Mathf.Infinity);
        Debug.DrawLine(rightTopCorner, leftTopCorner, Color.blue, Mathf.Infinity);

        if(divided) {
            for (int i = 0; i < subDividedQuads.Length; i++)
                subDividedQuads[i].DebugLines();
        }
    }
}
