using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerScript : MonoBehaviour
{
    [Header("PlayerMovement")]
    public float movementSpeed = 3f;
    public MainCameraController MCC;
    public float rotSpeed = 600f;
    Quaternion requiredRotation;

    [Header("Player Animator")]
    public Animator animator;

    [Header("Gravity & Collision")]
    bool playerControl = true;
    public CharacterController cC;
    [SerializeField] float surfaceCheckRadius = 0.1f;
    public Vector3 surfaceCheckOffset;
    public LayerMask surfaceLayer;
    bool OnSurface;
    [SerializeField] float fallingSpeed;
    [SerializeField] Vector3 moveDir;
    //second
    public float gravity = -9.81f;
    float animationMoveSpeed;
    public Transform surfacecheck;//is grounded
  //  bool OnSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    Vector3 velocity;
    private void Update()
    {
        Surface();
        /*if (OnSurface)
        {
            fallingSpeed = -0.5f;
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * movementSpeed;
        velocity.y = fallingSpeed;*/
        playerMovement();
       // SurfaceCheck();

        Debug.Log("player status: " + OnSurface);
    }
    void playerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;
        var movementDirection = MCC.flatRotation * movementInput;
        cC.Move(movementDirection * movementSpeed * Time.deltaTime);
        cC.Move(velocity * Time.deltaTime);
        if (movementAmount > 0)
        {
            requiredRotation = Quaternion.LookRotation(movementDirection);
        }
        movementDirection = moveDir;
         transform.rotation=Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed*Time.deltaTime);
        animator.SetFloat("MovementValue", movementAmount,0.2f,Time.deltaTime);
    }
    public void setControl(bool hasControl)
    {
        this.playerControl = hasControl;
        cC.enabled = hasControl;
        if (!hasControl)
        {
            animator.SetFloat("MovementValue", 0f);

        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);
    }
    void Surface()
    {
        OnSurface = Physics.CheckSphere(surfacecheck.position, surfaceDistance, surfaceMask);
        if (OnSurface && velocity.y < 0)
            velocity.y = -2f;
        //gravity
        velocity.y += gravity * Time.deltaTime;
        //cC.Move(velocity * Time.deltaTime);
    }

}

//player movement extra
/*
 [Header("Player Movement")]
    public float movementSpeed = 5f;
    public float rotSpeed = 450f;
    Quaternion requiredRotation;
    bool playerControl = true;
    public MainCameraController MCC;

    [Header("Player Animator")]
    public Animator animator;
    public CharacterController cC;

    [Header("Player Collison and Gravity")]
    [SerializeField]float surfaceCheckRadius = 0.1f;
    public Vector3 surfaceCheckOffset;
    public LayerMask surfaceLayer;
    bool OnSurface;
    [SerializeField] float fallingSpeed;
    [SerializeField] Vector3 moveDir;
    private void Update()
    {
        if(OnSurface)
        {
            fallingSpeed = -0.5f;
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * movementSpeed;
        velocity.y = fallingSpeed;
        PlayerMovement();
        SurfaceCheck();
    }
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;
        var movementDirection = MCC.flatRotation * movementInput;
        cC.Move(movementDirection * movementSpeed * Time.deltaTime);

        if (movementAmount > 0)
        {
          //  transform.position += movementDirection * movementSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(movementDirection);
        }
        movementDirection = moveDir;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

        animator.SetFloat("MovementValue", movementAmount, 0.1f, Time.deltaTime);
    }

    void SurfaceCheck()
    {
        OnSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius, surfaceLayer);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);
    }
    public void setControl(bool hasControl)
    {
        this.playerControl = hasControl;
        cC.enabled = hasControl;
        if(!hasControl)
        {
            animator.SetFloat("MovementValue", 0f);
            
        }

    }

 */






/*
   [Header("PlayerMovement")]
    public float playerSpeed = 1.1f;
    public float playerSprint = 5f;
    public bool ismoving = false;
    bool playerControl = true;

    [Header("Player Animation & Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;
    float animationMoveSpeed;

    [Header("Player Jumping and velocity")]
    public float turnCalmTime = 0.1f;
    private float turnCalmVelocity;
    Vector3 velocity;
    public Transform surfacecheck;//is grounded
    bool OnSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    public LayerMask ObstacleLayer;
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
        playerMove();
        //  Jump();
        sprint();


        if (!playerControl)
        {
            return;
        }

        Surface();
        animator.SetFloat("MovementValue", animationMoveSpeed, 0.1f, Time.deltaTime);

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

        if (Input.GetKey(KeyCode.DownArrow) && OnSurface)
        {
            playerSpeed = -1.1f;
        }
        else
            playerSpeed = 1.1f;
        float horizontal_move = Input.GetAxisRaw("Horizontal");
        float Vertical_move = Input.GetAxisRaw("Vertical");
        float moveSpeed = Mathf.Clamp01(Mathf.Abs(horizontal_move) + Mathf.Abs(Vertical_move));

        Vector3 direction = new Vector3(horizontal_move, 0f, Vertical_move).normalized;

        if (direction.magnitude >= 0.1f)
        {

            ismoving = true;
            moving = true;
            animationMoveSpeed = moveSpeed / 2;
            // handgun2.ismoving = true;

            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movedirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            cC.Move(movedirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {

            ismoving = false;
            moving = false;
            animationMoveSpeed = 0f;
        }

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && OnSurface)
        {
            velocity.y = Mathf.Sqrt(jumrange * -2 * gravity);
            animator.SetTrigger("Jump");


        }
        else
        {
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
                animationMoveSpeed = moveSpeed;

                ismoving = true;
                running = true;
            }
            else
            {
                animationMoveSpeed = 0;
                running = false;
                ismoving = false;
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

    public void RotateTowardsTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f, ObstacleLayer); // Adjust the radius as needed
        if (colliders.Length > 0)
        {
            Vector3 direction = (colliders[0].transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, ObstacleLayer * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("No target objects found!");
        }
    }
    public void setControl(bool hasControl)
    {
        this.playerControl = hasControl;
        cC.enabled = hasControl;
        if (!hasControl)
        {
            animator.SetFloat("MovementValue", 0f);

        }

    }
 */