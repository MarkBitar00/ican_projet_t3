using UnityEngine;

public class InstrumentWithLever : MonoBehaviour
{
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
        if (transform.position != lastPosition)
        {
            Debug.Log("Changed positions");
        }
    }
}
