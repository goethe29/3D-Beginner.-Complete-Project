using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _maxViewDistance;
    private Transform _transform;
    private WaypointPatrol _patrolScript;
    private RaycastHit _hit;
    private float _currentViewDistance;

    // Start is called before the first frame update
    private void Start()
    {
        _patrolScript = GetComponent<WaypointPatrol>();
        _transform = GetComponent<Transform>();
        _currentViewDistance = _maxViewDistance;
    }

    private void FixedUpdate() 
    {
        HuntPlayer();
    }

    private void HuntPlayer() 
    {
        var hitDirection = (_player.transform.position - _transform.position).normalized;
        
        if (Physics.Raycast(_transform.position, hitDirection, out _hit, _maxViewDistance))
        {
            //logic of chase to implement.
        }

        AdjustViewDistance();
        Debug.DrawRay(_transform.position, hitDirection * _currentViewDistance, Color.red);
    }

    private void AdjustViewDistance() 
    {
        if (_hit.distance <= _maxViewDistance)
        {
            _currentViewDistance = _hit.distance;
        }
        else
        {
            _currentViewDistance = _maxViewDistance;
        }
    }
}
