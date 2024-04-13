using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWayPointNavigator : MonoBehaviour
{
    [Header("AI Character")]
    KnightAI character;
    public WayPoint currentWaypoint;
    int direction;

    private void Awake()
    {
        character = GetComponent<KnightAI>();
    }

    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        character.LocateDestination(currentWaypoint.GetPosition());
    }
    // Update is called once per frame
    void Update()
    {
        if (character.AIDestinationReached)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWayPoint;

            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWayPoint;

            }
            character.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}
