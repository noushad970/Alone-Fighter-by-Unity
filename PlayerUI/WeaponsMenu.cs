using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    public GameObject weaponsMenuUI;
    public bool weaponsMenuActive=false;
    public GameObject mainCamera;
    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon4StockUI;
    [Header("Potions")]
    public Inventory inventory;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O) && weaponsMenuActive==false)
        {
            weaponsMenuUI.SetActive(true);
            weaponsMenuActive = true;
            mainCamera.GetComponent<MainCameraController>().enabled = false;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.O) && weaponsMenuActive == true)
        {
            weaponsMenuUI.SetActive(false);
            weaponsMenuActive = false;
            mainCamera.GetComponent<MainCameraController>().enabled = true;
            Time.timeScale = 1;
        }
        weaponsCheck();
    }
    void weaponsCheck()
    {
        weapon1.SetActive(true);
        if(inventory.isWeapon1Picked)
        {
            weapon3.SetActive(true);
        }
        if (inventory.isWeapon2Picked)
        {
            weapon2.SetActive(true);
        }
        if (inventory.isWeapon3Picked)
        {
            weapon4.SetActive(true);
            weapon4StockUI.SetActive(true);
        }
        
    }
}
