using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyVertex
{ 
    public int vertexIndex;
    public Vector3 initialVertexPosition;
    public Vector3 currentVertexPosition;

    public Vector3 currentVelocity;

    public JellyVertex(int _vertexIndex, Vector3 _initialVertexPosition, Vector3 _currentVectexPostion) {
        vertexIndex = _vertexIndex;
        initialVertexPosition = _initialVertexPosition;
        currentVertexPosition = _currentVectexPostion;
        currentVelocity = Vector3.zero;
    }

    public Vector3 GetCurrentDisplacement() {
        return currentVertexPosition - initialVertexPosition;
    }

    public void UpdateVelocity(float bounceSpeed, float time = -1) {
        if(time == -1) {
            time = Time.deltaTime;
        }
        currentVelocity = currentVelocity - GetCurrentDisplacement() * bounceSpeed * time;
    }

    public void Settle(float stiffness, float time = -1) {
        if(time == -1) {
            time = Time.deltaTime;
        }
        currentVelocity *= 1 - stiffness * time;
    }

    public void ApplyPressureToVertex(Transform transform, Vector3 position, float pressure) {
        Vector3 distanceVertexPoint = currentVertexPosition - transform.InverseTransformPoint(position);
        float adaptedPressure = pressure / (1 + distanceVertexPoint.sqrMagnitude);
        float velocity = adaptedPressure * Time.deltaTime;
        currentVelocity += distanceVertexPoint.normalized * velocity;
    }
}
