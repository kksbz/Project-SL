using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Button newGameBt; // 새게임 버튼
    [SerializeField] private Button loadBt; // 로드 버튼
    [SerializeField] private Button optionBt; // 옵션 버튼
    [SerializeField] private Button exitBt; // 종료 버튼
    [Header("타이틀 패널 모음")]
    [SerializeField] private GameObject loadPanel; // 로드패널
    // Start is called before the first frame update
    void Start()
    {
        // 새게임 버튼
        newGameBt.onClick.AddListener(() =>
        {

        });
        // 로드 버튼
        loadBt.onClick.AddListener(() =>
        {
            loadPanel.SetActive(true);
        });
        // 옵션 버튼
        optionBt.onClick.AddListener(() =>
        {

        });
        // 종료 버튼
        exitBt.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    } // Start
} // TitleController
