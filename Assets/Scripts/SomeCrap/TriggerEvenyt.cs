using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvenyt : MonoBehaviour
{
    public Material changMatTo;
    public float forceToAdd;
    Rigidbody currentRb;
    Renderer currentRenderer;


    void OnTriggerEnter(Collider other)
    {
        currentRenderer = other.gameObject.GetComponent<Renderer>();
        currentRb = other.gameObject.GetComponent<Rigidbody>();
        currentRenderer.material = changMatTo;
        currentRb.AddForce(Vector3.up * forceToAdd, ForceMode.Impulse);
    }
}
