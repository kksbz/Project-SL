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
            topLineText.text = "상점 - 구매";
        }
        else
        {
            topLineText.text = "상점 - 판매";
        }
    } // OnEnable
} // ShopPanel



