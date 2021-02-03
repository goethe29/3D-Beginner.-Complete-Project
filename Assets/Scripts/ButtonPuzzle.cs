using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    
    [SerializeField] private float _pushDistance = 0.2f;
    [SerializeField] private Color _initialColor;
    [SerializeField] private Color _colorActivated = Color.green;
    [SerializeField] private Color _colorPushedWrong = Color.red;
    [SerializeField] private GameObject _key;
    [SerializeField] private PuzzleStatus _puzzleStatus;
    
    private List<GameObject> _pushingObjects;
    private bool _isActivated = false;
    private bool _isPushed = false;
    private Transform _transform;
    private MeshRenderer _renderer;
    private MeshRenderer _keyRenderer;
    private Color _keyInitialColor;

    private void Start() {
        _transform = gameObject.transform;
        _pushingObjects = new List<GameObject>();
        _renderer = GetComponent<MeshRenderer>();
        //_initialColor = _renderer.material.color;
        _renderer.material.color = _initialColor;
        if (_key) CacheKey();
    }

    private void CacheKey()
    {
        _keyRenderer = _key.GetComponent<MeshRenderer>();
        //_keyInitialColor = _keyRenderer.material.color;
        _keyInitialColor = _initialColor;
        _keyRenderer.material.color = _keyInitialColor;
    }
    
    private void OnTriggerEnter(Collider other) {
        _pushingObjects.Add(other.gameObject);
        MoveButton();
    }   

    private void OnTriggerExit(Collider other) 
    {
        _pushingObjects.Remove(other.gameObject);
        if(_pushingObjects.Count == 0) _isPushed = false;
        MoveButton();
    }

    private void MoveButton() 
    {
        if (_pushingObjects.Count == 1 && !_isPushed)
        {
            transform.position = transform.position - new Vector3(0, _pushDistance, 0);
            _isPushed = true;
            
        } else if (_pushingObjects.Count == 0)
        {
            transform.position = transform.position + new Vector3(0, _pushDistance, 0);
            _isPushed = false;

        }
        ToggleButton();
    }

    private void ToggleButton() 
    {
        if (_isActivated & _key & !_pushingObjects.Contains(_key))
        {
            _keyRenderer.material.color = _keyInitialColor;
        }


        if (_isPushed && (_pushingObjects.Contains(_key) || !_key))
        {
                _isActivated = _puzzleStatus.Solved = true;
                _renderer.material.color = _colorActivated;
                if(_key)
                    _keyRenderer.material.color = _colorActivated; 
        } else
        {
            _isActivated = _puzzleStatus.Solved = false;
            if (_isPushed)
            {
                _renderer.material.color = _colorPushedWrong;
            } else
            {
                _renderer.material.color = _initialColor;
            }
        }
    } 
}
