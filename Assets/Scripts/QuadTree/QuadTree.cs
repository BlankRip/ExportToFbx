﻿using System.Collections.Generic;


public class QuadTree 
{
    public delegate void DebuggingEvent(Rectangle rectangle);
    public DebuggingEvent debugEvent;
    public Quad rootQuad;

    public void InitilizeTree(float rectangelCenterX, float rectangelCenterY, float rectangelHalfHight, float rectangelHalfWidth, int sectionCapacity) {
        Rectangle quadRectangel = new Rectangle(rectangelCenterX, rectangelCenterY, rectangelHalfHight, rectangelHalfWidth);
        rootQuad = new Quad(quadRectangel, sectionCapacity);
    }

    public void AddObjectToTree(float objectPositionX, float objectPositionY, object obj) {
        Point pointToAdd = new Point(objectPositionX, objectPositionY, obj);
        rootQuad.AddPoint(pointToAdd);
    }

    public List<object> ReturnObjectsInArea(float areaCenterX, float areaCenterY, float areaHalfHight, float areaHalfWidth) {
        Rectangle area = new Rectangle(areaCenterX, areaCenterY, areaHalfHight, areaHalfWidth);
        List<Point> points = rootQuad.GetPointsInArea(area, null);
        List<object> objsInArea = new List<object>();
        foreach (Point point in points) {
            objsInArea.Add(point.objectData);
        }
        return objsInArea;
        
    }

    public void ClearTree() {
        rootQuad.Clear();
    }

    public void DebugTree() {
        if(debugEvent != null) {
            rootQuad.DebugLines(debugEvent);
        }
        else
            throw new System.Exception("The QuadTree does not have it's debug event subscribed");
    }

}


public class Point 
{
    public float x, y;
    public object objectData;

    public Point(float x, float y, object obj) {
        this.x = x;
        this.y = y;
        objectData = obj;
    }
}

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

    public bool InMe(Point point) {
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
    List<Point> pointsInQuad;
    int capacity;
    bool divided;

    public Quad(Rectangle boundary, int capacity) {
        this.boundary = boundary;
        this.capacity = capacity;
        pointsInQuad = new List<Point>();
        divided = false;
    }

    public bool AddPoint(Point point) {
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

    public List<Point> GetPointsInArea(Rectangle area, List<Point> found) {
        if(found == null)
            found = new List<Point>();

        if(!boundary.AreaOverlap(area))
            return found;
        else {
            foreach (Point point in pointsInQuad)
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

    public void Clear() {
        pointsInQuad.Clear();
        if(divided) {
            for (int i = 0; i < subDividedQuads.Length; i++)
                    subDividedQuads[i].Clear();
        }
        divided = false;
        subDividedQuads = null;
    }

    public void DebugLines(QuadTree.DebuggingEvent dEvent) {
        dEvent.Invoke(boundary);
        if(divided) {
            for (int i = 0; i < subDividedQuads.Length; i++)
                subDividedQuads[i].DebugLines(dEvent);
        }
    }
}
