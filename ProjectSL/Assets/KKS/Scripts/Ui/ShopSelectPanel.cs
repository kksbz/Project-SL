using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelectPanel : MonoBehaviour
{
    [SerializeField] private ShopPanel shopPanel; // 상점 패널
    [SerializeField] private Button buyBt; // 아이템 구매 버튼
    [SerializeField] private Button sellBt; // 아이템 판매 버튼
    [SerializeField] private Button exitBt; // 나가기 버튼
    void Start()
    {
        // 상점 판매용 아이템 리스트
        foreach (var itemData in DataManager.Instance.itemDatas)
        {
            if (int.Parse(itemData[0]) != 1 && int.Parse(itemData[0]) != 2)
            {
                ItemData item = new ItemData(itemData);
                shopPanel.shopItems.Add(item);
            }
        }
        shopPanel.GetTypeSprites();

        // 아이템 구매 버튼
        buyBt.onClick.AddListener(() =>
        {
            shopPanel.isBuyPanel = true;
            shopPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        // 아이템 판매 버튼
        sellBt.onClick.AddListener(() =>
        {
            shopPanel.isBuyPanel = false;
            shopPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        // 나가기 버튼
        exitBt.onClick.AddListener(() =>
        {
            UiManager.Instance.shopPanel.SetActive(false);
            GameManager.Instance.player.StateMachine.ResetInput();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        });
    } // Start

    private void OnEnable()
    {
        shopPanel.gameObject.SetActive(false);
    } // OnEnable
} // ShopSelectPanel
