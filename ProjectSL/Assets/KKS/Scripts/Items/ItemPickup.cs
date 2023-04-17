using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void Pickup()
    {
        Inventory.Instance.AddItem(item.itemData);
        Destroy(gameObject);
    } // Pickup

    private void OnMouseDown()
    {
        Pickup();
    }
} // ItemPickup
