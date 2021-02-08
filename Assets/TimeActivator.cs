using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeActivator : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [SerializeField] float _timeTillActivationSec;

    private void Start() {
        Invoke("Activate", _timeTillActivationSec);
    }

    private void Activate()
    {
        _gameObject.SetActive(true);
    }
}
