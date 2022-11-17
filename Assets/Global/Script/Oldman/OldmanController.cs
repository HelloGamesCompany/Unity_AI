using System;
using UnityEngine;

public class OldmanController : MonoBehaviour
{
    //private Wander wander;
    private Animator anim;

    [HideInInspector]
    public Vector3 benchPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnSee(GameObject g)
    {
        if (g is null)
        {
            throw new ArgumentNullException(nameof(g));
        }

        benchPos = g.transform.position;

        if (anim && anim.GetCurrentAnimatorStateInfo(0).IsName("WanderState"))
        {
            anim.SetTrigger("GoToBench");
        }
    }
}
