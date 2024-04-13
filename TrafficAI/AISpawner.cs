using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script must need to attach to the waypoints parent object
public class AISpawner : MonoBehaviour
{
    
    public GameObject[] AIPrefab;
    public int AItoSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        int count = 0;
        while (count < AItoSpawn)
        {
            int randomIndex=Random.Range(0, AIPrefab.Length);
            GameObject obj= Instantiate(AIPrefab[randomIndex]);
            Transform child= transform.GetChild(Random.Range(0,transform.childCount-1));
            obj.GetComponent<WayPointNavigator>().currentWaypoint = child.GetComponent<WayPoint>();

            obj.transform.position = child.position;
            yield return new WaitForSeconds(1f);
            count++;
        }

    }

    
}
