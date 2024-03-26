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
    [SerializeField] NewParkourAction jumpDownParkourAction;
    private void Update()
    {
        var hitData = environmentChecker.CheckObstacle();
        if (Input.GetButton("Jump") && !PlayerInAction)
        {
            
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
       if(playerScript.playerOnledge && !PlayerInAction && !hitData.hitFound && Input.GetButton("Jump"))
        {
            if(playerScript.LedgeInfo.angle<=50)
            {
                //slide changes made here: if player is on the ledge than player will stop.only jump down if i press jump key
                // playerScript.movementSpeed = 0f;
                // animator.SetFloat("MovementValue", 0f,0.1f,Time.deltaTime);
                // playerScript.movementSpeed = 3f;

                // if (Input.GetButton("Jump")) {
                //    animator.CrossFade("JumpingDown", .1f);
                //   }
                animator.CrossFade("JumpingDown", .1f);
                playerScript.playerOnledge = false;
                //StartCoroutine(PerformParkourAction(jumpDownParkourAction));
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
            Debug.Log("Animation Name is Incorrect.the name is: "+action.AnimationName+" Other name: " +animator.name);
        }
        //yield return new WaitForSeconds(animationState.length);

        float timercounter = 0f;
        while (timercounter <= animationState.length)
        {
            timercounter += Time.deltaTime;
            //make player to look toward to the obstacle
            if(action.LookAtObstacle)
            {
                transform.rotation= Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, playerScript.rotSpeed*Time.deltaTime);
            }
            if(action.AllowTargetMatching)
            {
                compareTarget(action);
            }

            if (animator.IsInTransition(0) && timercounter > 0.4f) break;
           yield return null;
        }
        yield return new WaitForSeconds(action.ParkourActionDelay);
        playerScript.setControl(true);
        PlayerInAction =false;
        
    }
    void compareTarget(NewParkourAction action)
    {
        animator.MatchTarget(action.comparePosition,transform.rotation,action.CompareBodyPart,new MatchTargetWeightMask(action.ComparePositionWeight,0),action.CompareStartTime,action.CompareEndTime);
    }
    
}
