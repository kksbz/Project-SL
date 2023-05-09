using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiInPutManager : Singleton<UiInPutManager>
{
    private bool isExitUi = false;
    public bool isInvincibility = false;

    public void UiInPutSystem()
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
    } // UiInPutSystem

    // 치트 : 무적 OnOff
    public void Cheat_Invincibility()
    {
        isInvincibility = !isInvincibility;
        Debug.Log($"무적치트 On : {isInvincibility}");
    } // Cheat_Invincibility

    // 치트 : 소울 얻기
    public void Cheat_GetSoul()
    {
        UiManager.Instance.soulBag.GetSoul(10000);
    } // Cheat_GetSoul

    //! ESC��ư ��� �Լ�
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
