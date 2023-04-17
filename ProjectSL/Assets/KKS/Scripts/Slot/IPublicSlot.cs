using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemData;

public interface IPublicSlot
{
    ItemType SlotType { get; set; }
    void AddItem(ItemData _item);
    void RemoveItem();
} // PublicSlot
