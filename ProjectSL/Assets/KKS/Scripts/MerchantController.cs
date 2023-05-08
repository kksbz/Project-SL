using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    private bool isEnterPlayer = false;

    void Update()
    {
        if (isEnterPlayer == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.player.StateMachine.LockInput();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                UiManager.Instance.shopPanel.SetActive(true);
                UiManager.Instance.interactionBar.SetActive(false);
                UiManager.Instance.interactionText.text = null;
            }
        }
    } // Update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionText.text = "상인과 대화하기 : E 키";
            UiManager.Instance.interactionBar.SetActive(true);
            isEnterPlayer = true;
        }
    } // OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.bonfirePanel.SetActive(false);
            UiManager.Instance.interactionBar.SetActive(false);
            UiManager.Instance.interactionText.text = null;
            isEnterPlayer = false;
        }
    } // OnTriggerExit
} // MerchantController
