using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<string,int> _inventory;

    private void Start() {
        _inventory = new Dictionary<string, int>();
    }
    
    public void AddToInventory(InventoryItem item)
    {
        var key = item.UniqueName;
        if(_inventory.ContainsKey(key)) 
        {
            _inventory[key] += 1;
        }
        else 
        {
            _inventory.Add(key, 1);
        }
    }

    public bool HasItem(InventoryItem item)
    {
        var key = item.UniqueName;
        return _inventory.ContainsKey(key); 
    }
}
