using System;
using UnityEngine;

public class HurdyGurdyLever : MonoBehaviour
{
    private bool isMoving = false;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OnVelocityChanged();
    }
    
    private void OnVelocityChanged()
    {
        var currentAngularVelocity = rigidBody.angularVelocity;
        var currentValue = currentAngularVelocity.z;
        Debug.Log(currentAngularVelocity);
        if (!currentAngularVelocity.Equals(Vector3.zero) && Math.Abs(currentValue) > 0.1f && currentValue > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }
}
