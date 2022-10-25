using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldmanController : MonoBehaviour
{
    //private Wander wander;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim) anim.SetBool("Wander", true);

        //wander = GetComponent<Wander>();

        //InvokeRepeating(nameof(FindTarget), 0, 0.2f);
    }
    //private void FindTarget()
    //{
    //    if (wander) wander.Go();
    //}

    public void OnSee(GameObject g)
    {
        Debug.Log("See" + g.name);
        if (anim) anim.SetBool("Wander", false);
    }
}
