using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoadPanel : MonoBehaviour
{
    public bool isSave; // 세이브, 로드 선택 변수
    [SerializeField] List<TMP_Text> saveSlotTexts;
    [SerializeField] List<TMP_Text> saveSlotTimeTexts;
    [SerializeField] private Button saveAutoBt; // 자동저장 슬롯
    [SerializeField] private Button saveSlot1; // 저장 슬롯 1
    [SerializeField] private Button saveSlot2; // 저장 슬롯 2
    [SerializeField] private Button saveSlot3; // 저장 슬롯 3
    [SerializeField] private GameObject warning; // 경고창
    [SerializeField] private GameObject selectSlot; // 슬롯선택창
    [SerializeField] private TMP_Text selectSlotText; // 슬롯선택창 텍스트
    [SerializeField] private Button selectSlotYesBt; // 슬롯선택창 선택버튼
    [SerializeField] private Button selectSlotNoBt; // 슬롯선택창 취소버튼
    private string saveText = "선택한 슬롯에 저장하시겠습니까?";
    private string hasSaveText = "저장된 데이터가 있습니다. 덮어씌우시겠습니까?";
    private string loadText = "선택한 슬롯으로 시작하시겠습니까?";
    private int selectSlotNum;
    void Start()
    {
        // 자동저장 슬롯 선택 버튼
        saveAutoBt.onClick.AddListener(() =>
        {
            SelectSlot(0);
        });

        // 저장슬롯1 선택 버튼
        saveSlot1.onClick.AddListener(() =>
        {
            SelectSlot(1);
        });

        // 저장슬롯2 선택 버튼
        saveSlot2.onClick.AddListener(() =>
        {
            SelectSlot(2);
        });

        // 저장슬롯3 선택 버튼
        saveSlot3.onClick.AddListener(() =>
        {
            SelectSlot(3);
        });

        // 슬롯선택창 선택 버튼
        selectSlotYesBt.onClick.AddListener(() =>
        {
            if (isSave == true)
            {
                DataManager.Instance.slotNum = selectSlotNum;
                DataManager.Instance.SaveData();
                RenewalSlotText();
            }
            else
            {
                GameManager.Instance.LoadSaveDataScene(selectSlotNum);
                gameObject.SetActive(false);
                UiManager.Instance.optionPanel.gameObject.SetActive(false);
            }
            selectSlot.SetActive(false);
        });

        // 슬롯선택창 취소 버튼
        selectSlotNoBt.onClick.AddListener(() =>
        {
            selectSlot.SetActive(false);
        });
    } // Start

    private void OnEnable()
    {
        RenewalSlotText();
    } // OnEnable

    private void Update()
    {
        if (warning.activeInHierarchy == true)
        {
            // 경고창이 활성화되어 있을 때 아무키나 입력시 경고창 비활성화
            if (Input.anyKeyDown)
            {
                warning.SetActive(false);
            }
        }
    } // Update

    //! 저장슬롯 텍스트 갱신하는 함수
    private void RenewalSlotText()
    {
        DataManager.Instance.RenewalSavefile();
        for (int i = 0; i < DataManager.Instance.hasSavefile.Length; i++)
        {
            if (DataManager.Instance.hasSavefile[i] == true)
            {
                DataManager.Instance.slotNum = i;
                List<string> playerDatas = DataManager.Instance.LoadPlayerInfoData();
                PlayerStatus playerStat = JsonUtility.FromJson<PlayerStatus>(playerDatas[0]);
                saveSlotTexts[i].text = playerStat.Name;
                saveSlotTimeTexts[i].text = playerDatas[1];
            }
            else
            {
                saveSlotTexts[i].text = "비어있음";
                saveSlotTimeTexts[i].text = "";
            }
        }
    } // RenewalSlotText

    //! 슬롯 선택 함수
    private void SelectSlot(int num)
    {
        // 세이브패널 선택 시
        if (isSave == true)
        {
            if (DataManager.Instance.hasSavefile[num] == false)
            {
                // 선택한 슬롯에 세이브파일이 없을 경우
                selectSlotNum = num;
                selectSlotText.text = saveText;
                selectSlot.SetActive(true);
                return;
            }
            else
            {
                // 선택한 슬롯에 세이브파일이 있을 경우
                selectSlotNum = num;
                selectSlotText.text = hasSaveText;
                selectSlot.SetActive(true);
                return;
            }
        }
        else // 로드패널 선택 시
        {
            if (DataManager.Instance.hasSavefile[num] == false)
            {
                // 선택한 슬롯에 세이브파일이 없을 경우
                warning.SetActive(true);
                return;
            }
            else
            {
                // 선택한 슬롯에 세이브파일이 있을 경우
                selectSlotNum = num;
                selectSlotText.text = loadText;
                selectSlot.SetActive(true);
                return;
            }
        }
    } // SelectSlot
} // SaveAndLoadPanel
