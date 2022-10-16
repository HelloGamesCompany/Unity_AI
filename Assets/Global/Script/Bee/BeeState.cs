using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeState : MonoBehaviour
{
    [SerializeField]
    private GameObject dieParticle = null;

    private BeeManager beeManager = null;

    private void Start()
    {
        beeManager = GetComponentInParent<BeeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BeeKeeper"))
        {
            beeManager.BeeDestroyed();

            GameObject p = Instantiate(dieParticle);
            p.transform.position = transform.position;
            Destroy(p, 3.0f);

            gameObject.SetActive(false);            
        }
    }
}
