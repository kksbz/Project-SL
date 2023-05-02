using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiInPutManager : Singleton<UiInPutManager>
{
    void Update()
    {
        UiInPutSystem();
    }

    private void UiInPutSystem()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != GData.SCENENAME_TITLE)
        {
            //GameManager.Instance.player.StateMachine.LockInput();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Inventory.Instance.invenObj.activeInHierarchy == true)
            {
                ExitUiPanel();
            }
            else if (UiManager.Instance.statusPanel.gameObject.activeInHierarchy == true)
            {
                UiManager.Instance.statusPanel.gameObject.SetActive(false);
            }
            else if (UiManager.Instance.warp.warpPanel.activeInHierarchy == true)
            {
                UiManager.Instance.warp.warpPanel.SetActive(false);
            }
            else if (UiManager.Instance.optionPanel.gameObject.activeInHierarchy == true)
            {
                if (UiManager.Instance.optionPanel.goBackText.activeInHierarchy == false)
                {
                    UiManager.Instance.optionPanel.gameObject.SetActive(false);
                }
            }
            else
            {
                UiManager.Instance.quickBar.SetActive(!UiManager.Instance.quickBar.activeSelf);
            }
        }
    } // UiInPutSystem

    //! ESC버튼 기능 함수
    private void ExitUiPanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Inventory.Instance.invenObj.SetActive(false);
        Inventory.Instance.equipSlotPanel.SetActive(true);
        Inventory.Instance.equipInvenPanel.SetActive(false);
        Inventory.Instance.selectPanel.gameObject.SetActive(false);
        Inventory.Instance.totalInvenPanel.SetActive(false);
    }
} // UiInPutManager
