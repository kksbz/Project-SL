using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectPanel : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel; // 레벨업 패널
    [SerializeField] private GameObject potionUpPanel; // 에스트병 강화 패널
    [SerializeField] private Button levelUpSelectBt; // 레벨업 선택 버튼
    [SerializeField] private Button hpPotionUpSelectBt; // 에스트병 강화 버튼
    [SerializeField] private Button exitBt; // 나가기 버튼

    void Start()
    {
        // 레벨업 선택 버튼
        levelUpSelectBt.onClick.AddListener(() =>
        {
            levelUpPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // 포션 강화 선택 버튼
        hpPotionUpSelectBt.onClick.AddListener(() =>
        {
            potionUpPanel.SetActive(true);
            gameObject.SetActive(false);
        });
        // 나가기 버튼
        exitBt.onClick.AddListener(() =>
        {
            UiManager.Instance.levelUpPanel.SetActive(false);
        });
    } // Start
} // LevelUpSelectPanel
