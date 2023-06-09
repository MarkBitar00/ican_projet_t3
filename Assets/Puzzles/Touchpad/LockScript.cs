using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class LockScript : MonoBehaviour
{
    public GameObject _key;
    public float _rotationToOpen;
    [SerializeField] private UnityEvent Unlocked; // permet d'avoir la liste des callback à un event

    private void FixedUpdate()
    {
        if (_key.activeSelf && _key.transform.localEulerAngles.x >= _rotationToOpen)
        {
            Unlock(); // appelle l'event
        }
    }

    public void KeyIsIn()
    {
        GameObject key = GetComponent<XRSocketInteractor>().GetOldestInteractableSelected().transform.gameObject;
        key.SetActive(false);
    }

    public void Unlock()
    {
        Unlocked?.Invoke();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LockScript))]
public class LockScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("ForceUnlock", GUILayout.Width(363)))
        {
            LockScript script = (LockScript)target;
            script.Unlock();
        }
    }
}
#endif