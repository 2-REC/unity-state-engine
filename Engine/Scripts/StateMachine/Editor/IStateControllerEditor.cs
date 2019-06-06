using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IStateController), true)]
public sealed class IStateControllerEditor : Editor {

    const string DEFAULT_SCENE = "<none>";

    SerializedProperty canLeaveGraphProp;
    SerializedProperty exitSceneProp;


    private void OnEnable() {
        canLeaveGraphProp = serializedObject.FindProperty("canLeaveGraph");
        exitSceneProp = serializedObject.FindProperty("exitScene");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(canLeaveGraphProp, new GUIContent("Can Leave Graph"));
        if (canLeaveGraphProp.boolValue) {
            EditorGUI.indentLevel++;

            List<string> list = new List<string>() { DEFAULT_SCENE };
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                if (scene.enabled) {
                    list.Add(scene.path);
                }
            }

            int choice = list.IndexOf(exitSceneProp.stringValue);
            if (choice == -1) {
                choice = 0; // default
            }
            choice = EditorGUILayout.Popup("Exit Scene", choice, list.ToArray());
            if (choice == 0) {
                exitSceneProp.stringValue = "";
            }
            else {
                exitSceneProp.stringValue = list[choice];
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

}
