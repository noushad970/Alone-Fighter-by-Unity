using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with player
public class FistFight : MonoBehaviour
{
    [Header("Fist Fight")]
    public float timer = 0f;
    public int fistfightVal;
    public Animator anim;
    public Inventory inventory;
    [Header("Give Damage to Enemy")]
    public Transform attackArea;
    public float giveDamage = 20f;
    public float attackRadius;
    public LayerMask knightLayer;
    float timeBTAttack = 2f;


    [SerializeField] Transform leftHandPunch;
    [SerializeField] Transform RightHandPunch;
    [SerializeField] Transform LeftLegKick;
    [SerializeField] Transform RightLegKick;
    public PlayerScript player;
    [SerializeField] float timerCoolDown = 1f;
    public AudioSetup audioSetup;



    // Update is called once per frame
    void Update()
    {
            if (!Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
            }
            else
            {
                anim.SetBool("FistFightActive", true);
                player.movementSpeed = player.slowRunSpeed;

                FistFightMode();
                timer = 0f;
            }
            if (timer >= 5f)
            {
                player.movementSpeed = player.slowRunSpeed;
                anim.SetBool("FistFightActive", false);
            inventory.fistFightMode = false;
            timer = 0f;
            this.gameObject.GetComponent<FistFight>().enabled = false;  
            }

            timerCoolDown-=Time.deltaTime;
        
        



    }
    bool touchBody=false;
    void Attack()
    {
        Collider[] hitKnight=Physics.OverlapSphere(attackArea.position, attackRadius,knightLayer);
        foreach(Collider knight in hitKnight)
        {  
            KnightAI knightAI = knight.GetComponent<KnightAI>();

            if (knightAI != null)
            {
                knightAI.takeDamage(giveDamage);
                touchBody = true;
            }

        }
        foreach (Collider knight2 in hitKnight)
        {
            Enemy2 knightAI2 = knight2.GetComponent<Enemy2>();

            if (knightAI2 != null)
            {
                knightAI2.takeDamage(giveDamage);
                touchBody = true;
            }

        }

    }
    private void OnDrawGizmosSelected()
    {
        if(attackArea == null) { return; }
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }
    
    IEnumerator timebtAttack()
    {
        timeBTAttack = 0f;
        yield return new WaitForSeconds(1.5f);
        timeBTAttack = 2f;
    }
    void FistFightMode()
    {
        if(Input.GetMouseButtonDown(0) && timerCoolDown<=0 && !Inventory.weaponIn1Hand && !Inventory.weaponIn2Hand && !Inventory.weaponIn3Hand && timeBTAttack==2f)
        {
            fistfightVal = Random.Range(1, 4);
            if (fistfightVal == 1)
            {
                attackArea = RightHandPunch;
                attackRadius = 1.5f;
                if (touchBody)
                {
                    audioSetup.playFistPunch1Sound();
                    touchBody = false;
                }
                else if(!touchBody)
                    audioSetup.playfistAttackEmptySound();
                Attack();
                StartCoroutine(singleFist());

            }
           
            if (fistfightVal == 3)
            {
                if (touchBody)
                {
                    audioSetup.playFistPunch1Sound();
                    touchBody = false;
                }
                else
                    audioSetup.playfistAttackEmptySound();
                attackArea = RightLegKick;
                attackRadius = 1.5f;
                Attack();
                StartCoroutine(SingleKick());
            }
            if (fistfightVal == 2)
            {
                if (touchBody)
                {
                    audioSetup.playFistPunch2Sound();
                    touchBody = false;
                }
                else
                    audioSetup.playfistAttackEmptySound();
                attackArea = RightLegKick;
                attackRadius = 1.5f;
                Attack();
                StartCoroutine(swingKick());
            }
            if (fistfightVal == 4)
            {
                if (touchBody)
                {
                    audioSetup.playFistPunch1Sound();
                    touchBody = false;
                }
                else
                    audioSetup.playfistAttackEmptySound();
                attackArea = RightHandPunch;
                attackRadius = 1.5f;
                Attack();
                StartCoroutine(punch2());
            }
            StartCoroutine(timebtAttack());
        }
        
        
    }
    IEnumerator singleFist()
    {
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
       // isFistAnimated = true;
        anim.SetBool("SingleFist", true);
        yield return new WaitForSeconds(1.05f);
        timerCoolDown = 1f;
        anim.SetBool("SingleFist", false);
        player.movementSpeed = player.slowRunSpeed;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    
    IEnumerator SingleKick()
    {
        //isFistAnimated = true;
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SingleKick", true);
        yield return new WaitForSeconds(1f);
        timerCoolDown = 1f;
        anim.SetBool("SingleKick", false);
        player.movementSpeed = player.slowRunSpeed;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator punch2()
    {
        // isFistAnimated = true;
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("Punch2", true);
        yield return new WaitForSeconds(2f);
        timerCoolDown = 1f;
        anim.SetBool("Punch2", false);
        player.movementSpeed = player.slowRunSpeed;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator swingKick()
    {
        //     isFistAnimated = true;
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SwingKick", true);
        yield return new WaitForSeconds(1.1f);
        timerCoolDown = 1f;
        anim.SetBool("SwingKick", false);
        player.movementSpeed = player.slowRunSpeed;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
}
