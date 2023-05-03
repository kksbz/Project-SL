using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectPanel : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel; // ������ �г�
    [SerializeField] private GameObject potionUpPanel; // ����Ʈ�� ��ȭ �г�
    [SerializeField] private Button levelUpSelectBt; // ������ ���� ��ư
    [SerializeField] private Button hpPotionUpSelectBt; // ����Ʈ�� ��ȭ ��ư
    [SerializeField] private Button exitBt; // ������ ��ư

    void Start()
    {
        // ������ ���� ��ư
        levelUpSelectBt.onClick.AddListener(() =>
        {
            levelUpPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // ���� ��ȭ ���� ��ư
        hpPotionUpSelectBt.onClick.AddListener(() =>
        {
            potionUpPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // ������ ��ư
        exitBt.onClick.AddListener(() =>
        {
            UiManager.Instance.levelUpPanel.SetActive(false);
        });
    } // Start
} // LevelUpSelectPanel
