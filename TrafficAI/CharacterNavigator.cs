using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour
{
    [Header("Character Info")]
    public float AIMovingSpeed;
    public float AITurningSpeed = 300f;
    public float AIStopSpeed = 1f;

    [Header("Destination Var")]
    public Vector3 AIDestination;
    public bool AIDestinationReached;
    private void Update()
    {
        walk();
    }
    public void walk()
    {
        if(transform.position!=AIDestination)
        {
            Vector3 AIDestinationDirection=AIDestination-transform.position;
            AIDestinationDirection.y = 0;
            float AIDestinationDistance = AIDestinationDirection.magnitude;
            if(AIDestinationDistance>=AIStopSpeed)
            {
                //turning
                AIDestinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(AIDestinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, AITurningSpeed * Time.deltaTime);


                //moving AI
                transform.Translate(Vector3.forward * AIMovingSpeed * Time.deltaTime);
            }
            else
            {
                AIDestinationReached=true;
            }


        }
    }
    public void LocateDestination(Vector3 destination)
    {
        this.AIDestination=destination;
        AIDestinationReached=false;
    }
}
