using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSoul : MonoBehaviour
{
    private bool isEnterPlayer = false;
    public int souls;

    void Update()
    {
        if (isEnterPlayer == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ÀÒ¾î¹ö¸° ¼Ò¿ï È¹µæ½Ã UI¿¡ ¸Þ½ÃÁö Ç¥½Ã
                UiManager.Instance.messagePanel.GetSoulBackMessage();
                UiManager.Instance.soulBag.GetSoul(souls);
                UiManager.Instance.interactionBar.SetActive(false);
                UiManager.Instance.interactionText.text = null;
                Destroy(gameObject);
            }
        }
    } // Update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionText.text = "ÀÒ¾î¹ö¸° ¼Ò¿ï È¹µæ : E Å°";
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
} // DropSoul
