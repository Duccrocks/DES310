using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BookManager)), CanEditMultipleObjects]
public class BookManagerEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        BookManager books = (BookManager)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Randomize Colour"))
        {
            books.Colour();
        }
    }
}
