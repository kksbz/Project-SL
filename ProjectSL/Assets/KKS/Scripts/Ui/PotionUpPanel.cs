using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionUpPanel : MonoBehaviour
{
    [SerializeField] private Button selectBt; // 선택 버튼
    [SerializeField] private Button ExitBt; // 취소 버튼
    [SerializeField] private TMP_Text nowQuantityText; // 에스트병 현재 수량 텍스트
    [SerializeField] private TMP_Text afterQuantityText; // 에스트병 증가 후 수량 텍스트
    [SerializeField] private TMP_Text possessionSouText; // 필요 소울 텍스트
    [SerializeField] private GameObject successPanel; // 강화성공창
    [SerializeField] private GameObject warningPanel; // 경고창
    [SerializeField] private TMP_Text warningText; // 경고창 텍스트
    void Start()
    {
        // 선택 버튼
        selectBt.onClick.AddListener(() =>
        {
            if (Inventory.Instance.Soul < 5000)
            {
                warningText.text = "강화에 필요한 소울이 부족합니다";
                warningPanel.SetActive(true);
            }
            else
            {
                foreach (ItemData item in Inventory.Instance.inventory)
                {
                    if (item.itemID == 1)
                    {
                        if (item.maxQuantity < 9)
                        {
                            item.maxQuantity += 1;
                            item.Quantity = item.maxQuantity;
                            UiManager.Instance.soulBag.GetSoul(-5000);
                            successPanel.SetActive(true);
                        }
                        else
                        {
                            warningText.text = "에스트병의 최대 보유 수량은 9가 최대입니다";
                            warningPanel.SetActive(true);
                        }
                        break;
                    }
                }
            }
        });
        // 취소 버튼
        ExitBt.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UiManager.Instance.levelUpPanel.SetActive(false);
        });
    } // Start

    private void OnEnable()
    {
        possessionSouText.text = "5000";
        foreach (ItemData item in Inventory.Instance.inventory)
        {
            if (item.itemID == 1)
            {
                if (item.maxQuantity < 9)
                {
                    nowQuantityText.text = item.maxQuantity.ToString();
                    afterQuantityText.text = (item.maxQuantity + 1).ToString();
                }
                else
                {
                    nowQuantityText.text = "Max";
                    afterQuantityText.text = "MAX";
                    possessionSouText.text = "강화 불가";
                }
                break;
            }
        }
    } // OnEnable

    private void Update()
    {
        if (warningPanel.activeInHierarchy == true)
        {
            if (Input.anyKeyDown)
            {
                warningPanel.SetActive(false);
            }
        }
        if (successPanel.activeInHierarchy == true)
        {
            if (Input.anyKeyDown)
            {
                successPanel.SetActive(false);
                gameObject.SetActive(false);
                UiManager.Instance.levelUpPanel.SetActive(false);
            }
        }
    } // Update
} // PotionUpPanel
