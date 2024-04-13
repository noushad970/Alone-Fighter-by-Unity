using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with anything that we want to destroy after 1 second
public class Des : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyItem());
    }
    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
