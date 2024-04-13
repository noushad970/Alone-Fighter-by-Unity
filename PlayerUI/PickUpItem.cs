using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with pickup item which will be in the world and player will pick up such as Gun,health Up, coins

public class PickUpItem : MonoBehaviour
{
    [Header("Item Info")]
    public int itemRadius;
    public string itemTag;
    private GameObject itemToPick;
    public Inventory inventory;
    public GameManager manager;

    [Header("Player Info")]
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        itemToPick = GameObject.FindWithTag(itemTag);   
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)<itemRadius)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if (itemTag == "Sword")
                {
                    Debug.Log(itemTag + " is picked up");
                    inventory.isWeapon1Picked = true;
                }
                else if (itemTag == "Rifle")
                {
                    Debug.Log(itemTag + " is picked up");
                    inventory.isWeapon2Picked = true;

                }
                else if (itemTag == "Health")
                {
                    manager.numberOfhealth += 1;
                    Debug.Log(itemTag + " is picked up");
                }
                else if (itemTag == "Energy")
                {
                    manager.numberOfEnergy += 1; 
                    Debug.Log(itemTag + " is picked up");
                }
                else if (itemTag == "Granade")
                {
                    Debug.Log(itemTag + " is picked up");
                    inventory.isWeapon3Picked = true;
                }
                itemToPick.SetActive(false);
            }
        }
    }
}
