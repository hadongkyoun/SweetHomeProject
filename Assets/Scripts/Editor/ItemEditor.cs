
using System;
using System.IO.Enumeration;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemInformation))]
public class ItemEditor : Editor
{

    private ItemInformation itemInformation;
    private Sprite sprite;
    private GUILayoutOption[] options;

    void OnEnable()
    {
        options = new GUILayoutOption[] {GUILayout.Width(128), GUILayout.Height(128)};

        itemInformation = serializedObject.targetObject as ItemInformation;

        sprite = itemInformation.Sprite;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginVertical();

        GUILayout.Label("Source Image");
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, options);

        EditorGUILayout.EndVertical();
    }
}

