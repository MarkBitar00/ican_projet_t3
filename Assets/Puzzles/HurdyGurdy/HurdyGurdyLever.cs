using UnityEngine;

public class HurdyGurdyLever : MonoBehaviour
{
    public bool isMoving = false;
    
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        OnPositionChanged();
    }

    private void OnPositionChanged()
    {
        var isSleeping = GetComponent<Rigidbody>().IsSleeping();
        if (!isSleeping && lastPosition != transform.position)
        {
            lastPosition = transform.position;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
