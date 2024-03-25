using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScripts : MonoBehaviour
{
    public EnvironmentChecker environmentChecker;
    bool PlayerInAction;
    public Animator animator;
    public PlayerScript playerScript;

    [Header("Parkour Action Area")]
    public List<NewParkourAction> newParkourAction;
    private void Update()
    {
       if(Input.GetButton("Jump") && !PlayerInAction)
        {
            var hitData = environmentChecker.CheckObstacle();
            if (hitData.hitFound)
            {
              //  playerScript.RotateTowardsTarget();
                foreach(var action in newParkourAction)
                {
                    if(action.CheckIfAvailable(hitData,transform))
                    {
                        StartCoroutine(PerformParkourAction(action));
                        break;
                    }
                }
                }
        }
    }
    IEnumerator PerformParkourAction(NewParkourAction action)
    {
        PlayerInAction = true;
        playerScript.setControl(false);
        animator.CrossFade(action.AnimationName, 0.2f);
        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);
        if(!animationState.IsName(action.AnimationName))
        {
            Debug.Log("Animation Name is Incorrect");
        }
        //yield return new WaitForSeconds(animationState.length);

        float timercounter = 0f;
        while (timercounter <= animationState.length)
        {
            timercounter += Time.deltaTime;
            //make player to look toward to the obstacle
            /*if(action.LookAtObstacle)
            {
                transform.rotation= Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, playerScript.rotSpeed*Time.deltaTime);
            }*/
            if(action.AllowTargetMatching)
            {
                compareTarget(action);
            }


           yield return null;
        }
            playerScript.setControl(true);
        PlayerInAction =false;
        
    }
    void compareTarget(NewParkourAction action)
    {
        animator.MatchTarget(action.comparePosition,transform.rotation,action.CompareBodyPart,new MatchTargetWeightMask(action.ComparePositionWeight,0),action.CompareStartTime,action.CompareEndTime);
    }
    
}
