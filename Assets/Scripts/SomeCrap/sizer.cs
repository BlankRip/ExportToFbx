using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizer : MonoBehaviour
{
    [SerializeField] float scaleSize;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.localScale = collision.gameObject.transform.localScale * scaleSize;
    }
}
