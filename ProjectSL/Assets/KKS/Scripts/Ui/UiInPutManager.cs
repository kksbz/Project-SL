using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInPutManager : Singleton<UiInPutManager>
{
    private bool isExitUi = false;
    void Update()
    {
        UiInPutSystem();
    }

    private void UiInPutSystem()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CheckActiveTitleScene() == true)
        {
            GameManager.Instance.player.StateMachine.LockInput();
            isExitUi = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Inventory.Instance.invenObj.activeInHierarchy == true)
            {
                ExitUiPanel();
                isExitUi = true;
            }
            else if (UiManager.Instance.statusPanel.gameObject.activeInHierarchy == true)
            {
                UiManager.Instance.statusPanel.gameObject.SetActive(false);
                isExitUi = true;
            }
            else if (UiManager.Instance.warp.warpPanel.activeInHierarchy == true)
            {
                UiManager.Instance.warp.warpPanel.SetActive(false);
                isExitUi = true;
            }
            else if (UiManager.Instance.optionPanel.gameObject.activeInHierarchy == true)
            {
                if (UiManager.Instance.optionPanel.goBackText.activeInHierarchy == false)
                {
                    UiManager.Instance.optionPanel.gameObject.SetActive(false);
                    isExitUi = true;
                }
            }
            else if (UiManager.Instance.levelUpPanel.activeInHierarchy == true)
            {
                UiManager.Instance.levelUpPanel.SetActive(false);
                isExitUi = true;
            }
            else if (UiManager.Instance.shopPanel.activeInHierarchy == true)
            {
                UiManager.Instance.shopPanel.SetActive(false);
                isExitUi = true;
            }
            else
            {
                UiManager.Instance.quickBar.SetActive(!UiManager.Instance.quickBar.activeSelf);
                if (UiManager.Instance.quickBar.activeInHierarchy == false)
                {
                    isExitUi = true;
                }
            }

            if (isExitUi == true)
            {
                GameManager.Instance.player.StateMachine.ResetInput();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
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
