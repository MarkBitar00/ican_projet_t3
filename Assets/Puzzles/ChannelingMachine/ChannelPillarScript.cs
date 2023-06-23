using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ChannelPillarScript : MonoBehaviour
{
    public ChannelMachineScript channelMachine;
    public Dictionary<GameObject,int> charges = new Dictionary<GameObject,int>();
    public List<Material> materials = new List<Material>();
    public GameObject PivotFeedback;
    public int actualPower;
    public int expectedPower;

    private float _targety = 0f;

    public void Update()
    {
        if (_targety != PivotFeedback.transform.localScale.y)
        {

            if (Mathf.Abs(_targety - PivotFeedback.transform.localScale.y) > 0.03f)
            {
                if (_targety < PivotFeedback.transform.localScale.y)
                {
                    PivotFeedback.transform.localScale = new Vector3(PivotFeedback.transform.localScale.x, PivotFeedback.transform.localScale.y - 0.2f*Time.deltaTime, PivotFeedback.transform.localScale.z);
                }
                if (_targety > PivotFeedback.transform.localScale.y)
                {
                    PivotFeedback.transform.localScale = new Vector3(PivotFeedback.transform.localScale.x, PivotFeedback.transform.localScale.y + 0.2f*Time.deltaTime, PivotFeedback.transform.localScale.z);
                }
            }
            else
            {
                PivotFeedback.transform.localScale = new Vector3(PivotFeedback.transform.localScale.x, _targety, PivotFeedback.transform.localScale.z);
            }
        }
    }

    public void ActualizeSocket(GameObject _socket)
    {
        charges[_socket] = _socket.GetComponent<XRSocketInteractor>().GetOldestInteractableSelected().transform.gameObject.GetComponent<CrystalScript>().Power;
        ActualizePower();
    }

    public void ResetSocket(GameObject _socket)
    {
        charges[_socket] = 0;
        ActualizePower();
    }

    public void ActualizePower() {
        actualPower = 0;
        foreach (KeyValuePair<GameObject, int> entry in charges)
        {
            actualPower += entry.Value;
        }
        ActualizeFeedback((float)actualPower / (float)expectedPower );
        if (actualPower == expectedPower)
        {
            channelMachine.changeState(this.gameObject,true);
        }
        else
        {
            channelMachine.changeState(this.gameObject, false);
        }
    }

    public void ActualizeFeedback(float _powerRatio)
    {
        if(_powerRatio < 1)
        {
            GetComponent<MeshRenderer>().material = materials[0];
            _targety = _powerRatio;
        }
        if(_powerRatio == 1)
        {
            GetComponent<MeshRenderer>().material = materials[2];
            _targety = 1;
        }
        if (_powerRatio > 1)
        {
            GetComponent<MeshRenderer>().material = materials[1];
            _targety = 0;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ChannelPillarScript))]
public class ChannelPillarScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("ForceRefresh", GUILayout.Width(363)))
        {
            ChannelPillarScript script = (ChannelPillarScript)target;
            script.ActualizePower();
        }
    }
}
#endif
