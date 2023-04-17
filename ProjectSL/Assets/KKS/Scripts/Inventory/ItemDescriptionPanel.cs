using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Image showIcon; // ������ ������ ������
    [SerializeField] private TMP_Text showItemName; // ������ ������ �̸�
    [SerializeField] private TMP_Text showDescription; // ������ ������ ����

    void Start()
    {
        HideItemData();
    } // Start

    //! ��Ȱ��ȭ �� �г� �ʱ�ȭ
    private void OnDisable()
    {
        HideItemData();
    } // OnDisable

    //! ������ ���� �гο� ������ ������ ���ϴ� �Լ�
    public void ShowItemData(ItemData item)
    {
        showIcon.sprite = Resources.Load<Sprite>(item.itemIcon);
        showIcon.color = new Color(1, 1, 1, 1);
        showItemName.text = item.itemName;
        showDescription.text = item.description;
    } // ShowItemData

    public void HideItemData()
    {
        showIcon.color = new Color(1, 1, 1, 0);
        showItemName.text = null;
        showDescription.text = null;
    } // HideItemData
} // ItemDescriptionPanel
