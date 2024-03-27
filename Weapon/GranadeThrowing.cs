using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeThrowing : MonoBehaviour
{
    public float throwForce = 10f;
    public Transform granadeArea;
    public GameObject granadePrefab;
    public Animator anim;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(GranadeAnim());
        }
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
    //    Destroy(granadePrefab);
    }

}
