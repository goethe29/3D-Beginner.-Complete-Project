using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    private enum Type { Door, Chest }
    [SerializeField] private Type _type = Type.Door;

    [SerializeField] private List<GameObject> _items;

    [SerializeField] private AudioClip _lockedSound;
    [SerializeField] private AudioClip _openingSound;

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
        if (_type == Type.Door)
        {
            AudioSource.PlayClipAtPoint(_openingSound, _position);
            Invoke("DeactivateDoor", 1);
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
