using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this script will not be attached. its need for creating waypoint
public class WayPoint : MonoBehaviour
{
    [Header("Waypoint Status")]
    public WayPoint previousWayPoint;
    public WayPoint nextWayPoint;

    [Range(0f, 5f)]
    public float waypointWidth;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * waypointWidth / 2f;
        Vector3 maxBound = transform.position - transform.right * waypointWidth / 2f;
        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}
