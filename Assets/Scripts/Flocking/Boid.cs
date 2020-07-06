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
    Vector3 alignmentForce;
    Vector3 saperationForce;
    Vector3 cohesionForce;

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
        alignmentForce = Flocking.instance.Alignemnt(this) * Flocking.instance.alignmentStrength;
        saperationForce = Flocking.instance.Saperation(this) * Flocking.instance.saperationStrength;
        cohesionForce = Flocking.instance.Cohesion(this) * Flocking.instance.cohesionStrength;

        rb.velocity += (alignmentForce + saperationForce + cohesionForce) * Time.deltaTime;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.velocity = rb.velocity.normalized * maximumVelocity;

        if(transform.position.x > 27 || transform.position.x < -27)
            transform.position = new Vector3((transform.position.x/Mathf.Abs(transform.position.x)) * 26.5f * -1, transform.position.y, transform.position.z);
        if(transform.position.z > 17 || transform.position.z < -17)
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z/Mathf.Abs(transform.position.z)) * 16.5f * -1);
    }
}
