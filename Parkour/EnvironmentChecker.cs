using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentChecker : MonoBehaviour
{
    //we must create a layer name obstacleLayer. this layer will be use to detect a player hit or not hit on that obstacle layer.we will add the layer name to the obstacle and layer mask to the player.
    public Vector3 rayoffset = new Vector3(0, 0.2f, 0);//position from where the ray will be create.from the fit
    public float raylength = 0.9f; //minimum distance between player and the object when raycast will work
    public LayerMask obstacleLayer;
    public float heightRayLength = 5f;
    
    public ObstacleInfo CheckObstacle()
    {
        var hitdata= new ObstacleInfo();
        var rayOrigin = transform.position + rayoffset;

        hitdata.hitFound = Physics.Raycast(rayOrigin, transform.forward, out  hitdata.hitInfo, raylength, obstacleLayer);
        Debug.DrawRay(rayOrigin, transform.forward * raylength, (hitdata.hitFound) ? Color.red : Color.green);
        if(hitdata.hitFound )
        {
            var heightOrigin = hitdata.hitInfo.point + Vector3.up * heightRayLength;
            hitdata.HeighthitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitdata.HeightInfo, heightRayLength, obstacleLayer);
            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitdata.HeighthitFound) ? Color.blue : Color.green);


        }
        return hitdata;
    }


}
public struct ObstacleInfo
{
    public bool hitFound;
    public RaycastHit hitInfo;
    public bool HeighthitFound;
    public RaycastHit HeightInfo;


}

