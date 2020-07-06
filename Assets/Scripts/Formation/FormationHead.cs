using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationHead : MonoBehaviour
{
    public static FormationHead instance;

    [SerializeField] float maxVelocity;
    [SerializeField] float maxForce;
    [SerializeField] GameObject boid;
    [SerializeField] int triOf = 4;
    Rigidbody rb;
    List<FBoid> boids;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
    }

    private void Start() 
    {
        FBoid currentBoid = Instantiate(boid, transform.position, transform.rotation).GetComponent<FBoid>();
        currentBoid.transform.position = new Vector3(transform.position.x, currentBoid.transform.position.y, transform.position.z);
        currentBoid.myX = 0;
        currentBoid.myZ = 0;
        currentBoid.followObj = gameObject.transform;

        for (int i = 1; i < triOf; i++)
        {
            currentBoid = Instantiate(boid, transform.position, transform.rotation).GetComponent<FBoid>();
            currentBoid.transform.position = new Vector3(transform.position.x + i, currentBoid.transform.position.y, transform.position.z - i);
            currentBoid.myX = i;
            currentBoid.myZ = i;
            currentBoid.followObj = gameObject.transform;

            currentBoid = Instantiate(boid, transform.position, transform.rotation).GetComponent<FBoid>();
            currentBoid.transform.position = new Vector3(transform.position.x - i, currentBoid.transform.position.y, transform.position.z - i);
            currentBoid.myX = -i;
            currentBoid.myZ = i;
            currentBoid.followObj = gameObject.transform;
        }

        // rb = GetComponent<Rigidbody>();
        // rb.velocity = Vector3.forward * 10;
    }


    public Vector3 Arrive(Vector3 seekPoition, float slowRadios, Rigidbody rb)
    {
        Vector3 desigeredVelocity = (seekPoition - rb.transform.position).normalized * maxVelocity;

        float distance = Vector3.Distance(rb.transform.position, seekPoition);
        if (distance < slowRadios)
            desigeredVelocity = desigeredVelocity * (distance / slowRadios);

        Vector3 steering = (desigeredVelocity - rb.velocity);
        if (steering.magnitude > maxForce)
            steering = steering.normalized * maxForce;
            
        return steering.normalized;
    }
}
