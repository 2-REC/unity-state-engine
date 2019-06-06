using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager), true)]
public sealed class GameManagerEditor : Editor {

    SerializedProperty gameStateManagerProp;
    SerializedProperty gameDataManagerProp;
    SerializedProperty useGlobalDataManagerProp;
    SerializedProperty globalDataManagerProp;


    private void OnEnable() {
        gameStateManagerProp = serializedObject.FindProperty("gameStateManager");
        gameDataManagerProp = serializedObject.FindProperty("gameDataManager");
        useGlobalDataManagerProp = serializedObject.FindProperty("useGlobalDataManager");
        globalDataManagerProp = serializedObject.FindProperty("globalDataManager");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(gameStateManagerProp, new GUIContent("Game State Manager"));

        EditorGUILayout.PropertyField(gameDataManagerProp, new GUIContent("Game Data Manager"));

        EditorGUILayout.PropertyField(useGlobalDataManagerProp, new GUIContent("Use Global Data Manager"));
        if (useGlobalDataManagerProp.boolValue) {
            EditorGUILayout.PropertyField(globalDataManagerProp, new GUIContent("Global Data Manager"));
        }

        serializedObject.ApplyModifiedProperties();
    }

}
