using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    private enum Type { Door, Chest }
    [SerializeField] private Type _type = Type.Door;

    private enum Status { Locked, Unlocked }
    [SerializeField] private Status _status = Status.Locked;

    [SerializeField] private List<InventoryItem> _items;

    [SerializeField] private AudioClip _lockedSound;
    [SerializeField] private AudioClip _openingSound;

    [SerializeField] private InventoryItem _key;
    private GameObject _player;
    private InventorySystem _inventory;

    private Vector3 _position;

    void Start()
    {
        _position = GetComponent<Transform>().position;
    }

    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject;
            _inventory = _player.GetComponentInChildren<InventorySystem>();
            CheckLock();
        }
    }

    void CheckLock () 
    {
        if (_status == Status.Unlocked || _key == null)
        {
            Open();
        }

        else
        {
            if (_inventory.HasItem(_key))
            {
                Open();
            }

            else
            {
                AudioSource.PlayClipAtPoint(_lockedSound, _position);
            }
        }
    }

    void Open() 
    {
        AudioSource.PlayClipAtPoint(_openingSound, _position);

        if (_type == Type.Door)
        {
            Invoke("DeactivateDoor", 1);
        }

        if (_type == Type.Chest)
        {
            TakeItems();
        }
    }

    void DeactivateDoor() 
    {
        gameObject.SetActive(false);
    }
    
    void TakeItems() 
    {
        foreach (var item in _items) 
        {
            _inventory.AddToInventory(item);
        }
    }  
}
