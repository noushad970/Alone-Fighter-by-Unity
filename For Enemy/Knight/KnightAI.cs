using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script will attach with the walking enemy
public class KnightAI : MonoBehaviour
{
    [Header("Character Info")]
    public float AIMovingSpeed;
    public float AITurningSpeed = 300f;
    public float AIStopSpeed = 1f;
    public float RunningSpeed;
    public float currentMovingSpeed;
    public float MaxHealth = 120f;
    public float currentHealth;

    [Header("Destination Var")]
    public Vector3 AIDestination;
    public bool AIDestinationReached;

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
        currentMovingSpeed = AIMovingSpeed;
        currentHealth = MaxHealth;
        playerBody = GameObject.Find("Player");
    }
    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);
        if (!playerInvisionRadius && !playerInAttackRadius)
        {
            anim.SetBool("Idle", false);
            walk();
        }
        if (playerInvisionRadius && !playerInAttackRadius)
        {
            anim.SetBool("Idle", false);
            ChasePlayer();
        }
        if (playerInAttackRadius && playerInvisionRadius)
        {
            anim.SetBool("Idle", true);
            SingleMeleeModes();
            
        }
    }
    public void walk()
    {
        currentMovingSpeed = AIMovingSpeed;
        if (transform.position != AIDestination)
        {
            Vector3 AIDestinationDirection = AIDestination - transform.position;
            AIDestinationDirection.y = 0;
            float AIDestinationDistance = AIDestinationDirection.magnitude;
            if (AIDestinationDistance >= AIStopSpeed)
            {
                //turning
                AIDestinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(AIDestinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, AITurningSpeed * Time.deltaTime);
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);

                //moving AI
                transform.Translate(Vector3.forward * currentMovingSpeed * Time.deltaTime);
            }
            else
            {
                AIDestinationReached = true;
            }


        }
    }
    void SingleMeleeModes()
    {
        if (!previouslyAttack)
        {
            singleMeleeVal = Random.Range(1, 2);
        }
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
    public void LocateDestination(Vector3 destination)
    {
        this.AIDestination = destination;
        AIDestinationReached = false;
    }

    public void takeDamage(float amount)
    {
        anim.SetTrigger("GetHit");
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void ChasePlayer()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Run", true);
        anim.SetBool("Attack", false);
        currentMovingSpeed = RunningSpeed;
        transform.position += transform.forward * currentMovingSpeed * Time.deltaTime;
        transform.LookAt(playerBody.transform);
    }
    void Die()
    {
        anim.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<CapsuleCollider
            >().enabled = false;
    }
    void AttackPlayer()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackArea.position, attackingRadius, playerLayer);
        foreach (Collider player in hitPlayer)
        {
           PlayerScript playerScript=player.GetComponent<PlayerScript>();
            if(playerScript != null)
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
        AIMovingSpeed = 0f;
        RunningSpeed = 0f;
        yield return new WaitForSeconds(.2f);
        anim.SetBool("Attack1", false);
        AIMovingSpeed = 1f;
        RunningSpeed = 2f;
    }
    IEnumerator Attack2()
    {
        anim.SetBool("Attack2", true);
        AIMovingSpeed = 0f;
        RunningSpeed = 0f;
        yield return new WaitForSeconds(.2f);
        anim.SetBool("Attack2", false);
        AIMovingSpeed = 1f;
        RunningSpeed = 2f;
    }

    private void ActiveAttack()
    {
        previouslyAttack = false;
    }

}
