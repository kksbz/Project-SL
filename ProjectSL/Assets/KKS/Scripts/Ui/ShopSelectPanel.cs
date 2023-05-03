using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelectPanel : MonoBehaviour
{
    [SerializeField] private GameObject buyPanel; // ������ ���� �г�
    [SerializeField] private GameObject sellPanel; // ������ �Ǹ� �г�
    [SerializeField] private Button buyBt; // ������ ���� ��ư
    [SerializeField] private Button sellBt; // ������ �Ǹ� ��ư
    [SerializeField] private Button exitBt; // ������ ��ư
    void Start()
    {
        // ������ ���� ��ư
        buyBt.onClick.AddListener(() =>
        {
            buyPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // ������ �Ǹ� ��ư
        sellBt.onClick.AddListener(() =>
        {
            sellPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // ������ ��ư
        exitBt.onClick.AddListener(() =>
        {

        });
    } // Start
} // ShopSelectPanel
