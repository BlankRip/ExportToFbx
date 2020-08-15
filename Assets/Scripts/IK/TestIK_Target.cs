using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIK_Target : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float initalForce;

    private void Start() {
        Vector3 dir = Random.onUnitSphere;
        rb = GetComponent<Rigidbody>();

        rb.AddForce((dir.normalized * initalForce), ForceMode.Impulse);
    }
}
