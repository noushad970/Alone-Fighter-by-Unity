using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this need for creating action button

[CreateAssetMenu(menuName = "Parkour Menu/Create new Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [Header("Checking Obstacle Height")]
    [SerializeField] string animationName;
    [SerializeField] string barrierTag;
    [SerializeField] float minimumHeight;
    [SerializeField] float maximumHeight;

    [Header("Rotating to Obstacles")]
    [SerializeField] bool lookAtObstacle;
    [SerializeField] float parkourActionDelay;
    public Quaternion RequiredRotation { get; set; }


    [Header("Target Matching")]
    [SerializeField] float compareStartTime;
    [SerializeField] float compareEndTime;
    [SerializeField] bool allowTargetMatching;
    [SerializeField] AvatarTarget compareBodyPart;
    public Vector3 comparePosition { get; set; }
    [SerializeField] Vector3 comparePositionWeight= new Vector3(0,1,0);


    public bool CheckIfAvailable(ObstacleInfo hitData,Transform player)
    {   //check barrier 
        if(hitData.hitInfo.transform.tag== barrierTag) 
        {
            return true;
        }
        //checkHeight
        
        float checkHeight = hitData.HeightInfo.point.y - player.position.y;
        if(checkHeight < minimumHeight || checkHeight>maximumHeight) { return false; }
        if(lookAtObstacle)
        {
            RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
        }
        if(allowTargetMatching)
        {
            comparePosition = hitData.hitInfo.point;
        }
        return true;
    }
    public bool LookAtObstacle => lookAtObstacle;
    public string AnimationName => animationName;
    public float CompareStartTime => compareStartTime;
    public float CompareEndTime => compareEndTime;
    public bool AllowTargetMatching=>allowTargetMatching;
    public float ParkourActionDelay => parkourActionDelay;
    public AvatarTarget CompareBodyPart => compareBodyPart;
    public Vector3 ComparePositionWeight => comparePositionWeight;
}

