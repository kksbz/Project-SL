using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour
{
    [SerializeField] private Button selectBt; // ���ù�ư
    [SerializeField] private Button cancelBt; // ��ҹ�ư
    [SerializeField] private GameObject warningObj; // ���â
    [SerializeField] private GameObject selectName; // ����Ȯ��â
    [SerializeField] private Button selectYesBt; // ����Ȯ�� ���ù�ư
    [SerializeField] private Button selectNoBt; // ����Ȯ�� ��ҹ�ư
    [SerializeField] private TMP_Text selectNameText; // ���������� �̸�
    [SerializeField] private TMP_InputField inputName; // �̸��Է�ĭ
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
