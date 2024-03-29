using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistFight : MonoBehaviour
{
    [Header("Fist Fight")]
    public float timer = 0f;
    public int fistfightVal;
    public Animator anim;
    [Header("Give Damage to Enemy")]
    public Transform attackArea;
    public float giveDamage = 20f;
    public float attackRadius;
    public LayerMask knightLayer;

    [SerializeField] Transform leftHandPunch;
    [SerializeField] Transform RightHandPunch;
    [SerializeField] Transform LeftLegKick;
    [SerializeField] Transform RightLegKick;
    public PlayerScript player;
    
   

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
                player.movementSpeed = 3f;

                FistFightMode();
                timer = 0f;
            }
            if (timer >= 7f)
            {
                player.movementSpeed = 3f;
                anim.SetBool("FistFightActive", false);
            }

            
        
        



    }
    void Attack()
    {
        Collider[] hitKnight=Physics.OverlapSphere(attackArea.position, attackRadius,knightLayer);
        foreach(Collider knight in hitKnight)
        {  
            KnightAI knightAI = knight.GetComponent<KnightAI>();
            if(knightAI != null)
            {
                knightAI.takeDamage(giveDamage);
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        if(attackArea == null) { return; }
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }
    void FistFightMode()
    {
        if(Input.GetMouseButtonDown(0))
        {
            fistfightVal = Random.Range(1, 5);
        }
        if(fistfightVal==1)
        {
            attackArea = RightHandPunch;
            attackRadius = 0.5f;
            Attack();
            StartCoroutine(singleFist());
            
        }
        if (fistfightVal == 2)
        {
            attackArea = RightHandPunch;
            attackRadius = 0.5f;
            Attack();
            StartCoroutine(DoubleFist());
        }
        if (fistfightVal == 3)
        {
            attackArea = RightLegKick;
            attackRadius = 0.5f;
            Attack();
            StartCoroutine(SingleKick());
        }
        if (fistfightVal == 4)
        {
            attackArea = RightLegKick;
            attackRadius = 0.5f;
            Attack();
            StartCoroutine(swingKick());
        }
        if (fistfightVal == 5)
        {
            attackArea = RightHandPunch;
            attackRadius = 0.5f;
            Attack();
            StartCoroutine(punch2());
        }
    }
    IEnumerator singleFist()
    {
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
       // isFistAnimated = true;
        anim.SetBool("SingleFist", true);
        yield return new WaitForSeconds(1.05f);
        anim.SetBool("SingleFist", false);
        player.movementSpeed = 3f;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
    IEnumerator DoubleFist()
    {
        // isFistAnimated = true;
        player.movementSpeed = 0f;
        anim.SetFloat("MovementValue", 0f);
        anim.SetBool("DoubleFist", true);
        yield return new WaitForSeconds(1.12f);
        anim.SetBool("DoubleFist", false);
        player.movementSpeed = 3f;
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
        anim.SetBool("SingleKick", false);
        player.movementSpeed = 3f;
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
        anim.SetBool("Punch2", false);
        player.movementSpeed = 3f;
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
        anim.SetBool("SwingKick", false);
        player.movementSpeed = 3f;
        anim.SetFloat("MovementValue", 0f);
        // isFistAnimated = false;
    }
}
