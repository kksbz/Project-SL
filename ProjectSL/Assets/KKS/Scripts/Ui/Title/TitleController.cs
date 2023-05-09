using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Button continueBt; // 이어하기 버튼
    [SerializeField] private Button newGameBt; // 새게임 버튼
    [SerializeField] private Button loadBt; // 로드 버튼
    [SerializeField] private Button optionBt; // 옵션 버튼
    [SerializeField] private Button exitBt; // 종료 버튼
    [Header("타이틀 패널 모음")]
    [SerializeField] private GameObject newGamePanel; // 뉴게임패널
    [SerializeField] private GameObject loadPanel; // 로드패널

    void Start()
    {
        //! 자동저장 데이터가 있으면 컨티뉴버튼 활성화
        if (DataManager.Instance.hasSavefile[0] == true)
        {
            continueBt.gameObject.SetActive(true);
        }
        // 계속하기 버튼
        continueBt.onClick.AddListener(() =>
        {
            // 자동저장슬롯의 데이터로 바로 시작
            GameManager.Instance.LoadSaveDataScene(0);
        });
        // 새게임 버튼 플레이어 이름 입력창 활성화
        newGameBt.onClick.AddListener(() =>
        {
            newGamePanel.SetActive(true);
        });
        // 로드 버튼 세이브 슬롯 패널 활성화
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
            //UnityEditor.EditorApplication.isPlaying = false;
        });
    } // Start
} // TitleController
