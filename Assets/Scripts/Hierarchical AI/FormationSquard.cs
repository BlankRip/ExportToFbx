using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemies{Dog, Cat, Man}


public class FormationSquard : MonoBehaviour
{
    public enum FormationType {Circle, V }
    FormationType formationType;
    public Enemies meAttack;
    public Vector3 seekPos;

    [SerializeField] float circleRadius = 3;
    [SerializeField] int boidsInFormation = 4;
    [SerializeField] GameObject boid;
    Rigidbody rb;
    List<FBoid> allBoids;

    private void OnEnable()
    {
        allBoids = new List<FBoid>();
        if(boidsInFormation % 2 == 0)
            boidsInFormation++;
        
        for (int i = 0; i < boidsInFormation; i++)
        {
            FBoid currentBoid  = Instantiate(boid, transform.position, transform.rotation).GetComponent<FBoid>();
            allBoids.Add(currentBoid);
        }

        Debug.Log("<color=green>" + gameObject.name + "</color><color=blue> is now attacking: </color><color=red>" + 
        meAttack.ToString() + "</color><color=blue> which is at position: </color><color=yellow>" + seekPos.ToString() + "</color>");
        if(meAttack == Enemies.Dog || meAttack == Enemies.Man)
            formationType = FormationType.V;
        if(meAttack == Enemies.Cat)
            formationType = FormationType.Circle;

        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        rb.velocity += Steering.instance.Arrive(seekPos, 0.5f, rb);

        UpdateFormation();
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
            }
        }
    }
}
