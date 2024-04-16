using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
//this script will attach with player

public class PlayerScript : MonoBehaviour
{
    [Header("Player Health & Energy")]
    public float PlayerHealth=200f;
    public float presentHealth;
    private float PlayerEnergy = 100f;
    public float presentEnergy;
    public HealthBar healthBar;
    public EnergyBar energyBar;
    public GameObject damageIndicator;
    
    [Header("PlayerMovement")]
    
    public float movementSpeed;
    public MainCameraController MCC;
    public float rotSpeed = 600f;
    Quaternion requiredRotation;
    public EnvironmentChecker environmentChecker;
    
    public float walkSpeed = 2f;
    
    public float slowRunSpeed = 4.5f;
    
    public float fastRunSpeed = 5.5f;


    [Header("Player Animator")]
    public Animator animator;
    public AudioSetup audioSetup;

    [Header("Gravity & Collision")]
    bool playerControl = true;
    public CharacterController cC;
    [SerializeField] float surfaceCheckRadius = 0.1f;
    public Vector3 surfaceCheckOffset;
    public LayerMask surfaceLayer;
    bool OnSurface;
    [SerializeField] float fallingSpeed;
    [SerializeField] Vector3 moveDir;
    [SerializeField] Vector3 RequiredMoveDir;
    //public FistFight fistFight;
    public bool playerOnledge { get; set; }
    public LedgeInfo LedgeInfo { get; set; }
    //second
    public float gravity = -9.81f;
    float animationMoveSpeed;
    public Transform surfacecheck;//is grounded
  //  bool OnSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    Vector3 velocity;

    private void Awake()
    {
        presentHealth = PlayerHealth;
        presentEnergy = PlayerEnergy;
        healthBar.GiveFullHealth(presentHealth);
        energyBar.GiveFullEnergy(presentEnergy);
    }

    private void Update()
    {
        if(animator.GetFloat("MovementValue") >=0.9999)
        {
            PlayerEnergyDecrease(0.02f);
        }
        if(presentEnergy<=0)
        {

            movementSpeed = 2f;
            if (!Input.GetButton("Horizontal") || !Input.GetButton("Vertical"))
            {
                animator.SetFloat("MovementValue", 0f);
            }
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                
                animator.SetFloat("MovementValue", 0.5f);
                StartCoroutine(setEnergy());
            }

        }
        
        if(Inventory.weaponIn1Hand || Inventory.weaponIn2Hand && !RifleControl.isReloading)
        {
            if (PlayerEnergy >= 1)
            {

                movementSpeed = 2.3f;
            }
            if (presentEnergy <= 0)
            {
                movementSpeed = 1.5f;
            }
            if (PlayerEnergy >= 1 && Input.GetKey(KeyCode.RightShift) && movementSpeed>0)
            {
                movementSpeed = 3.4f;
                animator.SetFloat("MovementValue", 1.5f);
            }
        }
        else if(!RifleControl.isReloading)
        {
            if (PlayerEnergy >= 1)
            {

                movementSpeed = slowRunSpeed;
            }
            if (presentEnergy <= 0)
            {
                movementSpeed = walkSpeed;
            }
            if (PlayerEnergy >= 1 && Input.GetKey(KeyCode.RightShift) && movementSpeed>0)
            {
                movementSpeed = fastRunSpeed;
                animator.SetFloat("MovementValue", 1.5f);
            }
        }
        if (RifleControl.isReloading)
            movementSpeed = 0f;
        if (OnSurface)
        {

            fallingSpeed = -0.5f;
            velocity = moveDir * movementSpeed;
           playerOnledge= environmentChecker.CheckLedge(moveDir,out LedgeInfo ledgeInfo);
            if(playerOnledge )
            {
                LedgeInfo = ledgeInfo;
                playerLedgeMovement() ;
                Debug.Log("Player On ledge");
            }
            animator.SetFloat("MovementValue", velocity.magnitude / movementSpeed, 0.2f, Time.deltaTime);
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
            velocity = transform.forward * movementSpeed / 2;
        }
        velocity.y = fallingSpeed;

        
        /*        else
                {
                    fallingSpeed += Physics.gravity.y * Time.deltaTime;
                }

                var velocity = moveDir * movementSpeed;
                velocity.y = fallingSpeed;*/
        animator.SetBool("OnSurface", OnSurface);
        playerMovement();
        // SurfaceCheck();
        Surface();
        Debug.Log("player status: " + OnSurface);
    }
    void playerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;
        RequiredMoveDir = MCC.flatRotation * movementInput;
       
            cC.Move(velocity * Time.deltaTime);

        /* if (Input.GetKey(KeyCode.RightShift))
        {
            movementSpeed = fastRunSpeed;
            animator.SetFloat("MovementValue", 1.5f, 0.2f, Time.deltaTime);
        }
        else
            movementSpeed = slowRunSpeed;
       if(OnSurface && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJump", true);
        }
        else
        {
            animator.SetBool("IsJump", false);
        }
        */

        if (movementAmount > 0 && moveDir.magnitude>0.2f)
        {
            requiredRotation = Quaternion.LookRotation(moveDir);
        }
         moveDir= RequiredMoveDir;
         transform.rotation=Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed*Time.deltaTime);
        if(movementAmount>0)
            AudioSetup.isWalking = true;
        else
            AudioSetup.isWalking = false;
       
    }
    void playerLedgeMovement()
    {
        float angle = Vector3.Angle(LedgeInfo.surfaceHit.normal, moveDir);
        if(angle<90)
        {
            velocity = Vector3.zero;
            moveDir = Vector3.zero;
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);
    }
    void Surface()
    {
        OnSurface = Physics.CheckSphere(surfacecheck.position, surfaceDistance, surfaceMask);
    }
    public void PlayerHitDamage(float giveDamage)
    {
        presentHealth-= giveDamage;
        StartCoroutine(showDamage());
        healthBar.SetHealth(presentHealth);
        if(presentHealth <= 0)
        {
            PlayerDie();
        }

    }
    void PlayerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject,1f);
    }
    public void PlayerEnergyDecrease(float EnergyDecrease)
    {
        presentEnergy-= EnergyDecrease;
        energyBar.SetEnergy(presentEnergy);
    }
    IEnumerator setEnergy()
    {
        
        yield return new WaitForSeconds(5f);
        energyBar.GiveFullEnergy(presentEnergy);
        presentEnergy = 100f;
    }
    IEnumerator showDamage()
    {
        yield return new WaitForSeconds(.5f);
        damageIndicator.SetActive(true);
        yield return new WaitForSeconds(.2f);
        damageIndicator.SetActive(false);
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