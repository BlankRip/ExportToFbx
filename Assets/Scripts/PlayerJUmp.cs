using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJUmp : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayers;

    float horizontalInput;
    float verticelInput;
    bool grounded;
    bool jump;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticelInput = Input.GetAxisRaw("Vertical");

        GroundedCheck();

        if(grounded)
            Debug.Log("<color=green> CanJump</color>");
        else
            Debug.Log("<color=red> Can not Jump</color>");

        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void FixedUpdate() 
    {
        Movement(horizontalInput, verticelInput, ref jump);
    }

    private void Movement(float horizontalInput, float verticelInput, ref bool jump)
    {
        rb.velocity = new Vector3(horizontalInput * speed, rb.velocity.y, verticelInput * speed);

        if(jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
    }

    private void GroundedCheck()
    {
        if(Physics.Raycast(transform.position, Vector3.down, transform.localScale.y/2 + 0.1f, groundLayers))
            grounded = true;
        else
            grounded = false;

    }
}
