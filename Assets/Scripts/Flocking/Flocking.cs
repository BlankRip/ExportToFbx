using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public static Flocking instance;
    [SerializeField] float cohsionAlignRadius = 3;
    [SerializeField] float alignmentStrength = 1;
    [SerializeField] float chosionStrength = 1;
    [SerializeField] float saperationRadius = 0.2f;
    [SerializeField] float saperationStrength = 1;
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

    public Vector3 Flock(Boid boid)
    {
        Vector3 flockForce = Alignemnt(boid) + Saperation(boid) + Cohesion(boid);
        return flockForce;
    }

    private Vector3 Alignemnt(Boid currentBoid)
    {
        Vector3 alignementVelocity = Vector3.zero;
        int boidsInRadius = 0;
        float distance;

        foreach (var boid in allBoids)
        {
            if(boid != currentBoid)
            {
                distance = Vector3.Distance(currentBoid.transform.position, boid.transform.position);
                if(distance > 0 && distance < cohsionAlignRadius)
                {
                    alignementVelocity += boid.rb.velocity;
                    boidsInRadius++;
                }
            }
        }

        if(boidsInRadius != 0)
        {
            alignementVelocity = alignementVelocity/boidsInRadius;
            alignementVelocity = alignementVelocity.normalized * alignmentStrength;
        }
        
        return alignementVelocity;
    }

    private Vector3 Saperation(Boid currentBoid)
    {
        Vector3 saperationVelocity = Vector3.zero;
        Vector3 distanceVector;
        int boidsInRadius= 0;
        float distance;

        foreach (var boid in allBoids)
        {
            if(boid != currentBoid)
            {
                distanceVector = currentBoid.transform.position - boid.transform.position;
                distance = distanceVector.magnitude;
                if(distance > 0 && distance < saperationRadius)
                {
                    saperationVelocity += distanceVector;
                    boidsInRadius++;
                }
            }
        }

        if(boidsInRadius != 0)
        {
            saperationVelocity = saperationVelocity/boidsInRadius;
            saperationVelocity = saperationVelocity.normalized * saperationStrength;
        }

        return saperationVelocity;
    }

    private Vector3 Cohesion(Boid currentBoid)
    {
        Vector3 cohesionForce = Vector3.zero;
        int boidsInRadius = 0;
        float distance;

        foreach (var boid in allBoids)
        {
            if(boid != currentBoid)
            {
                distance = Vector3.Distance(currentBoid.transform.position, boid.transform.position);
                if(distance > 0 && distance < cohsionAlignRadius)
                {
                    cohesionForce = boid.transform.position;
                    boidsInRadius++;
                }
            }
        }
        
        if(boidsInRadius != 0)
        {
            cohesionForce = cohesionForce/boidsInRadius;
            cohesionForce = cohesionForce - currentBoid.transform.position;
            cohesionForce = cohesionForce.normalized * chosionStrength;
        }

        return cohesionForce;
    }
}
