using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    [SerializeField] Boid boidPrefab;
    [SerializeField] float yValue, maxX, minX, maxZ, minZ;
    [SerializeField] int maxInOneQuad = 4, numberOfBoidsToSpawn = 20;
    Rectangle initialBoundary;
    Quad theTree;

    private void Start() {
        initialBoundary = new Rectangle(0, 0, maxX, maxZ);
        theTree = new Quad(initialBoundary, maxInOneQuad);

        for (int i = 0; i < numberOfBoidsToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            Transform boidTransform = Instantiate(boidPrefab, spawnPos, Quaternion.identity).GetComponent<Transform>();
            theTree.AndObject(boidTransform);
        }

        theTree.DebugLines();
    }

    
}
