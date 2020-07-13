using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    #region Initilize
    public static Steering instance;
    public List<Boid> allBoids;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        allBoids = new List<Boid>();
    }
    #endregion


    #region Seek
    [SerializeField] float maxSeekVelocity = 8;
    [SerializeField] float maxSeekForce = 8;
    public Vector3 Arrive(Vector3 seekPoition, float slowRadios, Rigidbody rb)
    {
        Vector3 desigeredVelocity = (seekPoition - rb.transform.position).normalized * maxSeekVelocity;

        float distance = Vector3.Distance(rb.transform.position, seekPoition);
        if (distance < slowRadios)
            desigeredVelocity = desigeredVelocity * (distance / slowRadios);

        Vector3 steering = (desigeredVelocity - rb.velocity);
        if (steering.magnitude > maxSeekForce)
            steering = steering.normalized * maxSeekForce;
            
        return steering.normalized;
    }
    #endregion


    #region Alingnment
    [SerializeField] float alignmentRadius = 3;
    public float alignmentStrength = 1;
    public Vector3 Alignemnt(Boid currentBoid)
    {
        Vector3 alignementForce = Vector3.zero;
        int boidsInRadius = 0;
        float distance;

        foreach (Boid boid in allBoids)
        {
            if(boid != currentBoid)
            {
                distance = Vector3.Distance(currentBoid.transform.position, boid.transform.position);
                if(distance > 0 && distance < alignmentRadius)
                {
                    alignementForce += boid.rb.velocity;
                    boidsInRadius++;
                }
            }
        }

        if(boidsInRadius != 0)
            alignementForce = alignementForce/boidsInRadius;
        
        return alignementForce;
    }
    #endregion


    #region Cohesion
    [SerializeField] float cohesionRadious = 3;
    public float cohesionStrength = 1;
    public Vector3 Cohesion(Boid currentBoid)
    {
        Vector3 cohesionForce = Vector3.zero;
        int boidsInRadius = 0;
        float distance;

        foreach (Boid boid in allBoids)
        {
            if(boid != currentBoid)
            {
                distance = Vector3.Distance(currentBoid.transform.position, boid.transform.position);
                if(distance > 0 && distance < cohesionRadious)
                {
                    cohesionForce += boid.transform.position;
                    boidsInRadius++;
                }
            }
        }
        
        if(boidsInRadius != 0)
        {
            cohesionForce = cohesionForce/boidsInRadius;
            cohesionForce = cohesionForce - currentBoid.transform.position;
        }

        return cohesionForce;
    }
    #endregion


    #region Saperation
    [SerializeField] float saperationRadius = 0.2f;
    public float saperationStrength = 1;
    public Vector3 Saperation(Boid currentBoid)
    {
        Vector3 saperationForce = Vector3.zero;
        Vector3 distanceVector;
        int boidsInRadius= 0;
        float distance;

        foreach (Boid boid in allBoids)
        {
            if(boid != currentBoid)
            {
                distanceVector = currentBoid.transform.position - boid.transform.position;
                distance = distanceVector.sqrMagnitude;
                if(distance > 0 && distance < saperationRadius * saperationRadius)
                {
                    saperationForce += distanceVector;
                    boidsInRadius++;
                }
            }
        }

        if(boidsInRadius != 0)
            saperationForce = saperationForce/boidsInRadius;

        return saperationForce;
    }
    #endregion

}
