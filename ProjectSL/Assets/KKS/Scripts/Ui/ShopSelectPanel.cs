using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelectPanel : MonoBehaviour
{
    [SerializeField] private GameObject buyPanel; // 아이템 구매 패널
    [SerializeField] private GameObject sellPanel; // 아이템 판매 패널
    [SerializeField] private Button buyBt; // 아이템 구매 버튼
    [SerializeField] private Button sellBt; // 아이템 판매 버튼
    [SerializeField] private Button exitBt; // 나가기 버튼
    void Start()
    {
        // 아이템 구매 버튼
        buyBt.onClick.AddListener(() =>
        {
            buyPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // 아이템 판매 버튼
        sellBt.onClick.AddListener(() =>
        {
            sellPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // 나가기 버튼
        exitBt.onClick.AddListener(() =>
        {

        });
    } // Start
} // ShopSelectPanel
