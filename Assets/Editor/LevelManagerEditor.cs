﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    SerializedProperty levelsProperty;

    bool scenesFoldOut = true;

    private void OnEnable()
    {
        levelsProperty = serializedObject.FindProperty("levels");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((LevelManager)target), typeof(LevelManager), false);

        scenesFoldOut = EditorGUILayout.BeginFoldoutHeaderGroup(scenesFoldOut, "Levels");
        EditorGUI.indentLevel++;
        var numScenes = EditorGUILayout.IntField("Size", levelsProperty.arraySize);
        levelsProperty.arraySize = numScenes;


        for (int i = 0; i < levelsProperty.arraySize; i++)
        {
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(levelsProperty.GetArrayElementAtIndex(i).stringValue);
            scene = (SceneAsset)EditorGUILayout.ObjectField("Element " + i, scene, typeof(SceneAsset), true);
            levelsProperty.GetArrayElementAtIndex(i).stringValue = AssetDatabase.GetAssetPath(scene);
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Debug Levels list");
        EditorGUILayout.PropertyField(levelsProperty);


        serializedObject.ApplyModifiedProperties();
    }
}