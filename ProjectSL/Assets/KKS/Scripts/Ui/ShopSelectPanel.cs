using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelectPanel : MonoBehaviour
{
    [SerializeField] private ShopPanel shopPanel; // ���� �г�
    [SerializeField] private Button buyBt; // ������ ���� ��ư
    [SerializeField] private Button sellBt; // ������ �Ǹ� ��ư
    [SerializeField] private Button exitBt; // ������ ��ư
    void Start()
    {
        // ���� �Ǹſ� ������ ����Ʈ
        foreach (var itemData in DataManager.Instance.itemDatas)
        {
            if (int.Parse(itemData[0]) != 1 && int.Parse(itemData[0]) != 2)
            {
                ItemData item = new ItemData(itemData);
                shopPanel.shopItems.Add(item);
            }
        }
        shopPanel.GetTypeSprites();

        // ������ ���� ��ư
        buyBt.onClick.AddListener(() =>
        {
            shopPanel.isBuyPanel = true;
            shopPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        // ������ �Ǹ� ��ư
        sellBt.onClick.AddListener(() =>
        {
            shopPanel.isBuyPanel = false;
            shopPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        // ������ ��ư
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
