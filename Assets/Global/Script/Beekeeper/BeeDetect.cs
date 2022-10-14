using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeDetect : MonoBehaviour
{
    BeekeeperController beekeeper = null;

    private void Start()
    {
        beekeeper = GetComponentInParent<BeekeeperController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bee") && !beekeeper.targetBee)
        {
            beekeeper.targetBee = other.gameObject;

            beekeeper.agent.destination = gameObject.transform.position;
        }
    }
}
