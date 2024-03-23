using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScripts : MonoBehaviour
{
    public EnvironmentChecker environmentChecker;
    private void Update()
    {
        var hitData = environmentChecker.CheckObstacle();
        if(hitData.hitFound)
        {
            Debug.Log("Object founded: " + hitData.hitInfo.transform.name);
        }
    }
}
