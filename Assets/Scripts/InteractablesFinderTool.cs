using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;

public class InteractablesFinderTool : EditorWindow
{
    private List<GameObject> _objectList;

    private int _grabMode = 0;
    private int _hasSocket = 0;
    private int _attachment = 0;
    private int _tag = 0;
    private string[] _selectedTags;
    private int _layer = 0;
    private string[] _selectedLayers;

    private bool _showList = true;

    [MenuItem("Tools/XR Interactables Finder")]
    public static void ShowWindow()
    {
        var window = GetWindow<InteractablesFinderTool>("XR Interactable Objects Finder");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Specify the XR Interactables characteristics", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.PrefixLabel("Grab mode");
                var grabModeOptions = new[] { "  Single hand", "  Double hand" };
                _grabMode = GUILayout.SelectionGrid(
                    _grabMode,
                    grabModeOptions,
                    1,
                    EditorStyles.radioButton
                );
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.PrefixLabel("Socket interactor");
                var socketInteractionOptions = new[] { "  Without socket", "  With socket" };
                _hasSocket = GUILayout.SelectionGrid(
                    _hasSocket,
                    socketInteractionOptions,
                    1,
                    EditorStyles.radioButton
                );
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.PrefixLabel("Hinge joint");
                var attachmentOptions = new[] { "  Without joint", "  With joint" };
                _attachment = GUILayout.SelectionGrid(
                    _attachment,
                    attachmentOptions,
                    1,
                    EditorStyles.radioButton
                );
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.PrefixLabel("Object tag");
                var tags = UnityEditorInternal.InternalEditorUtility.tags;
                _tag = EditorGUILayout.MaskField(_tag, tags);
                _selectedTags = tags.Where((t, i) => (_tag & (1 << i)) == (1 << i)).ToArray();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.PrefixLabel("Object layer");
                var layers = UnityEditorInternal.InternalEditorUtility.layers;
                _layer = EditorGUILayout.MaskField(_layer, layers);
                _selectedLayers = layers.Where((t, i) => (_layer & (1 << i)) == (1 << i)).ToArray();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Fetch objects"))
            {
                FetchObjects();
            }
        }
        EditorGUILayout.EndHorizontal();
        if (_objectList is not { Count: > 0 })
            return;
        _showList = EditorGUILayout.Foldout(_showList, "Object list");
        if (!_showList)
            return;
        foreach (var val in _objectList)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(val.name);
                if (GUILayout.Button("Show in scene"))
                {
                    Selection.activeGameObject = val;
                    SceneView.FrameLastActiveSceneView();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void FetchObjects()
    {
        _objectList = new List<GameObject>();
        var interactables = Object.FindObjectsOfType<XRBaseInteractable>();
        FilterObjects(interactables);
    }

    private void FilterObjects(IEnumerable<XRBaseInteractable> interactables)
    {
        var selectMode =
            _grabMode == 0 ? InteractableSelectMode.Single : InteractableSelectMode.Multiple;
        var filteredInteractables = interactables.Where(i => i.selectMode == selectMode).ToArray();
        filteredInteractables =
            _hasSocket == 0
                ? filteredInteractables
                    .Where(i => i.GetComponent<XRSocketInteractor>() == null)
                    .ToArray()
                : filteredInteractables
                    .Where(i => i.GetComponent<XRSocketInteractor>() != null)
                    .ToArray();
        filteredInteractables =
            _attachment == 0
                ? filteredInteractables.Where(i => i.GetComponent<HingeJoint>() == null).ToArray()
                : filteredInteractables.Where(i => i.GetComponent<HingeJoint>() != null).ToArray();
        filteredInteractables = filteredInteractables
            .Where(
                i =>
                    _selectedLayers.Contains(LayerMask.LayerToName(i.gameObject.layer))
                    && _selectedTags.Contains(i.gameObject.tag)
            )
            .ToArray();
        foreach (var interactable in filteredInteractables)
        {
            _objectList.Add(interactable.gameObject);
        }
    }
}
#endif
