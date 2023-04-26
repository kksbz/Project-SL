using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulBagUi : MonoBehaviour
{
    [SerializeField] GameObject getSoulTextPrefab; // �ټҿ� ������
    [SerializeField] private TMP_Text soulText; // ����UI �ҿ� ����â �ؽ�Ʈ

    private void Start()
    {
        soulText.text = Inventory.Instance.soul.ToString();
    } // Start

    //! �ҿ� ��� �Լ�
    public void GetSoul(int _soul)
    {
        Inventory.Instance.soul += _soul;
        soulText.text = Inventory.Instance.soul.ToString();
        InstanceGetSoulText(_soul);
    } // GetSoul

    //! �ҿ��ؽ�Ʈ ������Ʈ �����ϰ� ���� �ҿ� �����ִ� �Լ�
    private void InstanceGetSoulText(int _soul)
    {
        GameObject getSoulObj = Instantiate(getSoulTextPrefab);
        getSoulObj.transform.SetParent(transform);
        getSoulObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 50, 0);
        getSoulObj.GetComponent<TMP_Text>().text = "+" + _soul.ToString();
    } // InstanceGetSoulText
} // SoulBagUi
