using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with any object in which we want to give damage
public class Objects : MonoBehaviour
{
    public float objectHealth = 120f;
    public void objectHitDamage(float amount)
    {
        objectHealth -= amount;
        if(objectHealth <= 0)
        {
            DestroyObject();
        }
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
