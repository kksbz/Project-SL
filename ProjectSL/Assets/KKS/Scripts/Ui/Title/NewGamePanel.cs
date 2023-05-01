using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour
{
    [SerializeField] private Button selectBt; // 선택버튼
    [SerializeField] private Button cancelBt; // 취소버튼
    [SerializeField] private GameObject warningObj; // 경고창
    [SerializeField] private GameObject selectName; // 최종확인창
    [SerializeField] private Button selectYesBt; // 최종확인 선택버튼
    [SerializeField] private Button selectNoBt; // 최종확인 취소버튼
    [SerializeField] private TMP_Text selectNameText; // 최종결정한 이름
    [SerializeField] private TMP_InputField inputName; // 이름입력칸
    public int selectSlotNum = 0;
    void Start()
    {
        selectBt.onClick.AddListener(() =>
        {
            selectNameText.text = inputName.text;
            selectName.SetActive(true);
        });

        cancelBt.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        selectYesBt.onClick.AddListener(() =>
        {
            DataManager.Instance.selectPlayerName = selectNameText.text;
            GameManager.Instance.NewGamePlay(selectSlotNum);
            selectName.SetActive(false);
        });

        selectNoBt.onClick.AddListener(() =>
        {
            selectName.SetActive(false);
        });
    } // Start

    private void OnDisable()
    {
        inputName.text = "";
    } // OnDisable

    void Update()
    {
        if (warningObj.activeInHierarchy == true)
        {
            if (Input.anyKeyDown)
            {
                warningObj.SetActive(false);
            }
        }

        if (inputName.text.Length > 8)
        {
            warningObj.SetActive(true);
            inputName.text = "";
        }
    } // Update
} // NewGamePanel
