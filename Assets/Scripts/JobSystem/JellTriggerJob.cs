using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellTriggerJob : MonoBehaviour
{
    [SerializeField] LayerMask jellyLayer;
    [SerializeField] float pressure = 3;
    private Camera gameCam;
    RaycastHit hitInfo;
    Ray ray;
    JellyObjJob jellyObj;

    private void Start() {
        gameCam = Camera.main;
    }

    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            ray = gameCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, jellyLayer))
            {
                jellyObj = hitInfo.transform.gameObject.GetComponent<JellyObjJob>();
                jellyObj.ApplyPressureToPoint(hitInfo.point, pressure);
            }
        }
    }
}
