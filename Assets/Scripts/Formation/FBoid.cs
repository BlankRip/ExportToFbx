using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBoid : MonoBehaviour
{
    public Vector3 seekPosition;
    Rigidbody rb;
    [SerializeField] float maxVel;
    public int myX;
    public int myZ;
    public Transform followObj;
    Vector3 seekForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        seekPosition = new Vector3(followObj.position.x + myX, followObj.position.y, followObj.position.z - myZ);
        seekForce = FormationHead.instance.Arrive(seekPosition, 0.5f, rb);
        rb.velocity += seekForce;
        //transform.position = seekPosition;
    }
}
