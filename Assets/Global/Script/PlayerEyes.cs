using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MMCol")
        {
            //player.GetComponent<Controller>().DetectExTarget(other.gameObject);
        }
    }
}
