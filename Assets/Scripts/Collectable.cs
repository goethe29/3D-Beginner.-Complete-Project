using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //private InventorySystem _playerInventory;
    [SerializeField] GameObject _popup;
    [SerializeField] InventoryItem _item;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        var collidedObject = other.gameObject;
        if (Input.GetKeyDown(KeyCode.E) && collidedObject.CompareTag("Player"))
        {
            AddToInventory(collidedObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            _popup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            _popup.SetActive(false);
        }
    }

    private void AddToInventory (GameObject player) 
    {
        var inventory = player.GetComponentInChildren<InventorySystem>();
        inventory.AddToInventory(_item);
    }
}
