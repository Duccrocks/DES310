using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(TreeGen)), CanEditMultipleObjects]
public class TreeGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TreeGen treeGen = (TreeGen)target;
        
        DrawDefaultInspector();
        
        if (GUILayout.Button("Generate"))
        {
            treeGen.Generate();
        }
    }
}
