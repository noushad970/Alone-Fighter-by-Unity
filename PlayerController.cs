using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement")]
    public float playerSpeed = 1.1f;
    public float playerSprint = 5f;
    public bool ismoving = false;

    [Header("Player Animation & Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Jumping and velocity")]
    public float turnCalmTime = 0.1f;
    private float turnCalmVelocity;
    Vector3 velocity;
    public Transform surfacecheck;//is grounded
    bool OnSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    public float jumrange = 1f;

    [Header("Player Health & Things")]
    private float playerHealth = 200f;
    public float presentHealth;
   // public GameObject RayCastShootingArea;
    //public HealthBar healthbar;

    [Header("Player Scripth Camera")]
    public Transform playerCamera;
    public bool isActivePlayer = true;
  //  public PLayer player;

   // public GunSound sound;
    bool moving, running;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        animator = GetComponent<Animator>();
       // healthbar.GiveFullHealth(presentHealth);

    }
    private void Update()
    {

       
        Surface();
        playerMove();
        Jump();
        sprint();
        if (moving == true)
        {

        }
        else if (!moving)
        {

        }
        if (running == true)
        {


        }
        else if (!running)
        {

        }
    }
    void Surface()
    {
        OnSurface = Physics.CheckSphere(surfacecheck.position, surfaceDistance, surfaceMask);
        if (OnSurface && velocity.y < 0)
            velocity.y = -2f;
        //gravity
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
    }
    void playerMove()
    {
       // float moveAmmount = Mathf.Abs(horizontal_move) + Mathf.Abs(Vertical_move);

        if (Input.GetKey(KeyCode.DownArrow) && OnSurface)
        {
            playerSpeed = -1.1f;
        }
        else
            playerSpeed = 1.1f;
        float horizontal_move = Input.GetAxisRaw("Horizontal");
        float Vertical_move = Input.GetAxisRaw("Vertical");
        float moveSpeed = (Mathf.Abs(horizontal_move) + Mathf.Abs(Vertical_move));

        Vector3 direction = new Vector3(horizontal_move, 0f, Vertical_move).normalized;
        
        if (direction.magnitude >= 0.1f)
        {

            ismoving = true;
            moving = true;
            //animator.SetBool("Walk", true);
            //animator.SetBool("Run", false);
            animator.SetFloat("MoveSpeed", moveSpeed,0.2f,Time.deltaTime);

            // handgun2.ismoving = true;

            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movedirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            cC.Move(movedirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("MoveSpeed", 0, 0.2f, Time.deltaTime);
            //animator.SetBool("Walk", false);
            //animator.SetBool("Run1", false);
            ismoving = false;
            // handgun2.ismoving = false;
            moving = false;
        }

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && OnSurface)
        {
            velocity.y = Mathf.Sqrt(jumrange * -2 * gravity);
            //animator.SetBool("IdleAim", false);
            animator.SetTrigger("Jump");


        }
        else
        {
            // animator.SetBool("IdleAim", true);
            animator.ResetTrigger("Jump");


        }

    }
    void sprint()
    {
        if (Input.GetKey(KeyCode.RightShift) && OnSurface && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            float horizontal_move = Input.GetAxisRaw("Horizontal");
            float Vertical_move = Input.GetAxisRaw("Vertical");
            float moveSpeed = Mathf.Clamp01(Mathf.Abs(horizontal_move) + Mathf.Abs(Vertical_move));
            Vector3 direction = new Vector3(horizontal_move, 0f, Vertical_move).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 movedirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                cC.Move(movedirection.normalized * playerSprint * Time.deltaTime);
                animator.SetFloat("MoveSpeed", 1f+moveSpeed, 0.2f, Time.deltaTime);

                ismoving = true;
                //handgun2.ismoving = true;
                running = true;
                //animator.SetBool("Walk1", false);
                //animator.SetBool("Run1", true);
            }
            else
            {
                animator.SetFloat("MoveSpeed", 1f + moveSpeed, 0.2f, Time.deltaTime);
                //animator.SetBool("Walk", false);
                //animator.SetBool("Run", false);
                running = false;
                ismoving = false;
                //handgun2.ismoving = false;
            }
        }
    }
    public void playerHitDamage(float takeDamage)
    {
        if (takeDamage > 0)
        {

        }
        presentHealth -= takeDamage;
       // healthbar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            playerDie();
          //  sound.PlayDeathHuman();

        }
    }
    private void playerDie()
    {

        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
    }


}