using System.Collections.Generic;


public class QuadTree_Test 
{
    public delegate void DebuggingEvent(Rectangle_Test rectangle);
    public DebuggingEvent debugEvent;
    public Quad_Test rootQuad;

    public void InitilizeTree(float rectangelCenterX, float rectangelCenterY, float rectangelHalfHight, float rectangelHalfWidth, int sectionCapacity) {
        Rectangle_Test quadRectangel = new Rectangle_Test(rectangelCenterX, rectangelCenterY, rectangelHalfHight, rectangelHalfWidth);
        rootQuad = new Quad_Test(quadRectangel, sectionCapacity);
    }

    public void AddObjectToTree(float objectPositionX, float objectPositionY, object obj) {
        Point_Test pointToAdd = new Point_Test(objectPositionX, objectPositionY, obj);
        rootQuad.AddPoint(pointToAdd);
    }

    public List<object> ReturnObjectsInArea(float areaCenterX, float areaCenterY, float areaHalfHight, float areaHalfWidth) {
        Rectangle_Test area = new Rectangle_Test(areaCenterX, areaCenterY, areaHalfHight, areaHalfWidth);
        List<Point_Test> points = rootQuad.GetPointsInArea(area, null);
        List<object> objsInArea = new List<object>();
        foreach (Point_Test point in points) {
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


public class Point_Test 
{
    public float x, y;
    public object objectData;

    public Point_Test(float x, float y, object obj) {
        this.x = x;
        this.y = y;
        objectData = obj;
    }
}

public class Rectangle_Test
{
    public float x,y, width,hight;
    public Rectangle_Test(float x, float y, float halfWidth, float halfHight) {
        this.x = x;
        this.y = y;
        width = halfWidth;
        hight = halfHight;
    }

    public bool InMe(Point_Test point) {
        if(point.x > x - width && point.x < x + width && point.y > y - hight && point.y < y + hight)
            return true;
        else
            return false;
    }

    public bool AreaOverlap(Rectangle_Test area) {
        if(area.x - area.width > x + width || area.x + area.width < x - width 
        || area.y - area.hight > y + hight || area.y + area.hight < y - hight)
            return false;
        else
            return true;
    }
}

public class Quad_Test
{
    Rectangle_Test boundary;
    Quad_Test[] subDividedQuads;
    List<Point_Test> pointsInQuad;
    int capacity;
    bool divided;

    public Quad_Test(Rectangle_Test boundary, int capacity) {
        this.boundary = boundary;
        this.capacity = capacity;
        pointsInQuad = new List<Point_Test>();
        divided = false;
    }

    public bool AddPoint(Point_Test point) {
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

    public List<Point_Test> GetPointsInArea(Rectangle_Test area, List<Point_Test> found) {
        if(found == null)
            found = new List<Point_Test>();

        if(!boundary.AreaOverlap(area))
            return found;
        else {
            foreach (Point_Test point in pointsInQuad)
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
        subDividedQuads = new Quad_Test[4];
        float xCenter = boundary.x;
        float yCenter = boundary.y;
        float xShift = boundary.width/2;
        float yShift = boundary.hight/2;

        Rectangle_Test quadrant0 = new Rectangle_Test(xCenter + xShift, yCenter + yShift, xShift, yShift);
        subDividedQuads[0] = new Quad_Test(quadrant0, capacity);

        Rectangle_Test quadrant1 = new Rectangle_Test(xCenter - xShift, yCenter + yShift, xShift, yShift);
        subDividedQuads[1] = new Quad_Test(quadrant1, capacity);

        Rectangle_Test quadrant2 = new Rectangle_Test(xCenter - xShift, yCenter - yShift, xShift, yShift);
        subDividedQuads[2] = new Quad_Test(quadrant2, capacity);

        Rectangle_Test quadrant3 = new Rectangle_Test(xCenter + xShift, yCenter - yShift, xShift, yShift);
        subDividedQuads[3] = new Quad_Test(quadrant3, capacity);

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

    public void DebugLines(QuadTree_Test.DebuggingEvent dEvent) {
        dEvent.Invoke(boundary);
        if(divided) {
            for (int i = 0; i < subDividedQuads.Length; i++)
                subDividedQuads[i].DebugLines(dEvent);
        }
    }
}
