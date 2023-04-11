using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            List<ItemData> items = ItemManager.Instance.items;
            foreach (ItemData item in items)
            {
                Debug.Log($"{item.itemID}, {item.itemName}, {item.itemType}, {item.description}");
            }
        }
    }
}
