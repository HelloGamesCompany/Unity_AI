using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    public GameObject thief;

    public void RespawnThief()
    {
        StartCoroutine(RespawnThiefCoroutine());
    }

    IEnumerator RespawnThiefCoroutine()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        thief.transform.position = this.transform.position;
        thief.SetActive(true);
    }

}
