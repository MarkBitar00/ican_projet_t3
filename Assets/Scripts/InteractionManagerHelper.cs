using Autodesk.Fbx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRInteractionManager))]
public class InteractionManagerHelper : MonoBehaviour
{
}

[CustomEditor(typeof(InteractionManagerHelper))]
public class InteractionManagerHelperEditor : Editor
{
    private XRBaseInteractable[] _InterractableTab;
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if(FindObjectsOfType<XRInteractionManager>()?.Length == 1)
        {
            EditorGUILayout.LabelField("Interactaion manager : " + FindObjectsOfType<XRInteractionManager>()[0].name);
        }
        if (_InterractableTab == null || _InterractableTab.Count() == 0)
        {
            if (GUILayout.Button("Show Interactables"))
            {
                _InterractableTab = FindObjectsOfType<XRBaseInteractable>();
            }
        }
        if (_InterractableTab != null && _InterractableTab.Count() > 0)
        {
            if (GUILayout.Button("Hide Interactables"))
            {
                _InterractableTab = null;
            }
            if (GUILayout.Button("Force load Manager"))
            {
                foreach (XRBaseInteractable _object in _InterractableTab)
                {
                    _object.interactionManager = FindObjectsOfType<XRInteractionManager>()[0];
                    EditorUtility.SetDirty(_object);
                }
            }
            if (_InterractableTab != null)
            {
                foreach (XRBaseInteractable _object in _InterractableTab)
                {
                    EditorGUILayout.BeginHorizontal();
                    string _isLoaded = "loaded";
                    if (_object.interactionManager == null || _object.interactionManager != FindObjectsOfType<XRInteractionManager>()[0])
                    {
                        _isLoaded = "";
                    }
                    string _type = _object.GetType() + "";
                    _type = _type.Split('.')[4];
                    if(GUILayout.Button(_object.name, GUILayout.Width(100)))
                    {
                        Selection.objects = new GameObject[] { _object.gameObject };
                    }
                    EditorGUILayout.LabelField(_type, GUILayout.Width(150));
                    EditorGUILayout.LabelField(_isLoaded, GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}
