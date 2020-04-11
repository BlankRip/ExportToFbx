using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float outOfViewDistance = 100;
    Transform myDad;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myDad = GameObject.FindGameObjectWithTag("Shootable").transform;
    }


    private void Update()
    {
        if (Vector3.Distance(myDad.position, transform.position) > outOfViewDistance)
            gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }
}
