using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulBagUi : MonoBehaviour
{
    [SerializeField] GameObject getSoulTextPrefab; // 겟소울 프리팹
    [SerializeField] private TMP_Text soulText; // 메인UI 소울 보유창 텍스트

    private void Start()
    {
        soulText.text = Inventory.Instance.soul.ToString();
    } // Start

    //! 소울 얻는 함수
    public void GetSoul(int _soul)
    {
        Inventory.Instance.soul += _soul;
        soulText.text = Inventory.Instance.soul.ToString();
        InstanceGetSoulText(_soul);
    } // GetSoul

    //! 소울텍스트 오브젝트 생성하고 얻은 소울 보여주는 함수
    private void InstanceGetSoulText(int _soul)
    {
        GameObject getSoulObj = Instantiate(getSoulTextPrefab);
        getSoulObj.transform.SetParent(transform);
        getSoulObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 50, 0);
        getSoulObj.GetComponent<TMP_Text>().text = "+" + _soul.ToString();
    } // InstanceGetSoulText
} // SoulBagUi
