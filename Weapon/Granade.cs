using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will attach with granade
public class Granade : MonoBehaviour
{
    public float granadeTimer = 3f;
    float counDown;
    public float giveDamage = 120f;
    public GameObject explotionEffect;
    bool hasExploded=false;
    public float radius = 10f;
    void Start()
    {
        counDown = granadeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        counDown -= Time.deltaTime;
        if(counDown<=0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        //show effect
        Instantiate(explotionEffect,transform.position,transform.rotation);

        //get nearby Objects detected 
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObjecct in colliders)
        {
            Objects obj = nearbyObjecct.GetComponent<Objects>();
            if (obj != null)
            {
                obj.objectHitDamage(giveDamage);
            }
        }
        Destroy(gameObject);
    }
}
