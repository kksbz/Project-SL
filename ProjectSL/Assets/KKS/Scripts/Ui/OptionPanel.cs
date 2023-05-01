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
    [SerializeField] private Button OptionBt;
    [SerializeField] private Button SaveBt;
    [SerializeField] private Button LoadBt;
    [SerializeField] private Button ExitBt;
    public GameObject goBackText; // 하단패널 뒤로가기 텍스트

    private void Start()
    {
        // 옵션 버튼
        OptionBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[1];
            panelText.text = "설정";
            goBackText.SetActive(true);
        });
        // 세이브 버튼
        SaveBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[2];
            panelText.text = "세이브";
            saveAndLoadPanel.isSave = true;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });
        // 로드 버튼
        LoadBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[3];
            panelText.text = "로드";
            saveAndLoadPanel.isSave = false;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });
        // 게임종료 버튼
        ExitBt.onClick.AddListener(() =>
        {
            // 자동저장 후 게임종료
            DataManager.Instance.slotNum = 0;
            DataManager.Instance.SaveData();
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        });
    } // Start

    private void OnEnable()
    {
        panelImage.sprite = panelSprites[0];
        panelText.text = "옵션";
        goBackText.SetActive(false);
    } // OnEnable

    private void Update()
    {
        if (goBackText.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                saveAndLoadPanel.gameObject.SetActive(false);
                panelImage.sprite = panelSprites[0];
                panelText.text = "옵션";
                goBackText.SetActive(false);
            }
        }
    } // Update
} // OptionPanel
