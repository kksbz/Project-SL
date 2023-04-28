using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiInPutManager : Singleton<UiInPutManager>
{
    private bool isOptionBar = false;
    private bool isEquipPanel = false;

    void Update()
    {
        UiInPutSystem();
    }

    private void UiInPutSystem()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Inventory.Instance.invenObj.activeInHierarchy == true)
            {
                ExitUiPanel();
            }
            else
            {
                UiManager.Instance.optionBar.SetActive(!UiManager.Instance.optionBar.activeSelf);
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
