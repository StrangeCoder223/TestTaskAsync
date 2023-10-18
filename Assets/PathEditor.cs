using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    private Path _path;

    private void OnEnable()
    {
        _path = (Path)target;
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty focusProperty = serializedObject.FindProperty("_focusOnNewPoint");
        focusProperty.boolValue = GUILayout.Toggle(focusProperty.boolValue, "Focus on new point");
            
        
        if (_path.Points.Count > 0)
        {
            foreach (var point in _path.Points)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    _path.RemovePoint(point);
                    break;
                }

                GUILayout.Label("Point", GUILayout.Width(50), GUILayout.Height(20));
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUI.BeginChangeCheck();
                point.Position = EditorGUILayout.Vector3Field("Position", point.transform.position);
                EditorGUI.EndChangeCheck();
                
                EditorGUILayout.EndVertical();

                serializedObject.ApplyModifiedProperties();
            }
        }
        
        if (GUILayout.Button("Add Point"))
        {
            _path.AddPoint();
        }

        if (GUI.changed)
        {
            SetObjectDirty(_path.gameObject);
        }
    }

    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}