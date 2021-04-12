using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellTriggerJob : MonoBehaviour
{
    [Header("UI Stuff")]
    [SerializeField] Text stateText;

    [Header("Grid Gen Stuff")]
    [SerializeField] Vector3 startPos = Vector3.zero;
    [SerializeField] [Range(0, 30)] int girdSizeX = 3;
    [SerializeField] [Range(0, 30)] int girdSizeY = 3;
    [SerializeField] Vector2 objDimentionsXZ = new Vector2(1, 1);
    [SerializeField] GameObject gridObj;

    [Header("Jelly Stuff")]
    [SerializeField] LayerMask jellyLayer;
    [SerializeField] float pressure = 3;
    private Camera gameCam;
    RaycastHit hitInfo;
    Ray ray;
    JellyObjJob jellyObj;

    private void Start() {
        gameCam = Camera.main;
        UIUpdate();
        SpawnGrid();
    }

    private void SpawnGrid() {
        for (int x = 0; x < girdSizeX; x++) {
            for (int y = 0; y < girdSizeY; y++) {
                Vector3 spawnPoint = new Vector3((startPos.x + (objDimentionsXZ.x * x)), startPos.y, (startPos.z + (objDimentionsXZ.y * y)));
                GameObject.Instantiate(gridObj, spawnPoint, Quaternion.identity);
            }
        }
    }

    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0)) {
            ray = gameCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, jellyLayer)) {
                jellyObj = hitInfo.transform.gameObject.GetComponent<JellyObjJob>();
                jellyObj.ApplyPressureToPoint(hitInfo.point, pressure);
            }
        }
    }

    private void UIUpdate() {
        if(UseJobSystem.yes)
            stateText.text = "Job System";
        else
            stateText.text = "NO Job System";
    }

    public void SwitchMode() {
        UseJobSystem.yes = !UseJobSystem.yes;
        if(UseJobSystem.yes) {
            ThreadManager.instance.CreateThread();
            stateText.text = "Job System";
        }
        else {
            ThreadManager.instance.TerminateThreads();
            stateText.text = "NO Job System";
        }
    }
}
