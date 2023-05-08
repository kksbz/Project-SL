using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] List<Sprite> panelSprites; // 상단패널 스프라이트 리스트
    [SerializeField] private Image panelImage; // 상단패널 이미지
    [SerializeField] private TMP_Text panelText; // 상단패널 텍스트
    [SerializeField] private SaveAndLoadPanel saveAndLoadPanel; // 세이브,로드 패널
    [SerializeField] private GameObject manualPanel; // 플레이어 조작법 패널
    [SerializeField] private GameObject checkPanel; // 확인 패널
    [SerializeField] private TMP_Text checkText; // 확인 패널 텍스트
    [SerializeField] private Button checkPanelSelectBt; // 확인 패널 선택 버튼
    [SerializeField] private Button checkPanelCancleBt; // 확인 패널 취소 버튼
    [SerializeField] private Button goTitleBt; // 타이틀로 가기 버튼
    [SerializeField] private Button OptionBt; // 옵션 버튼
    [SerializeField] private Button SaveBt; // 저장하기 버튼
    [SerializeField] private Button LoadBt; // 로드하기 버튼
    [SerializeField] private Button ExitBt; // 게임종료 버튼
    public GameObject goBackText; // 하단패널 뒤로가기 텍스트
    private bool isExitGame = false;

    private void Start()
    {
        // 타이틀로 가기 버튼
        goTitleBt.onClick.AddListener(() =>
        {
            checkText.text = "타이틀화면으로 가시겠습니까?";
            isExitGame = false;
            checkPanel.SetActive(true);
        });
        // 게임종료 버튼
        ExitBt.onClick.AddListener(() =>
        {
            checkText.text = "게임을 종료하시겠습니까?";
            isExitGame = true;
            checkPanel.SetActive(true);
        });
        // 옵션 버튼
        OptionBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[1];
            panelText.text = "조작법";
            manualPanel.SetActive(true);
            goBackText.SetActive(true);
        });
        // 세이브 버튼
        SaveBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[2];
            panelText.text = "저장하기";
            saveAndLoadPanel.isSave = true;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });
        // 로드 버튼
        LoadBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[3];
            panelText.text = "불러오기";
            saveAndLoadPanel.isSave = false;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });

        // 확인 패널 선택 버튼
        checkPanelSelectBt.onClick.AddListener(() =>
        {
            if (isExitGame == true)
            {
                // 자동저장 후 게임종료
                DataManager.Instance.slotNum = 0;
                DataManager.Instance.SaveData();
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                // 타이틀씬으로 이동
                checkPanel.SetActive(false);
                GameManager.Instance.GoTitleScene();
            }
        });
        // 확인 패널 취소 버튼
        checkPanelCancleBt.onClick.AddListener(() =>
        {
            checkPanel.SetActive(false);
        });
    } // Start

    private void OnEnable()
    {
        panelImage.sprite = panelSprites[0];
        panelText.text = "설정";
        goBackText.SetActive(false);
        manualPanel.SetActive(false);
        saveAndLoadPanel.gameObject.SetActive(false);
    } // OnEnable

    private void Update()
    {
        if (goBackText.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                saveAndLoadPanel.gameObject.SetActive(false);
                manualPanel.SetActive(false);
                panelImage.sprite = panelSprites[0];
                panelText.text = "설정";
                goBackText.SetActive(false);
            }
        }
    } // Update
} // OptionPanel
