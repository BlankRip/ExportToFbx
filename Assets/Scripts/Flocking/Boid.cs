using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] float minimumVelocity = 4;
    [SerializeField] float maximumVelocity = 9;
    float initialSpeed;
    public Rigidbody rb;
    Vector3 initialMoveDire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialMoveDire = Random.insideUnitSphere;
        initialMoveDire.y = 0;
        initialSpeed = Random.Range(minimumVelocity, maximumVelocity);

        rb.velocity = initialMoveDire * initialSpeed;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, rb.velocity.normalized, Color.red);

        rb.velocity += Flocking.instance.Flock(this);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.velocity = rb.velocity.normalized * maximumVelocity;
    }
}
