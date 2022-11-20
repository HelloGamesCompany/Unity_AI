using System;
using UnityEngine;

public class OldmanController : MonoBehaviour
{
    //private Wander wander;
    private Animator anim;

    [HideInInspector]
    public Vector3 benchPos = new Vector3(0, 0, 0);

    private float minimWanderTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        minimWanderTime -= Time.deltaTime;
    }

    public void OnSee(GameObject g)
    {
        if (minimWanderTime > 0)
        {
            return;
        }

        benchPos = g.transform.position;

        if (anim && anim.GetCurrentAnimatorStateInfo(0).IsName("WanderState"))
        {
            anim.SetTrigger("GoToBench");
        }
    }

    public void Help()
    {
        if (anim)
        {
            anim.SetTrigger("Help");
        }
    }

    public void Wander()
    {
        if (anim && 
            (anim.GetCurrentAnimatorStateInfo(0).IsName("SitState") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("CryForHelpState"))
            )
        {
            anim.SetTrigger("Wander");
        }
    }

    public void ResetWanderTime()
    {
        minimWanderTime = 5.0f;
    }
}
