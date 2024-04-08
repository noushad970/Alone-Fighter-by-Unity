using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleControl : MonoBehaviour
{
    [Header("Rifle Things")]
    public Transform shootingArea;
    public float giveDamage = 20f;
    public float shootingRange = 100f;
    public Animator animator;
    [Header("Rifle Ammo & Reload")]
    private int maximumAmmunition = 1;
    public int presentAmmo;
    public int mag;
    public float reloadingTime;
    public bool setReloading;

    private void Start()
    {
        presentAmmo = maximumAmmunition;
    }
    // Update is called once per frame
    void Update()
    {
        if(setReloading)
        {
            return;
        }
        if(presentAmmo<=0 && mag>0 )
        {
            StartCoroutine(Reload());
        }
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("RifleActive", true);
            animator.SetBool("RifleShoot", true);
            shoot();
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            animator.SetBool("RifleShoot", false);
           
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
        }
    }
    IEnumerator Reload()
    {
        setReloading=true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmo = maximumAmmunition;
        setReloading = false;
    }
}
