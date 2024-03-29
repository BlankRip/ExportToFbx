﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationHead : MonoBehaviour
{
    public enum FormationType {Circle, V }
    [HideInInspector] public static FormationHead instance;

    [SerializeField] FormationType formationType;
    [SerializeField] bool startMoveFormation;
    [SerializeField] bool stopMoveFormation;
    [SerializeField] bool rotateCircle;
    [SerializeField] float circleRadius = 3;
    [SerializeField] int boidsInFormation = 4;
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
    }

    private void Update() 
    {
        if(startMoveFormation)
        {
            rb.velocity = Vector3.forward * 7;
            startMoveFormation = false;
        }

        if(stopMoveFormation)
        {
            rb.velocity = Vector3.zero;
            stopMoveFormation = false;
        }


        UpdateFormation();
        FlipDir();

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(formationType == FormationType.Circle)
            {
                formationType = FormationType.V;
                if(rb.velocity.normalized == Vector3.forward)
                    transform.rotation = Quaternion.EulerAngles(0,0,0);
                else
                    transform.rotation = Quaternion.EulerAngles(0,Mathf.PI,0);

            }
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
                targetPos = (-transform.forward * ((i + iMod)/2)) + (transform.right * iModSide * ((i + iMod)/2));
                allBoids[i].seekPosition = transform.position + targetPos;
            }
            else if(formationType == FormationType.Circle)
            {
                float finalAngle = (transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + (i * circleSegments);
                targetPos.x = Mathf.Sin(finalAngle);
                targetPos.z = Mathf.Cos(finalAngle);
                allBoids[i].seekPosition = transform.position + (targetPos * circleRadius);
                if(rotateCircle)
                    transform.Rotate(0, 0.001f , 0);
            }
            
        }
    }

    private void FlipDir()
    {
        if(transform.position.z > 17 || transform.position.z < -17)
        {
            transform.position = transform.position - rb.velocity.normalized;
            transform.Rotate(0, 180 , 0);
            rb.velocity = rb.velocity.normalized * -7;
        }
    }
}
