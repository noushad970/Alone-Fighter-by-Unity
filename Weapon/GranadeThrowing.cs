using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this following line is added for android control
//using UnityStandardAssets.CrossPlatformInput;
//this script will attach with player
public class GranadeThrowing : MonoBehaviour
{
    public float throwForce = 10f;
    public Transform granadeArea;
    public GameObject granadePrefab;
    public Animator anim;
    float timer = 3f;
    public GameManager GM;
    public AudioSetup audioSetup;

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && timer == 3f && GM.numberOfGranade>0)
        {
            StartCoroutine(GranadeAnim());
            GM.numberOfGranade -= 1;
            StartCoroutine(timers());

        }
        /*
        for android control the code will be 
        if (CrossPlatformInputManager.GetButtonDown("Attack") && timer == 3f && GM.numberOfGranade>0)
        {
        StartCoroutine(GranadeAnim());
        GM.numberOfGranade -= 1;
        StartCoroutine(timers());
        }
        */
    }
    void throwGranade()
    {
        GameObject granade = Instantiate(granadePrefab, granadeArea.transform.position, granadeArea.transform.rotation);
        Rigidbody rb= granade.GetComponent<Rigidbody>();
        rb.AddForce(granadeArea.transform.forward*throwForce,ForceMode.VelocityChange);
    }
    IEnumerator GranadeAnim()
    {
       
        anim.SetBool("GranadeInAir", true);
        yield return new WaitForSeconds(0.5f);
        throwGranade();
        yield return new WaitForSeconds(1f); 
        anim.SetBool("GranadeInAir", false);
        
   
    }

    IEnumerator timers()
    {
        timer = 0f;
        yield return new WaitForSeconds(3f);
        timer = 3f;
        audioSetup.playGranadeSound();

    }

}
