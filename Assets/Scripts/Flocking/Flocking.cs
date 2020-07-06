using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public static Flocking instance;
    [SerializeField] float alignmentRadius = 3;
    public float alignmentStrength = 1;
    [SerializeField] float cohesionRadious = 3;
    public float cohesionStrength = 1;
    [SerializeField] float saperationRadius = 0.2f;
    public float saperationStrength = 1;
    Boid[] allBoids;
    
    private void Awake() 
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        allBoids = FindObjectsOfType<Boid>();
    }

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
                distance = distanceVector.magnitude;
                if(distance > 0 && distance < saperationRadius)
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
}
