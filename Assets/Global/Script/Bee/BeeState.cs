using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeState : MonoBehaviour
{
    private BeeManager beeManager = null;

    private void Start()
    {
        beeManager = GetComponentInParent<BeeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BeeKeeper"))
        {
            gameObject.SetActive(false);
            beeManager.BeeDestroyed();
        }
    }
}
