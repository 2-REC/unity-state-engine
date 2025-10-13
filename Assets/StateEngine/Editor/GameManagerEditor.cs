using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager), true)]
public sealed class GameManagerEditor : Editor {

    SerializedProperty gameStateManagerProp;
    SerializedProperty gameDataManagerProp;
    SerializedProperty useGlobalDataManagerProp;
    SerializedProperty globalDataManagerProp;
    SerializedProperty gameStatesGraphProp;
    SerializedProperty gameDataProp;


    private void OnEnable() {
        gameStateManagerProp = serializedObject.FindProperty("gameStateManager");
        gameDataManagerProp = serializedObject.FindProperty("gameDataManager");
        useGlobalDataManagerProp = serializedObject.FindProperty("useGlobalDataManager");
        globalDataManagerProp = serializedObject.FindProperty("globalDataManager");
        gameStatesGraphProp = serializedObject.FindProperty("gameStatesGraph");
        gameDataProp = serializedObject.FindProperty("gameData");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(gameStateManagerProp, new GUIContent("Game State Manager"));

        EditorGUILayout.PropertyField(gameDataManagerProp, new GUIContent("Game Data Manager"));

        EditorGUILayout.PropertyField(useGlobalDataManagerProp, new GUIContent("Use Global Data Manager"));
        if (useGlobalDataManagerProp.boolValue) {
            EditorGUILayout.PropertyField(globalDataManagerProp, new GUIContent("Global Data Manager"));
        }

        EditorGUILayout.PropertyField(gameStatesGraphProp, new GUIContent("Game States Graph"));
        EditorGUILayout.PropertyField(gameDataProp, new GUIContent("Game Data"));

        serializedObject.ApplyModifiedProperties();
    }

}
