using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
//this script will attach with UI Canvas's Slider rename in HealthBar

public class EnergyBar : MonoBehaviour
{
    public Slider EnergyBarSlider;
    public void GiveFullEnergy(float Energy)
    {
        EnergyBarSlider.maxValue = Energy;
        EnergyBarSlider.value = Energy;
    }
    public void SetEnergy(float Energy)
    {
        EnergyBarSlider.value = Energy;
    }
}
