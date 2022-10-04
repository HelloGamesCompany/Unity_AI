using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLocator : MonoBehaviour
{
    [SerializeField]
    private Transform locator;

    // Update is called once per frame
    void Update()
    {
        transform.position = locator.position;
    }
}
