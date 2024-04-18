using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script will attach with an empty gameobject named GameManager

public class GameManager : MonoBehaviour
{
    public int numberOfGranade;
    public int numberOfhealth;
    public int numberOfEnergy;
    [Header("Ammo & Mag")]
    public RifleControl rifle;
    public Text rifleAmmoText;
    public Text rifleMagText;
    public Text granadeText;
    public Text granadeSlotText;
    public Text EnergySlotText;
    public Text HealthSlotText;
     public Text healthText;
     public Text energyText;
    [Header("Health & Energy")]
    public GameObject health;
    public GameObject energy;
    private void Update()
    {
        rifleAmmoText.text = "" + rifle.presentAmmo;
        rifleMagText.text = "" + rifle.mag;
        granadeText.text = "" + numberOfGranade;
        granadeSlotText.text = "" + numberOfGranade;
        HealthSlotText.text = "" + numberOfhealth;
        EnergySlotText.text = "" + numberOfEnergy;
        healthText.text = "" + numberOfhealth;
        energyText.text = "" + numberOfEnergy;
        if(numberOfEnergy > 0)
        {
            energy.SetActive(true);
        }
        if (numberOfEnergy <= 0)
            energy.SetActive(false);
        if (numberOfhealth > 0)
        {
            health.SetActive(true);
        }
        if (numberOfhealth <= 0)
            health.SetActive(false);
    }
}
