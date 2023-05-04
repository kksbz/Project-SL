using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] private Button saveAutoBt; // 자동저장 슬롯
    [SerializeField] private Button saveSlot1; // 저장 슬롯 1
    [SerializeField] private Button saveSlot2; // 저장 슬롯 2
    [SerializeField] private Button saveSlot3; // 저장 슬롯 3
    [SerializeField] private List<TMP_Text> saveSlotTexts;
    [SerializeField] private List<TMP_Text> saveSlotTimeTexts;
    [SerializeField] private GameObject newGamePanel; // 빈슬롯 클릭시 뉴게임 선택창
    [SerializeField] private Button newGameYesBt; // 뉴게임 선택버튼
    [SerializeField] private Button newGameNoBt; // 뉴게임 취소버튼
    [SerializeField] private NewGamePanel newGame; // 뉴게임 이름입력창
    [SerializeField] private GameObject saveSlotSelect; // 저장슬롯 선택 확인창
    [SerializeField] private Button saveSlotSelectYesBt; // 저장슬롯 선택 버튼
    [SerializeField] private Button saveSlotSelectNoBt; // 저장슬롯 취소 버튼
    private int selectNum;

    void Start()
    {
        // 저장슬롯 텍스트 할당
        for (int i = 0; i < DataManager.Instance.hasSavefile.Length; i++)
        {
            if (DataManager.Instance.hasSavefile[i] == true)
            {
                DataManager.Instance.slotNum = i;
                List<string> playerDatas = DataManager.Instance.LoadPlayerInfoData();
                StatusSaveData playerStat = JsonUtility.FromJson<StatusSaveData>(playerDatas[0]);
                saveSlotTexts[i].text = playerStat._playerStatusData.Name;
                saveSlotTimeTexts[i].text = playerDatas[1];
            }
            else
            {
                saveSlotTexts[i].text = "비어있음";
                saveSlotTimeTexts[i].text = "";
            }
        }
        // 자동저장 슬롯 버튼
        saveAutoBt.onClick.AddListener(() =>
        {
            SelectSlot(0);
        });
        // 저장 슬롯 1번 버튼
        saveSlot1.onClick.AddListener(() =>
        {
            SelectSlot(1);
        });
        // 저장 슬롯 2번 버튼
        saveSlot2.onClick.AddListener(() =>
        {
            SelectSlot(2);
        });
        // 저장 슬롯 3번 버튼
        saveSlot3.onClick.AddListener(() =>
        {
            SelectSlot(3);
        });
        // 뉴게임 선택 버튼
        newGameYesBt.onClick.AddListener(() =>
        {
            newGame.gameObject.SetActive(true);
            newGamePanel.SetActive(false);
        });
        // 뉴게임 취소 버튼
        newGameNoBt.onClick.AddListener(() =>
        {
            newGame.selectSlotNum = 0;
            newGamePanel.SetActive(false);
        });
        // 저장슬롯 선택 버튼
        saveSlotSelectYesBt.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadSaveDataScene(selectNum);
            saveSlotSelect.SetActive(false);
        });
        // 저장슬롯 취소 버튼
        saveSlotSelectNoBt.onClick.AddListener(() =>
        {
            saveSlotSelect.SetActive(false);
        });
    } // Start

    private void OnDisable()
    {
        newGame.selectSlotNum = 0;
    } // OnDisable

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    } // Update

    //! 슬롯 선택 함수
    private void SelectSlot(int num)
    {
        // 세이브파일이 없을 경우 뉴게임패널 활성화 리턴
        if (DataManager.Instance.hasSavefile[num] == false)
        {
            newGame.selectSlotNum = num;
            newGamePanel.SetActive(true);
            return;
        }
        selectNum = num;
        saveSlotSelect.SetActive(true);
    } // SelectSlot
} // LoadPanel
