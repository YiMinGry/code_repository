using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    // 지형
    private float detectionDistance = 10f;
    public LayerMask targetLayer;
    private Vector3 offset = new Vector3(0, 1f, 0);
    private Vector3 rotationOffset = new Vector3(25, 0, 0);
    public RaycastHit hitInfo;

    // 오브젝트
    private float detectionRadius = 3f;
    private float viewAngle = 60;
    public LayerMask targetObjLayer; 
    private Collider[] detectedColliders = new Collider[10];
    private Collider nearObject;

    public bool IsDetect => nearObject != null;

    // 지형 검출
    public void DetectTerrain()
    {
        Vector3 origin = transform.position + offset;
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles + rotationOffset);
        Vector3 direction = rotation * Vector3.forward;

        if (Physics.Raycast(origin, direction, out hitInfo, detectionDistance, targetLayer))
        {
            nearObject = hitInfo.collider;
        }
        else
        {
            nearObject = null;
        }
    }

    // 오브젝트 검출
    public void DetectObject()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, detectedColliders, targetObjLayer);

        if (count > 0)
        {
            float shortestDistance = detectionRadius;
            nearObject = null;

            for (int i = 0; i < detectedColliders.Length; i++)
            {
                Collider col = detectedColliders[i];
                if (col == null) continue;

                Vector3 directionToTarget = (col.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    Vector3 closestPoint = col.ClosestPoint(transform.position);
                    float distance = Vector3.Distance(transform.position, closestPoint);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearObject = col;
                    }
                }
            }
        }
    }


    public void Update()
    {
        DetectTerrain();
        DetectObject();
    }


    public Collider GetNearCollider()
    {
        return nearObject;
    }

    public GameObject GetNearGameObject()
    {
        return nearObject != null ? nearObject.gameObject : null;
    }

    public InteractionObject GetNearInteraction()
    {
        return nearObject != null && nearObject.TryGetComponent(out InteractionObject interaction) ? interaction : null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 origin = transform.position + offset;
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles + rotationOffset);
        Vector3 direction = rotation * Vector3.forward;

        Gizmos.DrawLine(origin, origin + direction * detectionDistance);
        Gizmos.DrawWireSphere(origin + direction * detectionDistance, 0.1f);

        Gizmos.color = Color.blue;
        Vector3 forward = transform.forward * detectionRadius;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * forward;

        int segments = 30;
        Vector3 prevPoint = transform.position + leftBoundary;

        for (int i = 1; i <= segments; i++)
        {
            float segmentAngle = -viewAngle / 2 + (viewAngle / segments) * i;
            Vector3 nextBoundary = Quaternion.Euler(0, segmentAngle, 0) * forward;
            Vector3 nextPoint = transform.position + nextBoundary;

            Gizmos.DrawLine(transform.position, prevPoint);
            Gizmos.DrawLine(prevPoint, nextPoint);
            Gizmos.DrawLine(nextPoint, transform.position);

            prevPoint = nextPoint;
        }
    }
}
