using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script will attach with Idle Enemy
public class Enemy2 : MonoBehaviour
{
    [Header("Character Info")]
    public float AITurningSpeed = 300f;
    public float AIStopSpeed = 1f;
    public float RunningSpeed;
    public float currentMovingSpeed;
    public float MaxHealth = 120f;
    public float currentHealth;

    [Header("Knight AI")]
    public GameObject playerBody;
    public LayerMask playerLayer;
    public float visionRadius;
    public bool playerInvisionRadius;
    public float attackRadius;
    public bool playerInAttackRadius;


    [Header("Knight Attack Var")]
    public int singleMeleeVal;
    public Transform attackArea;
    public float giveDamage;
    public float attackingRadius;
    bool previouslyAttack;
    public float timebtwAttack;
    public Animator anim;
  

    private void Start()
    {
       
        currentHealth = MaxHealth;
        playerBody = GameObject.Find("Player");
    }
    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);
        if (!playerInvisionRadius && !playerInAttackRadius)
        {
           
            Idle();
        }
        if (playerInvisionRadius && !playerInAttackRadius)
        {
            anim.SetBool("Idle", false);
            ChasePlayer();
        }
        if (playerInAttackRadius && playerInvisionRadius)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);
            SingleMeleeModes();

        }
    }
    public void Idle()
    {
        anim.SetBool("Run", false);
    }
    void SingleMeleeModes()
    {
        if (!previouslyAttack)
        {
            singleMeleeVal = Random.Range(1, 2);

            if (singleMeleeVal == 1)
            {

                AttackPlayer();
                StartCoroutine(Attack1());

            }
            if (singleMeleeVal == 2)
            {

                AttackPlayer();
                StartCoroutine(Attack2());
            }
        }
        


    }
    

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        StartCoroutine(wait1Sec());
        
    }
    IEnumerator wait1Sec()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("GetHit");
    }
    public void ChasePlayer()
    {
       
        anim.SetBool("Run", true); 
        currentMovingSpeed = RunningSpeed;
        transform.position += transform.forward * currentMovingSpeed * Time.deltaTime;
        transform.LookAt(playerBody.transform);
    }
    void Die()
    {
        anim.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
    void AttackPlayer()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackArea.position, attackingRadius, playerLayer);
        foreach (Collider player in hitPlayer)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.PlayerHitDamage(giveDamage);
               
            }

        }
        previouslyAttack = true;
        Invoke(nameof(ActiveAttack), timebtwAttack);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackArea == null) { return; }
        Gizmos.DrawWireSphere(attackArea.position, attackingRadius);
    }
    IEnumerator Attack1()
    {
        anim.SetBool("Attack1", true);
      
        RunningSpeed = 0f;
        yield return new WaitForSeconds(.2f);
        anim.SetBool("Attack1", false);
       
        RunningSpeed = 2f;
    }
    IEnumerator Attack2()
    {
        anim.SetBool("Attack2", true);
       
        RunningSpeed = 0f;
        yield return new WaitForSeconds(.2f);
        anim.SetBool("Attack2", false);
      
        RunningSpeed = 2f;
    }

    private void ActiveAttack()
    {
        previouslyAttack = false;
    }
}
