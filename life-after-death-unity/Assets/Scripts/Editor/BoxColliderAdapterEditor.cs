using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoxColliderAdapter))]
public class BoxColliderAdapterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BoxColliderAdapter boxColliderAdapter = target as BoxColliderAdapter;

        if (GUILayout.Button("Adapt collider"))
        {
            boxColliderAdapter.AdaptColldier();
        }
    }
}
