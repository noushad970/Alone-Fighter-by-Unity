
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with player
public class SingleMeleeAttack : MonoBehaviour
{
    [Header("Fist Fight")]
    public float timer = 0f;
    public int SingleMeleeVal;
    public Animator anim;
    [Header("Give Damage to Enemy")]
    public Transform attackArea;
    public float giveDamage = 20f;
    public float attackRadius;
    public LayerMask knightLayer;

    
    public PlayerScript player;
    public AudioSetup audioSetup;
    public AudioSetup audioSetup2;
    float timerbtAttack = 1.5f;

    // Update is called once per frame
    void Update()
    {  
      SingleHandMeleeAttackMode();
    }
    void Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, knightLayer);
        foreach (Collider knight in hitKnight)
        {
            KnightAI knightAI = knight.GetComponent<KnightAI>();
            if (knightAI != null)
            {

                knightAI.takeDamage(giveDamage);
                
            }

        }
        foreach (Collider knight2 in hitKnight)
        {
            Enemy2 knightAI2 = knight2.GetComponent<Enemy2>();

            if (knightAI2 != null)
            {
                knightAI2.takeDamage(giveDamage);
                
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackArea == null) { return; }
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }
    void SingleHandMeleeAttackMode()
    {
        if (Input.GetMouseButtonDown(0) && Inventory.weaponIn1Hand && timerbtAttack==1.5f)
        {
            SingleMeleeVal = Random.Range(1, 4);

            if (SingleMeleeVal == 1 && Input.GetMouseButtonDown(0))
            {

                Attack();
                StartCoroutine(MeleeAttack1());

            }
            if (SingleMeleeVal == 2 && Input.GetMouseButtonDown(0))
            {

                Attack();
                StartCoroutine(MeleeAttack2());
            }
            if (SingleMeleeVal == 3 && Input.GetMouseButtonDown(0))
            {
                Attack();
                StartCoroutine(MeleeAttack3());
            }
            if (SingleMeleeVal == 4 && Input.GetMouseButtonDown(0))
            {
                Attack();
                StartCoroutine(MeleeAttack4());
            }
            StartCoroutine(timers());
        }
        
        
    }
    IEnumerator timers()
    {
        timerbtAttack = 0f;
        yield return new WaitForSeconds(1.5f);
        timerbtAttack = 1.5f;
    }
    public static bool isAttackingWithSword=false;
    IEnumerator MeleeAttack1()
    {
        isAttackingWithSword = true;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = true;
        anim.SetBool("SingleMeleeAttack1", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("SingleMeleeAttack1", false);
        isAttackingWithSword = false;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator MeleeAttack2()
    {
        isAttackingWithSword = true;
        // isFistAnimated = true;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SingleMeleeAttack2", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("SingleMeleeAttack2", false);
        isAttackingWithSword = false;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator MeleeAttack3()
    {
        isAttackingWithSword = true;
        //isFistAnimated = true;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SingleMeleeAttack3", true);
        yield return new WaitForSeconds(1.1f);
        anim.SetBool("SingleMeleeAttack3", false);
        isAttackingWithSword = false;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator MeleeAttack4()
    {
        isAttackingWithSword = true;
        // isFistAnimated = true;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SingleMeleeAttack4", true);
        yield return new WaitForSeconds(3.05f);
        anim.SetBool("SingleMeleeAttack4", false);
        isAttackingWithSword = false;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    
}
