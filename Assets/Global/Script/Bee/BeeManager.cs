using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bee;

    [SerializeField]
    private int beeNum = 1;

    private int currentBee = 1;

    [Range(0, 20)]
    public float maxSpeed = 1;

    [Range(0,20)]
    public float minSpeed = 0;

    [Range(0, 20)]
    public float rotationSpeed = 2.0f;

    public float radiusFromLeader = 10.0f;

    public float neighbourDistance = 1.0f;

    public float leaderSpeed = 8.0f;

    public List<GameObject> beeList = new List<GameObject>();

    public Vector3 targetDirection;

    public GameObject beeLeader = null;

    public GameObject leaderTarget = null;

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
            if (i <= (int)(beeNum/3)) b.GetComponent<BeeFlock>().followLeader = true;

            beeList.Add(b);
        }
        beeLeader = beeList[0];
        beeLeader.GetComponentInChildren<SpriteRenderer>().enabled = false;

        currentBee = beeNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (leaderTarget == null) return;

        targetDirection = leaderTarget.transform.position - beeLeader.transform.position;
        targetDirection.y += 0.5f;

        //Debug.Log(targetDirection);

        beeLeader.transform.position += targetDirection.normalized * leaderSpeed * Time.deltaTime;
        //beeLeader.transform.Translate(targetDirection.normalized * maxSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        foreach (var b in beeList)
        {
            b.gameObject.SetActive(true);
            Vector3 pos =
               transform.position +
               new Vector3(Random.Range(0.0f, 5.0f),
               Random.Range(0.0f, 5.0f),
               Random.Range(0.0f, 5.0f));
            b.transform.position = pos;
        }
    }

    public void GiveTarget(GameObject target)
    {
        leaderTarget = target;
        if (leaderTarget == null) targetDirection = Vector3.zero;
    }

    public void BeeDestroyed()
    {
        if (--currentBee <= 0)
        {
            leaderTarget.GetComponent<RunnerState>().LeavePanicRun();
            currentBee = beeNum;
            gameObject.SetActive(false);
        }
    }
}
