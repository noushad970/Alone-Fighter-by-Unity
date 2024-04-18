using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script will attach with the player

public class Inventory : MonoBehaviour
{
    [Header("Weapon 1 Slot")]
    public GameObject weapon1;
    public bool isWeapon1Picked=false;
    public bool isWeapon1Active=false;
    public SingleMeleeAttack SMAS;

    public bool fistFightMode=false;

    [Header("Weapon 2 Slot")]
    public GameObject weapon2;
    public bool isWeapon2Picked = false;
    public bool isWeapon2Active = false;
    public RifleControl rifle;

    [Header("Weapon 3 Slot")]
    public GameObject weapon3;
    public bool isWeapon3Picked = false;
    public bool isWeapon3Active = false;
    public GranadeThrowing granadeThrowing;

    [Header("Scripts")]
    public FistFight fistFight;
    public PlayerScript playerScript;
    public Animator anim;
    public GameManager GM;
    public AudioSetup audioSetup;
    [Header("Weapon UI")]
    public static bool weaponIn1Hand;
    public static bool weaponIn2Hand;
    public static bool weaponIn3Hand;
    [Header("Current Weapon UI")]
    public GameObject fistInHand;
    public GameObject pistolInHand;
    public GameObject swordInHand;
    public GameObject granadeInHand;
    public WeaponsMenu WM;


    private void Update()
    {
        weaponIn1Hand = isWeapon1Active;
        weaponIn2Hand = isWeapon2Active;
        weaponIn3Hand = isWeapon3Active;
        if(Input.GetMouseButtonDown(0) && !isWeapon1Active && !isWeapon2Active && !isWeapon3Active)
        {
            fistFightMode = true;
            isRifleActive();
        }
        if(Input.GetKeyDown("1") && !isWeapon1Active && !isWeapon2Active && !isWeapon3Active && isWeapon1Picked)
        {
            isWeapon1Active = true;
            swordInHand.SetActive(true);
            pistolInHand.SetActive(false);
            fistInHand.SetActive(false);
            granadeInHand.SetActive(false );
            audioSetup.playDrawSwordSound();
            isRifleActive();
        }
        else if(Input.GetKeyDown("1") && isWeapon1Active)
        {
            isWeapon1Active=false;
            swordInHand.SetActive(false);
            pistolInHand.SetActive(false);
            fistInHand.SetActive(true);
            granadeInHand.SetActive(false);
            isRifleActive();
        }

        if (Input.GetKeyDown("2") && !isWeapon1Active && !isWeapon2Active && !isWeapon3Active && isWeapon2Picked)
        {
            isWeapon2Active = true;
            swordInHand.SetActive(false);
            pistolInHand.SetActive(true);
            fistInHand.SetActive(false);
            granadeInHand.SetActive(false);
            isRifleActive();
        }
        else if (Input.GetKeyDown("2") && isWeapon2Active)
        {
            isWeapon2Active = false;

            swordInHand.SetActive(false);
            pistolInHand.SetActive(false);
            fistInHand.SetActive(true);
            granadeInHand.SetActive(false);
            isRifleActive();
        }

        if (Input.GetKeyDown("3") && !isWeapon1Active && !isWeapon2Active && !isWeapon3Active && isWeapon3Picked)
        {
            isWeapon3Active = true;

            swordInHand.SetActive(false);
            pistolInHand.SetActive(false);
            fistInHand.SetActive(false);
            granadeInHand.SetActive(true);
            isRifleActive();
        }
        else if (Input.GetKeyDown("3") && isWeapon3Active )
        {
            isWeapon3Active = false;

            swordInHand.SetActive(false);
            pistolInHand.SetActive(false);
            fistInHand.SetActive(true);
            granadeInHand.SetActive(false);
            isRifleActive();
        }

        if (isWeapon3Active && GM.numberOfGranade <= 0)
        {

            isWeapon3Active = false;
            granadeInHand.SetActive(false) ;
            fistInHand.SetActive(true);
            isRifleActive();
        }


        if (Input.GetKeyDown("4") && !isWeapon1Active && !isWeapon2Active && !isWeapon3Active && GM.numberOfhealth>0 && playerScript.presentHealth<100)
        {
            StartCoroutine(IncreaseHealth());
        }
        if (Input.GetKeyDown("5") && !isWeapon1Active && !isWeapon2Active && !isWeapon3Active && GM.numberOfEnergy > 0 && playerScript.presentEnergy<90)
        {
            StartCoroutine(IncreaseEnergy());
        }
        
    }
    void isRifleActive()
    {
        if(fistFightMode==true)
        {
            fistFight.GetComponent<FistFight>().enabled = true;
        }
        if(isWeapon1Active==true)
        {
            StartCoroutine(weapon1Go());
           SMAS.GetComponent<SingleMeleeAttack>().enabled = true;
            anim.SetBool("SingleHandMeleeActive", true);
        }
        if (isWeapon1Active == false)
        {
            StartCoroutine(weapon1Go());
            SMAS.GetComponent<SingleMeleeAttack>().enabled = false;
            anim.SetBool("SingleHandMeleeActive", false);
        }

        if (isWeapon2Active == true)
        {
            StartCoroutine(weapon2Go());
            rifle.GetComponent<RifleControl>().enabled = true;
            anim.SetBool("RifleActive", true);
        }
        if (isWeapon2Active == false)
        {
            StartCoroutine(weapon2Go());
            rifle.GetComponent<RifleControl>().enabled = false;
            anim.SetBool("RifleActive", false);
        }

        if (isWeapon3Active == true)
        {
            StartCoroutine(weapon3Go());
            granadeThrowing.GetComponent<GranadeThrowing>().enabled = true;
           
        }
        if (isWeapon3Active == false)
        {
            StartCoroutine(weapon3Go());
            granadeThrowing.GetComponent<GranadeThrowing>().enabled = false;
          
        }


    }
    IEnumerator weapon1Go()
    {
        if(!isWeapon1Active)
        {
            weapon1.SetActive(false);
        }
        yield return new WaitForSeconds(.1f);
        if(isWeapon1Active==true)
        {
            weapon1.SetActive(true);
        }
    }
    IEnumerator weapon2Go()
    {
        if (!isWeapon2Active)
        {
            weapon2.SetActive(false);
        }
        yield return new WaitForSeconds(.1f);
        if (isWeapon2Active == true)
        {
            weapon2.SetActive(true);
        }
    }
    IEnumerator weapon3Go()
    {
        if (!isWeapon3Active)
        {
            weapon3.SetActive(false);
        }
        yield return new WaitForSeconds(.1f);
        if (isWeapon3Active == true)
        {
            weapon3.SetActive(true);
        }
    }
    IEnumerator IncreaseHealth()
    {
        anim.SetBool("Drink", true);
        playerScript.movementSpeed = 0f;
        yield return new WaitForSeconds(2f);
        playerScript.movementSpeed = playerScript.slowRunSpeed;
        GM.numberOfhealth -= 1;
        anim.SetBool("Drink", false);
        playerScript.presentHealth = playerScript.PlayerHealth;
        playerScript.healthBar.GiveFullHealth(playerScript.PlayerHealth);
    }
    IEnumerator IncreaseEnergy()
    {
        anim.SetBool("Drink", true);
        playerScript.movementSpeed = 0f;
        yield return new WaitForSeconds(2f);
        playerScript.movementSpeed = playerScript.slowRunSpeed;
        anim.SetBool("Drink", false);
        playerScript.presentEnergy = 100f;
        GM.numberOfEnergy -= 1;
        playerScript.energyBar.GiveFullEnergy(100f);
    }
}
