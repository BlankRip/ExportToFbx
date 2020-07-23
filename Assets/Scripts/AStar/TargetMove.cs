using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    float horizontalInput;
    float verticalInput;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() 
    {
        rb.velocity = new Vector3(horizontalInput * speed, rb.velocity.y, verticalInput * speed);
    }
}
