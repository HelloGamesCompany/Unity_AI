using UnityEngine;
using UnityEngine.AI;

public class TaichiFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector3 _relativePos;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = _speed;

        transform.rotation = _target.transform.rotation;
        transform.position = _target.transform.TransformPoint(_relativePos);
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = _target.transform.TransformPoint(_relativePos);
    }
}
