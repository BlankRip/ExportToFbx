using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    [SerializeField] Boid boidPrefab;
    [SerializeField] float yValue, maxX, minX, maxZ, minZ;
    [SerializeField] int maxInOneQuad = 4, numberOfBoidsToSpawn = 20;
    Rectangle testAreaBoundary;
    Rectangle initialBoundary;
    Quad theTree;
    bool drawGiz;
    List<Vector2> inMyArea;

    private void Start() {
        initialBoundary = new Rectangle(0, 0, maxX, maxZ);
        testAreaBoundary = new Rectangle(Random.Range(minX + 4, maxX - 4),Random.Range(minZ + 4, maxZ - 4), 5,5);
        theTree = new Quad(initialBoundary, maxInOneQuad);

        for (int i = 0; i < numberOfBoidsToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            Transform boidTransform = Instantiate(boidPrefab, spawnPos, Quaternion.identity).GetComponent<Transform>();
            Vector2 pointIn2D = new Vector2(boidTransform.position.x, boidTransform.position.z);
            theTree.AddPoint(pointIn2D);
        }

        theTree.DebugLines();
        DebugLines();

        inMyArea = theTree.GetPointsInArea(testAreaBoundary, null);
        for (int i = 0; i < inMyArea.Count; i++)
            Debug.Log(inMyArea[i]);
        drawGiz = true;
    }

    public void DebugLines() {
        Vector3 leftTopCorner = new Vector3(testAreaBoundary.x - testAreaBoundary.width, 0, testAreaBoundary.y + testAreaBoundary.hight);
        Vector3 rightTopCorner = new Vector3(testAreaBoundary.x + testAreaBoundary.width, 0, testAreaBoundary.y + testAreaBoundary.hight);
        Vector3 leftBottomCorner = new Vector3(testAreaBoundary.x - testAreaBoundary.width, 0, testAreaBoundary.y - testAreaBoundary.hight);
        Vector3 rightBottomCorner = new Vector3(testAreaBoundary.x + testAreaBoundary.width, 0, testAreaBoundary.y - testAreaBoundary.hight);
        Debug.DrawLine(leftTopCorner, leftBottomCorner, Color.green, Mathf.Infinity);
        Debug.DrawLine(leftBottomCorner, rightBottomCorner, Color.green, Mathf.Infinity);
        Debug.DrawLine(rightBottomCorner, rightTopCorner, Color.green, Mathf.Infinity);
        Debug.DrawLine(rightTopCorner, leftTopCorner, Color.green, Mathf.Infinity);
    }

    private void OnDrawGizmos() {
        if(drawGiz) {
            for (int i = 0; i < inMyArea.Count; i++){
                Vector3 center = new Vector3(inMyArea[i].x, 0, inMyArea[i].y);
                Gizmos.DrawSphere(center, 0.6f);
            }
        }
    }
}
