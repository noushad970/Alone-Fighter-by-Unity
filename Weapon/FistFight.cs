using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistFight : MonoBehaviour
{
    public float timer = 0f;
    public int fistfightVal;
    public Animator anim;
  //  public bool isFistAnimated;

    private void Start()
    {
     //   isFistAnimated = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }
        else
        {
            anim.SetBool("FistFightActive", true);
            

            FistFightMode();
            timer = 0f;
        }
        if(timer>=7f)
        {
            anim.SetBool("FistFightActive", false);
        }
        
    }
    void FistFightMode()
    {
        if(Input.GetMouseButton(0))
        {
            fistfightVal = Random.Range(1, 5);
        }
        if(fistfightVal==1)
        {
            StartCoroutine(singleFist());
            
        }
        if (fistfightVal == 2)
        {
             StartCoroutine(DoubleFist());
        }
        if (fistfightVal == 3)
        {
            StartCoroutine(SingleKick());
        }
        if (fistfightVal == 4)
        {
            StartCoroutine(swingKick());
        }
        if (fistfightVal == 5)
        {
            StartCoroutine(punch2());
        }
    }
    IEnumerator singleFist()
    {
       // isFistAnimated = true;
        anim.SetBool("SingleFist", true);
        yield return new WaitForSeconds(1.1f);
        anim.SetBool("SingleFist", false);
       // isFistAnimated = false;
    }
    IEnumerator DoubleFist()
    {
       // isFistAnimated = true;
        anim.SetBool("DoubleFist", true);
        yield return new WaitForSeconds(1.22f);
        anim.SetBool("DoubleFist", false);
       // isFistAnimated = false;
    }
    IEnumerator SingleKick()
    {
        //isFistAnimated = true;
        anim.SetBool("SingleKick", true);
        yield return new WaitForSeconds(1.1f);
        anim.SetBool("SingleKick", false);
       // isFistAnimated = false;
    }
    IEnumerator punch2()
    {
       // isFistAnimated = true;
        anim.SetBool("Punch2", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Punch2", false);
       // isFistAnimated = false;
    }
    IEnumerator swingKick()
    {
       //     isFistAnimated = true;
        anim.SetBool("SwingKick", true);
        yield return new WaitForSeconds(1.18f);
        anim.SetBool("SwingKick", false);
       // isFistAnimated = false;
    }
}
