using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text topLineText;
    public bool isBuyPanel = true;

    private void OnEnable()
    {
        if (isBuyPanel == true)
        {
            topLineText.text = "���� - ����";
        }
        else
        {
            topLineText.text = "���� - �Ǹ�";
        }
    } // OnEnable
} // ShopPanel



