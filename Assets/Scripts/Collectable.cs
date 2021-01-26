using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //private InventorySystem _playerInventory;
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

    private void AddToInventory (GameObject player) 
    {
        var inventory = player.GetComponentInChildren<InventorySystem>();
        inventory.AddToInventory(_item);
    }
}
