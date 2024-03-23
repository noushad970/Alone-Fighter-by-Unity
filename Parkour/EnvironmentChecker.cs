using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentChecker : MonoBehaviour
{
    public Vector3 rayoffset = new Vector3(0, 0.2f, 0);//position from where the ray will be create.from the fit
    public float raylength = 0.5f; //minimum distance between player and the object when raycast will work
    public LayerMask obstacleLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
