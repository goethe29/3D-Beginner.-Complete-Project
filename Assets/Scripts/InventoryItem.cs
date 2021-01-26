using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item", order = 53)]
public class InventoryItem : ScriptableObject
{
    [Tooltip("Unique name if applicable")][SerializeField] string _uniqueName;

    [SerializeField] GameObject _model;

    private void Start() {
    }
    public string UniqueName 
    {
        get 
        {
            return _uniqueName; 
        }
    }
}