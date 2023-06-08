using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;
    public AnimationRegulator animationRegulator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GeneralManager))]
public class GeneralManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        
        if (GUILayout.Button("Reset Everything", GUILayout.Width(363))) {
             //add everthing the button would do.
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Finish Chest Puzzle", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        if (GUILayout.Button("Reset", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Finish Lazer Puzzle", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        if (GUILayout.Button("Reset", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Finish Music Puzzle", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        if (GUILayout.Button("Reset", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Finish Paint Puzzle", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        if (GUILayout.Button("Reset", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Finish Maze Puzzle", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        if (GUILayout.Button("Reset", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Fill Watch", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        if (GUILayout.Button("Reset", GUILayout.Width(180)))
        {
            //add everthing the button would do.
        }
        EditorGUILayout.EndHorizontal();
    }
}
#endif
