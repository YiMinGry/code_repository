using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMovementWithRandomY : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 2f;
    public float lookAheadDistance = 1f;
    public float rotationSpeed = 5f;
    public float randomYMin = 0f;
    public float randomYMax = 5f;

    private int currentWaypointIndex = 0;

    void Start()
    {
        RandomizeWaypointsY();
    }

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogWarning("웨이포인트가 설정되지 않았습니다!");
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= lookAheadDistance)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
                RandomizeWaypointsY();
            }
        }
    }

    private void RandomizeWaypointsY()
    {
        foreach (Transform waypoint in waypoints)
        {
            Vector3 newPosition = waypoint.position;
            newPosition.y = Random.Range(randomYMin, randomYMax);
            waypoint.position = newPosition;
        }
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);

        if (Application.isPlaying && waypoints.Count > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookAheadDistance);
        }
    }
}
