﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class NameTheItemTool : EditorWindow
{
    bool useTool = true;

    GameObject objCloseToCursor;
    Vector3 textPos;
    string mouseOnObject;
    string previousObject = "Nothing";

    string mouseOnWindow;
    string windowOnPreviousUpdate = "Nothing";

    //Style settings
    GUIStyle style = new GUIStyle();
    int textFontSize = 14;
    int sizeBeforUpdate;
    Color colorText = new Color32(255, 102, 0, 255);
    Color colorBeforeUpdate;
    bool updateSettings;
    bool showTextOnScene = true;

    [MenuItem("Window/Selection Identity")]
    public static void ShowWindow()
    {
        GetWindow<NameTheItemTool>("Selection Identity");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("ON"))
        {
            useTool = true;
            mouseOnObject = "Ready For Use";
            mouseOnWindow = "Ready For Use";
        }

        if(GUILayout.Button("OFF"))
        {
            useTool = false;
            mouseOnObject = "Tool is OFF";
            mouseOnWindow = "Tool is OFF";
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Mouse Hovering Over:", EditorStyles.boldLabel);
        GUILayout.Label("Object: " + mouseOnObject);
        GUILayout.Label("Window: " + mouseOnWindow);

        GUILayout.Space(20);
        GUILayout.Label("Tool Settings:", EditorStyles.boldLabel);
        showTextOnScene = EditorGUILayout.Toggle("Show Text on Scene: ", showTextOnScene);
        textFontSize = EditorGUILayout.IntField("Font Size", textFontSize);
        colorText = EditorGUILayout.ColorField("Font Color", colorText);
    }

    void OnInspectorUpdate()
    {
        if (useTool)
        {
            if (mouseOverWindow)
                mouseOnWindow = mouseOverWindow.ToString();
            else
                mouseOnWindow = "Nothing";

            if (mouseOnWindow != windowOnPreviousUpdate)
            {
                this.Repaint();
                windowOnPreviousUpdate = mouseOnWindow;
            }

            if (mouseOnObject != previousObject)
            {
                this.Repaint();
                previousObject = mouseOnObject;
            }

            if(textFontSize != sizeBeforUpdate || colorText != colorBeforeUpdate)
            {
                updateSettings = true;
            }
        }
    }

    private void OnDestroy()
    {
        SceneView.duringSceneGui -= this.SecenGUI;
    }

    private void OnFocus()
    {
        SceneView.duringSceneGui -= this.SecenGUI;
        SceneView.duringSceneGui += this.SecenGUI;

        style.fontStyle = FontStyle.BoldAndItalic;
    }

    void SecenGUI(SceneView scene)
    {
        if (useTool)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseMove)
            {
                objCloseToCursor = HandleUtility.PickGameObject(e.mousePosition, true);

                if (objCloseToCursor != null)
                {
                    //textPos = objCloseToCursor.transform.position;
                    textPos = HandleUtility.GUIPointToWorldRay(e.mousePosition + Vector2.right * 20).origin;
                    mouseOnObject = objCloseToCursor.name;
                }

                if(updateSettings)
                {
                    style.fontSize = textFontSize;
                    sizeBeforUpdate = textFontSize;

                    style.normal.textColor = colorText;
                    colorBeforeUpdate = colorText;
                    updateSettings = false;
                }
            }

            if (objCloseToCursor != null && showTextOnScene)
            {
                Handles.BeginGUI();
                Handles.Label(textPos, mouseOnObject, style);
                Handles.EndGUI();
            }
        }
    }

}