﻿using System.Collections;
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

    public bool AreaOverlap(Rectangle area) {
        if(area.x - area.width > x + width || area.x + area.width < x - width 
        || area.y - area.hight > y + hight || area.y + area.hight < y - hight)
            return false;
        else
            return true;
    }
}

public class Quad
{
    Rectangle boundary;
    Quad[] subDividedQuads;
    List<Vector2> pointsInQuad;
    int capacity;
    bool divided;

    public Quad(Rectangle boundary, int capacity) {
        this.boundary = boundary;
        this.capacity = capacity;
        pointsInQuad = new List<Vector2>();
        divided = false;
    }

    public bool AddPoint(Vector2 point) {
        if(!boundary.InMe(point))
            return false;

        if(pointsInQuad.Count < capacity) {
            pointsInQuad.Add(point);
            return true;
        } else {
            if(!divided)
                SubDevide();
            
            for (int i = 0; i < subDividedQuads.Length; i++) {
                if(subDividedQuads[i].AddPoint(point))
                    return true;
            }
            return false;
        }
    }

    public List<Vector2> GetPointsInArea(Rectangle area, List<Vector2> found) {
        if(found == null)
            found = new List<Vector2>();

        if(!boundary.AreaOverlap(area))
            return found;
        else {
            foreach (Vector2 point in pointsInQuad)
            {
                if(area.InMe(point))
                    found.Add(point);
                
                if(divided) {
                    for (int i = 0; i < subDividedQuads.Length; i++) {
                        found = subDividedQuads[i].GetPointsInArea(area, found);
                    }
                }
            }
            return found;
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

    public void DebugLines() {
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