using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemData;

public interface IPublicSlot
{
    GameObject SlotObj { get; }
    ItemType SlotType { get; set; }
    void AddItem(ItemData _item);
    void RemoveItem();
} // PublicSlot
