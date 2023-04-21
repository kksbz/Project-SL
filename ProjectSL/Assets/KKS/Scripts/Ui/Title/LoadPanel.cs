using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] private Button saveAutoBt; // 자동저장 슬롯
    [SerializeField] private Button saveSlot1; // 저장 슬롯 1
    [SerializeField] private Button saveSlot2; // 저장 슬롯 2
    [SerializeField] private Button saveSlot3; // 저장 슬롯 3
    [SerializeField] private Button exitBt; // 로드패널 종료 버튼
    // Start is called before the first frame update
    void Start()
    {
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
        // 로드패널 종료 버튼
        exitBt.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    } // Start

    //! 슬롯 선택 함수
    private void SelectSlot(int num)
    {
        // 세이브파일이 없을 경우 리턴
        if (DataManager.Instance.hasSavefile[num] == false)
        {
            return;
        }
        DataManager.Instance.slotNum = num;
        DataManager.Instance.LoadData();
    } // SelectSlot
} // LoadPanel
