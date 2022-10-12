using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLocator : MonoBehaviour
{
    [SerializeField]
    private Transform locator;

    private float defaultScale;

    private float locatorDefaultHeight;

    private void Start()
    {
        defaultScale = transform.localScale.x;

        locatorDefaultHeight = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (locator.position.y <= 1.0f)
        {
            transform.localScale = new Vector3(0, 0, 0);
            return;
        }

        transform.position = new Vector3(locator.position.x, 1.1f, locator.position.z);

        float temp = locator.position.y / locatorDefaultHeight;

        float newScale = defaultScale / temp;

        transform.localScale = new Vector3(newScale, newScale, newScale); 
    }
}
