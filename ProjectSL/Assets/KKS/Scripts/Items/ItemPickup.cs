using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void Pickup()
    {
        Inventory.Instance.AddItem(item.itemData);
        Destroy(item.gameObject);
    } // Pickup

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.InteractionBar.SetActive(true);
            UiManager.Instance.InteractionText.text = "æ∆¿Ã≈€ »πµÊ : E ≈∞";
        }
    } // OnTriggerEnter

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pickup();
                UiManager.Instance.InteractionBar.SetActive(false);
                UiManager.Instance.InteractionText.text = null;
            }
        }
    } // OnTriggerStay

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.InteractionBar.SetActive(false);
            UiManager.Instance.InteractionText.text = null;
        }
    } // OnTriggerExit
} // ItemPickup
