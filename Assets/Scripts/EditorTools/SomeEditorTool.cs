using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SomeEditorTool : EditorWindow
{
    [MenuItem("Window/Weapon Builder")]
    public static void ShowWindow()
    {
        GetWindow<SomeEditorTool>("Test Tool");
    }

    private void OnGUI()
    {
        
    }
}
