using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ReflectiveLaser : MonoBehaviour
{
    public int numberOfReflections;
    public float maxLaserLength;
    
    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit rayHit;
    private Vector3 direction;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        var remainingLength = maxLaserLength;
        for (var i = 0; i < numberOfReflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out rayHit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, rayHit.point);
                remainingLength -= Vector3.Distance(ray.origin, rayHit.point);
                ray = new Ray(rayHit.point, Vector3.Reflect(ray.direction, rayHit.normal));
                if (!rayHit.collider.CompareTag("Mirror")) break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}
