using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookToCamera : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent parentAgent = null;

    private SpriteRenderer sprite = null;

    private Camera mainCamera = null;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        mainCamera = Camera.main;
    }

    void Update()
    {
        //transform.rotation.SetLookRotation(Camera.main.transform.position);
        transform.LookAt(mainCamera.transform.position);

        SpriteFlip();
    }

    void SpriteFlip()
    {
        if (sprite && parentAgent)
        {
            Vector3 myAgentDes = mainCamera.WorldToScreenPoint(parentAgent.destination);

            Vector3 myScreenPos = mainCamera.WorldToScreenPoint(transform.position);

            sprite.flipX = (myAgentDes.x - myScreenPos.x) > 0 ? true : false;
        }
    }
}