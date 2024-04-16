using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with the player
public class RifleControl : MonoBehaviour
{
    [Header("Rifle Things")]
    public Transform shootingArea;
    public float giveDamage = 20f;
    public float shootingRange = 100f;
    public Animator animator;
    [Header("Rifle Ammo & Reload")]
    private int maximumAmmunition = 5;
    public int presentAmmo;
    public int mag;
    public float reloadingTime;
    public bool setReloading;
    bool isMoving=false;
    public PlayerScript playerScript;
    public GameObject crossHair;

    private void Start()
    {
        presentAmmo = maximumAmmunition;
    }
    // Update is called once per frame
    void Update()
    {
        if(animator.GetFloat("MovementValue")>0.0001f)
            isMoving = true;
        else if(animator.GetFloat("MovementValue")<0.0999999f)
            isMoving = false;
        if(setReloading)
        {
            return;
        }
        if(presentAmmo<=0 && mag>0 )
        {
            StartCoroutine(Reload());
        }
        if(Input.GetMouseButtonDown(0) && mag>0 && !isMoving)
        {
            animator.SetBool("RifleActive", true);
            animator.SetBool("RifleShoot", true);
            shoot();
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            animator.SetBool("RifleShoot", false);
           
        }
        if (Input.GetMouseButton(1))
        {
            crossHair.SetActive(true);
            animator.SetBool("RifleAim", true);
        }
        if (!Input.GetMouseButton(1))
        {
            crossHair.SetActive(false);
            animator.SetBool("RifleAim", false);
        }

    }
    void shoot()
    {
        if (mag <= 0)
        {
            return;
        }
        presentAmmo--;
        if (presentAmmo == 0)
        {
            mag--;
        }
    RaycastHit hitInfo;
        if(Physics.Raycast(shootingArea.position, shootingArea.forward, out hitInfo,shootingRange))
        {
            KnightAI knightAI=hitInfo.transform.GetComponent<KnightAI>();
            if(knightAI != null )
            {
                knightAI.takeDamage(giveDamage);
                
            }
            Debug.Log("Hit Info: " + hitInfo.transform.name);
        }
    }
   
    IEnumerator Reload()
    {
        setReloading=true;
        animator.SetFloat("MovementValue", 0f);
        playerScript.movementSpeed = 0f;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmo = maximumAmmunition;
        setReloading = false;
        animator.SetFloat("MovementValue", 0f);
        playerScript.movementSpeed = playerScript.slowRunSpeed;
    }
}
