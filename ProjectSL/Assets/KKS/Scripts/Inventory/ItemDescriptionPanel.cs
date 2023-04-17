using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Image showIcon; // 보여질 아이템 아이콘
    [SerializeField] private TMP_Text showItemName; // 보여질 아이템 이름
    [SerializeField] private TMP_Text showDescription; // 보여질 아이템 설명

    void Start()
    {
        HideItemData();
    } // Start

    //! 비활성화 시 패널 초기화
    private void OnDisable()
    {
        HideItemData();
    } // OnDisable

    //! 아이템 설명 패널에 보여질 데이터 정하는 함수
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
