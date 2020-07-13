using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBoid : MonoBehaviour
{
    public Vector3 seekPosition;
    Rigidbody rb;
    Vector3 seekForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        seekForce = Steering.instance.Arrive(seekPosition, 0.5f, rb);
        rb.velocity += seekForce;
        //transform.position = seekPosition;
    }
}