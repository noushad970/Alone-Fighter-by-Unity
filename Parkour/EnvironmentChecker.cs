using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with player
public class EnvironmentChecker : MonoBehaviour
{
    //we must create a layer name obstacleLayer. this layer will be use to detect a player hit or not hit on that obstacle layer.we will add the layer name to the obstacle and layer mask to the player.
    public Vector3 rayoffset = new Vector3(0, 0.2f, 0);//position from where the ray will be create.from the fit
    public float raylength = 0.9f; //minimum distance between player and the object when raycast will work
    public LayerMask obstacleLayer;
    public float heightRayLength = 5f;
    [Header("Check Ledges")]
    [SerializeField] float ledgeRayLength = 11f;
    [SerializeField] float ledgeRayHeightThreshold = 2.5f;//this is the distance between player and the ground if player is not on ground 

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
    public bool CheckLedge(Vector3 moveDirection,out LedgeInfo ledgeInfo)
    {
        ledgeInfo= new LedgeInfo();
        if(moveDirection== Vector3.zero) return false;
        //slite change made. ledgeOriginOffset change from 0.5 to .15f;

        float ledgeOriginOffset = 0.15f;//this value is the side distance from where the player will be in the ledge.
        var ledgeOrigin= transform.position+moveDirection*ledgeOriginOffset+Vector3.up;
        if(Physics.Raycast(ledgeOrigin,Vector3.down,out RaycastHit hit,ledgeRayLength,obstacleLayer))
        {
            Debug.DrawRay(ledgeOrigin, Vector3.down * ledgeRayLength, Color.blue);
            var surfaceRayCastOrigin = transform.position + moveDirection - new Vector3(0, 0.1f, 0);
            if(Physics.Raycast(surfaceRayCastOrigin, -moveDirection, out RaycastHit SurfaceHit, 2, obstacleLayer))
            { 
                float ledgeHeigth = transform.position.y - hit.point.y;
                
                if (ledgeHeigth >ledgeRayHeightThreshold)
                {
                    //Debug.Log("ledgeRayHeightThreshold: " + ledgeRayHeightThreshold);
                    ledgeInfo.angle=Vector3.Angle(transform.forward, SurfaceHit.normal);
                    ledgeInfo.surfaceHit=SurfaceHit;
                   // Debug.Log("LedgeHeight: " + ledgeHeigth + " LedgeAngle: " + ledgeInfo.angle);
                    ledgeInfo.height=ledgeHeigth;
                   // if(ledgeHeigth>1f)
                        return true;

                }
            }
        }
        return false;
    }

}
public struct ObstacleInfo
{
    public bool hitFound;
    public RaycastHit hitInfo;
    public bool HeighthitFound;
    public RaycastHit HeightInfo;


}
public struct LedgeInfo
{
    public float angle;
    public float height;
    public RaycastHit surfaceHit;
}

