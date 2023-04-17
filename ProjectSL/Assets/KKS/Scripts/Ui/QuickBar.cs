using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBar : MonoBehaviour
{
    public Button equip; // 장비창
    public Button inven; // 통합인벤
    public Button stat; // 플레이어 스텟
    public Button option; // 옵션
    public GameObject inventory;

    void Start()
    {
        equip.onClick.AddListener(() =>
        {
            Debug.Log("장비 인벤 선택함");
            inventory.SetActive(true);
            gameObject.SetActive(false);
        });

        inven.onClick.AddListener(() =>
        {
            Debug.Log("통합 인벤 선택함");
            inventory.SetActive(true);
            Inventory.Instance.equipSlotPanel.SetActive(false);
            Inventory.Instance.InitSameTypeTotalSlot(ItemData.ItemType.CONSUMPTION);
            Inventory.Instance.totalInvenPanel.SetActive(true);
            gameObject.SetActive(false);
        });

        stat.onClick.AddListener(() =>
        {
            Debug.Log("스테이터스 선택함");
            gameObject.SetActive(false);
        });

        option.onClick.AddListener(() =>
        {
            Debug.Log("옵션창 선택함");
            gameObject.SetActive(false);
        });
    } // Start
} // QuickSlotBar
