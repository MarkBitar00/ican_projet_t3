using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class LockScript : MonoBehaviour
{
    public HingeJoint _blockedObject;
    public float _newMinLimit;
    public float _newMaxLimit;

    public void modifyJointLimit()
    {
        JointLimits _newLimits = new JointLimits();
        _newLimits.max = _newMaxLimit;
        _newLimits.min = _newMinLimit;
        _blockedObject.limits = _newLimits;
        GameObject key = GetComponent<XRSocketInteractor>().GetOldestInteractableSelected().transform.gameObject;
        key.SetActive(false);
    }
}
