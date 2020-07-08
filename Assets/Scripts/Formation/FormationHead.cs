using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationHead : MonoBehaviour
{
    public enum FormationType {Circle, V }
    [HideInInspector] public static FormationHead instance;

    [SerializeField] FormationType formationType;
    [SerializeField] float circleRadius = 3;
    [SerializeField] int boidsInFormation = 4;
    [SerializeField] float maxVelocity;
    [SerializeField] float maxForce;
    [SerializeField] GameObject boid;
    Rigidbody rb;
    List<FBoid> allBoids;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
    }

    private void Start() 
    {
        allBoids = new List<FBoid>();
        if(boidsInFormation % 2 == 0)
            boidsInFormation++;
        
        for (int i = 0; i < boidsInFormation; i++)
        {
            FBoid currentBoid  = Instantiate(boid, transform.position, transform.rotation).GetComponent<FBoid>();
            allBoids.Add(currentBoid);
        }

        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * 7;
    }

    private void Update() 
    {
        UpdateFormation();
        FlipDir();

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(formationType == FormationType.Circle)
                formationType = FormationType.V;
            else if(formationType == FormationType.V)
                formationType = FormationType.Circle;
        }
    }

    public void UpdateFormation()
    {
        Vector3 targetPos = Vector3.zero;

        float circleSegments = 0;
        if(formationType == FormationType.Circle)
        {
            circleSegments = 2*Mathf.PI / allBoids.Count;
            targetPos.y = 0;
        }

        for (int i = 0; i < allBoids.Count; i++)
        {
            if(formationType == FormationType.V)
            {
                int iMod = i % 2;
                int iModSide = (iMod * 2) - 1;
                targetPos = (-transform.forward * (i + iMod)) + (transform.right * iModSide * (i + iMod));
                allBoids[i].seekPosition = transform.position + targetPos;
            }
            else if(formationType == FormationType.Circle)
            {
                targetPos.x = Mathf.Sin(i * circleSegments);
                targetPos.z = Mathf.Cos(i * circleSegments);
                allBoids[i].seekPosition = transform.position + (targetPos * circleRadius);
            }
            
        }
    }

    private void FlipDir()
    {
        if(transform.position.x > 27 || transform.position.x < -27)
        {
            transform.position = transform.position - transform.right;
            transform.LookAt(transform.position + (-transform.right * 5));
            rb.velocity = transform.forward * 7;
        }
        if(transform.position.z > 17 || transform.position.z < -17)
        {
            transform.position = transform.position - transform.forward;
            transform.LookAt(transform.position + (-transform.forward * 5));
            rb.velocity = transform.forward * 7;
        }
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
