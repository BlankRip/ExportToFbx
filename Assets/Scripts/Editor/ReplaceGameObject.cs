﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplaceGameObject : EditorWindow
{
    GameObject[] selectedObjects;
    GameObject replacementObject;
    bool copyRotation;
    bool copyScale;

    [MenuItem("Window/GameobjectReplacer")]
    public static void ShowWindow() {
        ReplaceGameObject window = GetWindow<ReplaceGameObject>("Gameobject Replacer");
        window.maxSize = new Vector2(window.maxSize.x, 150);
        window.minSize = new Vector2(250, 130);
    }

    private void OnGUI() {
        GUILayout.Space(15);
        replacementObject = (GameObject)EditorGUILayout.ObjectField("Replacement object", replacementObject, typeof(GameObject),true);
        GUILayout.Space(3);
        copyRotation = EditorGUILayout.Toggle("Copy Rotation", copyRotation);
        copyScale = EditorGUILayout.Toggle("Copy Scale", copyScale);

        GUILayout.Space(30);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if(GUILayout.Button("Replace", GUILayout.Width(75))) {
            Replace();
        }
        GUILayout.EndHorizontal();
    }

    private void Replace()
    {
        selectedObjects = Selection.gameObjects;

        string prefabType = PrefabUtility.GetPrefabAssetType(replacementObject).ToString();
        string instanceStatus = null;
        //Debug.Log(prefabType);
        if(prefabType == "Regular")
            instanceStatus = PrefabUtility.GetPrefabInstanceStatus(replacementObject).ToString();
        //Debug.Log(instanceStatus);
        
        List<GameObject> newSelected = new List<GameObject>();

        foreach (GameObject gameObject in selectedObjects)
        {
            if(gameObject == replacementObject) {
                newSelected.Add(gameObject);
                continue;
            }

            GameObject newGameObject = null;
            
            if(prefabType == "Regular") {
                if(instanceStatus == "Connected") {
                    Object newPrefab = PrefabUtility.GetCorrespondingObjectFromSource(replacementObject);
                    newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(newPrefab);
                    PrefabUtility.SetPropertyModifications(newGameObject, PrefabUtility.GetPropertyModifications(replacementObject));
                }
                else 
                    newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(replacementObject);
            }
            else {
                newGameObject = (GameObject)GameObject.Instantiate(replacementObject);
                newGameObject.name = gameObject.name;
            }

            Undo.RegisterCreatedObjectUndo(newGameObject, "created object");

            newGameObject.transform.position = gameObject.transform.position;
            if(copyRotation)
                newGameObject.transform.rotation = gameObject.transform.rotation;
            if(copyScale)
                newGameObject.transform.localScale = gameObject.transform.localScale;

            Undo.DestroyObjectImmediate(gameObject);
            newSelected.Add(newGameObject);
        }
        Selection.objects = newSelected.ToArray();

        string goString = (newSelected.Count > 1) ? " GameObjects have " : " GameObject has ";
        if(prefabType == "Regular") {
            if(instanceStatus == "Connected")
                prefabType = "Prefab Instance";
            else
                prefabType = "Prefab";
        }
        else
            prefabType = "None";
		Debug.Log(newSelected.Count.ToString() + goString + "been replaced with: <color=green>" + 
        replacementObject.name + "</color>\nPrefab Type: <color=blue>" + prefabType + "</color>");
    }

}