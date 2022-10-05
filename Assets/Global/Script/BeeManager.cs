using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bee;

    [SerializeField]
    private int beeNum = 1;

    [Range(0, 20)]
    public float maxSpeed = 1;

    [Range(0,20)]
    public float minSpeed = 0;

    [Range(0, 20)]
    public float rotationSpeed = 2.0f;

    public float neighbourDistance = 1.0f;

    public List<GameObject> beeList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < beeNum; i++)
        {
            Vector3 pos = 
                transform.position + 
                new Vector3(Random.Range(0.0f, 5.0f), 
                Random.Range(0.0f, 5.0f), 
                Random.Range(0.0f, 5.0f));

            GameObject b = Instantiate(bee);

            b.transform.position = pos;

            b.transform.SetParent(transform);

            b.GetComponent<BeeFlock>().manager = this;

            beeList.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
