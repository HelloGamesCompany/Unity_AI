using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    [SerializeField]
    private int beeNum = 1;

    private int currentBee = 1;

    [Range(0, 20)]
    public float maxSpeed = 1;

    [Range(0,20)]
    public float minSpeed = 0;

    [Range(0, 20)]
    public float maxRotationSpeed = 2.0f;

    [Range(0, 20)]
    public float minRotationSpeed = 2.0f;

    public float neighbourDistance = 1.0f;

    [HideInInspector]
    public List<GameObject> beeList = new List<GameObject>();

    private List<BeeFlock> beeFlocks = new List<BeeFlock>();

    [HideInInspector]
    public GameObject beeLeader = null;

    public float calDirFreq = 0.2f;

    public float beeRespawnFreq = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < beeNum; i++)
        {
            Vector3 pos = transform.position + new Vector3
            (
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f)
            );

            // Setup beeList
            beeList.Add(Instantiate(beePrefab));

            beeList[i].transform.position = pos;

            beeList[i].transform.SetParent(transform);

            // Setup beeFlock
            beeFlocks.Add(beeList[i].GetComponent<BeeFlock>());

            beeFlocks[i].manager = this;

            beeFlocks[i].movementSpeed = minSpeed;

            beeFlocks[i].rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        }
        currentBee = beeNum;

        Invoke("CalculBeesDirection", calDirFreq);

        InvokeRepeating("RespawnBee", 0, beeRespawnFreq);
    }

    private void RespawnBee()
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
    }

    private void CalculBeesDirection()
    {
        if (gameObject.activeSelf)
        {
            foreach (var bf in beeFlocks)
            {
                bf.CalculateDir();
            }
        }
        Invoke("CalculBeesDirection", calDirFreq);
    }

    private void OnEnable()
    {
        currentBee = beeNum;

        for (int i = 0; i < beeFlocks.Count; i++)
        {
            // Reset flocking parametre
            beeFlocks[i].movementSpeed = minSpeed;

            beeFlocks[i].rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

            // Reset general parametre
            Vector3 pos = transform.position + new Vector3
                (
                    Random.Range(0.0f, 1.0f),
                    Random.Range(0.0f, 1.0f),
                    Random.Range(0.0f, 1.0f)
                );

            beeList[i].gameObject.SetActive(true);

            beeList[i].transform.position = pos;       
        }
    }

    public void SetTarget(GameObject target)
    {
        beeLeader = target;

        for (int i = 0; i < beeFlocks.Count; i++)
        {
            // Reset flocking parametre
            beeFlocks[i].movementSpeed = maxSpeed;

            beeFlocks[i].rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        }
    }

    public void BeeDestroyed()
    {
        if (--currentBee <= 0)
        {
            if (beeLeader)
            {
                beeLeader.GetComponent<RunnerState>().LeavePanicRun();
                beeLeader = null;
            }
            gameObject.SetActive(false);
        }
    }
}