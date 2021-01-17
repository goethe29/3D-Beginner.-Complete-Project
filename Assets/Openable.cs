using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    private enum Type { Door, Chest }
    [SerializeField] private Type _type = Type.Door;

    private enum Status { Locked, Opened }
    [SerializeField] private Status _status = Status.Locked;

    [SerializeField] private List<GameObject> _items;

    [SerializeField] private AudioClip _lockedSound;
    [SerializeField] private AudioClip _openingSound;

    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _player;

    private Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        _position = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactivateDoor() 
    {
        gameObject.SetActive(false);
    }
    
    void Open() 
    {
        if (_status == Status.Opened || _key.transform.parent == _player.transform)
        {
            if (_type == Type.Door)
            {
                AudioSource.PlayClipAtPoint(_openingSound, _position);
                Invoke("DeactivateDoor", 1);
            }
        }
        else 
        {
            AudioSource.PlayClipAtPoint(_lockedSound, _position);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            Open();
        }
    }
}
