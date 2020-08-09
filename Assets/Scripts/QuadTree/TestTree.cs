using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    [SerializeField] Boid boidPrefab;
    [SerializeField] float yValue, maxX, minX, maxZ, minZ;
    [SerializeField] int maxInOneQuad = 4, numberOfBoidsToSpawn = 20;
    Rectangle testAreaBoundary;
    QuadTree theTree;
    bool drawGiz;
    List<Boid> inMyArea;
    List<Boid> allBoids;

    private void Start() {
        allBoids = new List<Boid>();
        inMyArea = new List<Boid>();
        testAreaBoundary = new Rectangle(Random.Range(minX + 4, maxX - 4),Random.Range(minZ + 4, maxZ - 4), 5,5);

        for (int i = 0; i < numberOfBoidsToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            Boid boid = Instantiate(boidPrefab, spawnPos, Quaternion.identity).GetComponent<Boid>();
            allBoids.Add(boid);
        }
        theTree = new QuadTree();
        theTree.InitilizeTree(0, 0, maxX, maxZ, maxInOneQuad);

        theTree.debugEvent += DebugLinesQuads;
        for (int i = 0; i < allBoids.Count; i++)
            theTree.AddObjectToTree(allBoids[i].transform.position.x, allBoids[i].transform.position.z, allBoids[i]);
        
        theTree.DebugTree();

        ConvertObjsFromTree();
        for (int i = 0; i < inMyArea.Count; i++)
            Debug.Log(inMyArea[i]);
        drawGiz = true;
    }

    private void Update() {
        BuildQuadTree();

        if(Input.GetKey(KeyCode.W)) {
            if(testAreaBoundary.y < maxZ) {
                testAreaBoundary.y += 0.2f;
                Debug.Log(inMyArea.Count);
            }
        }
        if(Input.GetKey(KeyCode.S)) {
            if(testAreaBoundary.y > minZ) {
                testAreaBoundary.y -= 0.2f;
                Debug.Log(inMyArea.Count);
            }
        }
        if(Input.GetKey(KeyCode.D)) {
            if(testAreaBoundary.x < maxX) {
                testAreaBoundary.x += 0.2f;
                Debug.Log(inMyArea.Count);
            }
        }
        if(Input.GetKey(KeyCode.A)) {
            if(testAreaBoundary.x > minX) {
                testAreaBoundary.x -= 0.2f;
                Debug.Log(inMyArea.Count);
            }
        }
        ConvertObjsFromTree();
        DebugLines();
    }

    private void BuildQuadTree() {
        theTree.ClearTree();

        for (int i = 0; i < allBoids.Count; i++)
            theTree.AddObjectToTree(allBoids[i].transform.position.x, allBoids[i].transform.position.z, allBoids[i]);
        
        theTree.DebugTree();
    }

    private void ConvertObjsFromTree() {
        List<object> objsInArea = theTree.ReturnObjectsInArea(testAreaBoundary.x, testAreaBoundary.y, testAreaBoundary.hight, testAreaBoundary.width);
        inMyArea.Clear();
        foreach (object obj in objsInArea) {
            inMyArea.Add((Boid)obj);
        }
    }

    public void DebugLinesQuads(Rectangle boundary) {
        Vector3 leftTopCorner = new Vector3(boundary.x - boundary.width, 0, boundary.y + boundary.hight);
        Vector3 rightTopCorner = new Vector3(boundary.x + boundary.width, 0, boundary.y + boundary.hight);
        Vector3 leftBottomCorner = new Vector3(boundary.x - boundary.width, 0, boundary.y - boundary.hight);
        Vector3 rightBottomCorner = new Vector3(boundary.x + boundary.width, 0, boundary.y - boundary.hight);
        Debug.DrawLine(leftTopCorner, leftBottomCorner, Color.blue);
        Debug.DrawLine(leftBottomCorner, rightBottomCorner, Color.blue);
        Debug.DrawLine(rightBottomCorner, rightTopCorner, Color.blue);
        Debug.DrawLine(rightTopCorner, leftTopCorner, Color.blue);
    }

    public void DebugLines() {
        Vector3 leftTopCorner = new Vector3(testAreaBoundary.x - testAreaBoundary.width, 0, testAreaBoundary.y + testAreaBoundary.hight);
        Vector3 rightTopCorner = new Vector3(testAreaBoundary.x + testAreaBoundary.width, 0, testAreaBoundary.y + testAreaBoundary.hight);
        Vector3 leftBottomCorner = new Vector3(testAreaBoundary.x - testAreaBoundary.width, 0, testAreaBoundary.y - testAreaBoundary.hight);
        Vector3 rightBottomCorner = new Vector3(testAreaBoundary.x + testAreaBoundary.width, 0, testAreaBoundary.y - testAreaBoundary.hight);
        Debug.DrawLine(leftTopCorner, leftBottomCorner, Color.green);
        Debug.DrawLine(leftBottomCorner, rightBottomCorner, Color.green);
        Debug.DrawLine(rightBottomCorner, rightTopCorner, Color.green);
        Debug.DrawLine(rightTopCorner, leftTopCorner, Color.green);
    }

    private void OnDrawGizmos() {
        if(drawGiz) {
            for (int i = 0; i < inMyArea.Count; i++){
                Vector3 center = new Vector3(inMyArea[i].transform.position.x, 0, inMyArea[i].transform.position.z);
                Gizmos.DrawSphere(center, 0.6f);
            }
        }
    }
}
