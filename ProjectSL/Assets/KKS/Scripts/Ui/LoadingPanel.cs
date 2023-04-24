using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;

    private void OnEnable()
    {
        int num = Random.Range(0, DataManager.Instance.itemDatas.Count);
        ItemData itemData = new ItemData(DataManager.Instance.itemDatas[num]);
        itemIcon.sprite = Resources.Load<Sprite>(itemData.itemIcon);
        itemName.text = itemData.itemName;
        itemDescription.text = itemData.description;
    } // OnEnable
} // LoadingPanel
