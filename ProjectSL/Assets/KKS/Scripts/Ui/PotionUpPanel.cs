using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionUpPanel : MonoBehaviour
{
    [SerializeField] private Button selectBt; // ���� ��ư
    [SerializeField] private Button ExitBt; // ��� ��ư
    [SerializeField] private TMP_Text nowQuantityText; // ����Ʈ�� ���� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text afterQuantityText; // ����Ʈ�� ���� �� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text possessionSouText; // �ʿ� �ҿ� �ؽ�Ʈ
    [SerializeField] private GameObject successPanel; // ��ȭ����â
    [SerializeField] private GameObject warningPanel; // ���â
    [SerializeField] private TMP_Text warningText; // ���â �ؽ�Ʈ
    void Start()
    {
        // ���� ��ư
        selectBt.onClick.AddListener(() =>
        {
            if (Inventory.Instance.Soul < 5000)
            {
                warningText.text = "��ȭ�� �ʿ��� �ҿ��� �����մϴ�";
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
                            warningText.text = "����Ʈ���� �ִ� ���� ������ 9�� �ִ��Դϴ�";
                            warningPanel.SetActive(true);
                        }
                        break;
                    }
                }
            }
        });
        // ��� ��ư
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
                    possessionSouText.text = "��ȭ �Ұ�";
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
