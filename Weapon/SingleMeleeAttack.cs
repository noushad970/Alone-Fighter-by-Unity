
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



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("SingleHandMeleeActive", true);
           
        }
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
    }
    private void OnDrawGizmosSelected()
    {
        if (attackArea == null) { return; }
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }
    void SingleHandMeleeAttackMode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SingleMeleeVal = Random.Range(1, 4);
        }
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
        
    }
    IEnumerator MeleeAttack1()
    {
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = true;
        anim.SetBool("SingleMeleeAttack1", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("SingleMeleeAttack1", false);
        player.movementSpeed = 3f;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator MeleeAttack2()
    {
        // isFistAnimated = true;
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SingleMeleeAttack2", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("SingleMeleeAttack2", false);
        player.movementSpeed = 3f;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator MeleeAttack3()
    {
        //isFistAnimated = true;
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SingleMeleeAttack3", true);
        yield return new WaitForSeconds(1.1f);
        anim.SetBool("SingleMeleeAttack3", false);
        player.movementSpeed = 3f;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator MeleeAttack4()
    {
        // isFistAnimated = true;
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("SingleMeleeAttack4", true);
        yield return new WaitForSeconds(3.05f);
        anim.SetBool("SingleMeleeAttack4", false);
        player.movementSpeed = 3f;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    
}
