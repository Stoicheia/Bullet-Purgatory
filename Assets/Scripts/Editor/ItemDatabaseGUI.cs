using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ItemDatabase d = (ItemDatabase)target;
        if(GUILayout.Button("Update Database"))
        {
            d.UpdateReferences();
        }
    }
}
