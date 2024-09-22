using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    [SerializeField] private Resource _resource;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private Vector3 _moveDirection;

    private Movement _movement;
    private float _distanceMapToStop = 0.2f;

    public event Action<Resource> CollectedResource;

    public bool IsResourceCollected { get; private set; }
    public bool IsStanding { get; private set; } = true;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (_resource != null)
        {
            _movement.Move(_moveDirection);
            IsStanding = false;
        }

        if (IsResourceCollected & Vector3.Distance(transform.position, _initialPosition) < _distanceMapToStop)
        {
            _resource.gameObject.SetActive(false);
            CollectedResource?.Invoke(_resource);
            _movement.Stop();
            IsResourceCollected = false;
            IsStanding = true;
            _resource = null;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Resource resource))
        {
            if (resource == _resource)
            {
                IsResourceCollected = true;
                ChangeMoveDirection();
                resource.Follow(transform);
            }
        }
    }

    public void StartMovingToResource(Resource resource)
    {
        _resource = resource;
        _moveDirection = _resource.transform.position - transform.position;
    }

    public void ChangeMoveDirection()
    {
        _moveDirection = _initialPosition - transform.position;
    }
}
