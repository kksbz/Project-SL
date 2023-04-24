using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool isEnterPlayer = false;

    private void Update()
    {
        if (isEnterPlayer == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pickup();
                UiManager.Instance.interactionBar.SetActive(false);
                UiManager.Instance.interactionText.text = null;
            }
        }
    } // Update

    private void Pickup()
    {
        Inventory.Instance.AddItem(item.itemData);
        Destroy(item.gameObject);
    } // Pickup

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionText.text = "������ ȹ�� : E Ű";
            UiManager.Instance.interactionBar.SetActive(true);
            isEnterPlayer = true;
        }
    } // OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionBar.SetActive(false);
            UiManager.Instance.interactionText.text = null;
            isEnterPlayer = false;
        }
    } // OnTriggerExit
} // ItemPickup
