using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LerpTester : MonoBehaviour
{
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [Range(0, 1)] [SerializeField] float t;
    private void Update() {
        transform.position = Vector3.Lerp(a.position, b.position, t);
    }
}