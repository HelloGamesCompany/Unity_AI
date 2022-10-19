using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldmanController : MonoBehaviour
{
    private Wander wander;

    // Start is called before the first frame update
    void Start()
    {
        wander = GetComponent<Wander>();

        InvokeRepeating(nameof(FindTarget), 0, 0.2f);
    }
    private void FindTarget()
    {
        if (wander) wander.Go();
    }

    public void OnSee(GameObject g)
    {
        Debug.Log("See" + g.name);
    }
}
