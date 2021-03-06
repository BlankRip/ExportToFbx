﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class JellyObjJob : MonoBehaviour
{
    [SerializeField] float bounceSpeed = 60;
    [SerializeField] float fallForce = 60;
    [SerializeField] float stiffness = 1;

    private MeshFilter meshFilter;
    private Mesh mesh;
    private MeshCollider meshCollider;
    public bool useMeshCollide;

    private JellyVertex[] jellyVertices;
    private Vector3[] currentMeshVertices;
    private float time;
    public bool added;


    private void Start() {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        if(useMeshCollide)
            meshCollider = GetComponent<MeshCollider>();

        GetVertices();
        added = false;
    }

    private void GetVertices() {
        jellyVertices = new JellyVertex[mesh.vertices.Length];
        currentMeshVertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++) {
            jellyVertices[i] = new JellyVertex(i, mesh.vertices[i], mesh.vertices[i]);
            currentMeshVertices[i] = mesh.vertices[i];
        }
    }

    private void Update() {
        time = Time.deltaTime;
        UpdateVertices();
    }

    private void UpdateVertices() {
        if(UseJobSystem.yes) {
            if(!added) { 
                ThreadManager.instance.AddJelly(this);
                added = true;
            }
                mesh.vertices = currentMeshVertices;
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();
                mesh.RecalculateTangents();
                if(useMeshCollide)
                    meshCollider.sharedMesh = mesh;
        } else {
            VerticesMath();
            mesh.vertices = currentMeshVertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            if(useMeshCollide)
                meshCollider.sharedMesh = mesh;
        }
    }

    public void VerticesMath() {
        for (int i = 0; i < jellyVertices.Length; i++) {
            jellyVertices[i].UpdateVelocity(bounceSpeed, time);
            jellyVertices[i].Settle(stiffness, time);

            jellyVertices[i].currentVertexPosition += jellyVertices[i].currentVelocity * time;
            currentMeshVertices[i] = jellyVertices[i].currentVertexPosition;
        }
    }

    public void ApplyPressureToPoint(Vector3 point, float pressure) {
        for (int i = 0; i < jellyVertices.Length; i++)
            jellyVertices[i].ApplyPressureToVertex(transform, point, pressure);
    }

    private void OnCollisionEnter(Collision other) {
        ContactPoint[] collisionPoints = other.contacts;
        for (int i = 0; i < collisionPoints.Length; i++) {
            Vector3 pressurPoint = collisionPoints[i].point + (collisionPoints[i].point * 0.1f);
            ApplyPressureToPoint(pressurPoint, fallForce);
        }
    }
}
