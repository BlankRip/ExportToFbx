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

    public void UpdateVelocity(float bounceSpeed) {
        currentVelocity = currentVelocity - GetCurrentDisplacement() * bounceSpeed * Time.deltaTime;
    }

    public void Settle(float stiffness) {
        currentVelocity *= 1 - stiffness * Time.deltaTime;
    }

    public void ApplyPressureToVertex(Transform transform, Vector3 position, float pressure) {
        Vector3 distanceVertexPoint = currentVertexPosition - transform.InverseTransformPoint(position);
        float adaptedPressure = pressure / (1 + distanceVertexPoint.sqrMagnitude);
        float velocity = adaptedPressure * Time.deltaTime;
        currentVelocity += distanceVertexPoint.normalized * velocity;
    }
}
