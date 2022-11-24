using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefCarBehaviour : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 direction;
    public float speed;
    public float distance;

    private bool _run = false;
    private float _runDistance = 0;
    private bool _returning = false;
    [SerializeField]
    private GameObject _thief;

    private void Update()
    {
        if (!_run)
            return;
        Vector3 movement = direction * speed * Time.deltaTime;
        _runDistance += movement.magnitude;
        transform.position += movement;

        if (_returning)
        {
            if (Vector3.Distance(transform.position, startPosition) <= 1.0f)
            {
                _thief.SetActive(true);
                Reset();
            }
        }
        else if (_runDistance >= distance)
            Return();

    }

    private void Return()
    {
        transform.position = startPosition + (-direction * distance);
        _returning = true;
    }

    public void Run()
    {
        _run = true;
    }

    private void Reset()
    {
        _run = false;
        _runDistance = 0;
        _returning = false;
    }
}
