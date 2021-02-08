using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] public GameObject _player;
    [SerializeField] private float _maxViewDistance = 10.0f;
    [SerializeField] private float _calmDownDelaySec = 1.0f;
    
    private string _playerTag;
    private Transform _transform;
    private WaypointPatrol _patrolScript;
    private Vector3 _lastPatrolPoint;
    private NavMeshAgent _navMesh;
    private RaycastHit _hit;
    
    private float _currentViewDistance;
    private bool _inChase;
    private bool _isCalmingDown;
    private float _timerSec = 0.0f;
    private float _refreshStatusIntervalSec = 1.0f;
    

    // Start is called before the first frame update
    private void Start()
    {
        _patrolScript = GetComponent<WaypointPatrol>();
        _navMesh = GetComponent<NavMeshAgent>();
        _transform = GetComponent<Transform>();

        _playerTag = _player.tag;
        _currentViewDistance = _maxViewDistance;
    }

    private void FixedUpdate() 
    {
        LookAtPlayer();
        _timerSec += Time.deltaTime;
        if (_timerSec >= _refreshStatusIntervalSec)
        {
            _timerSec -= _refreshStatusIntervalSec;
            RefreshStatus();
        }
    }

    private void LookAtPlayer() 
    {
        var hitDirection = (_player.transform.position - _transform.position).normalized;
        Physics.Raycast(_transform.position, hitDirection, out _hit, _maxViewDistance);
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

    private void RefreshStatus()
    {
        if (_hit.transform != null)
        {
            if (_hit.transform.CompareTag(_playerTag) && !_inChase)
        {
            ActivateChase();
        }
            else if (_hit.transform.CompareTag(_playerTag) && _inChase)
            {
                if (_isCalmingDown)
                {
                    _isCalmingDown = false;
                    StopCoroutine(CalmDown());
                }
                _navMesh.SetDestination(_player.transform.position);
            }
        }
        
        else if (_inChase)
        {
            StartCoroutine(CalmDown());
        }
    }

    private void ActivateChase()
    {
        _inChase = true;

        _lastPatrolPoint = _navMesh.destination;
        _patrolScript.enabled = false;
        _navMesh.SetDestination(_player.transform.position);
    }

    private IEnumerator CalmDown()
    {
        _isCalmingDown = true;
        yield return new WaitForSeconds(_calmDownDelaySec);
        
        _inChase = false;
        _patrolScript.enabled = true;
        _navMesh.SetDestination(_lastPatrolPoint);
    }
}
