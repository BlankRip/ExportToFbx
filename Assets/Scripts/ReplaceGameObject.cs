using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplaceGameObject : EditorWindow
{
    
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

        }
        GUILayout.EndHorizontal();
    }

}
