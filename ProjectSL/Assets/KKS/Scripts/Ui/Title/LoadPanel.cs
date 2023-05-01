using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] private Button saveAutoBt; // �ڵ����� ����
    [SerializeField] private Button saveSlot1; // ���� ���� 1
    [SerializeField] private Button saveSlot2; // ���� ���� 2
    [SerializeField] private Button saveSlot3; // ���� ���� 3
    [SerializeField] private List<TMP_Text> saveSlotTexts;
    [SerializeField] private List<TMP_Text> saveSlotTimeTexts;
    [SerializeField] private GameObject newGamePanel; // �󽽷� Ŭ���� ������ ����â
    [SerializeField] private Button newGameYesBt; // ������ ���ù�ư
    [SerializeField] private Button newGameNoBt; // ������ ��ҹ�ư
    [SerializeField] private NewGamePanel newGame; // ������ �̸��Է�â
    [SerializeField] private GameObject saveSlotSelect; // ���彽�� ���� Ȯ��â
    [SerializeField] private Button saveSlotSelectYesBt; // ���彽�� ���� ��ư
    [SerializeField] private Button saveSlotSelectNoBt; // ���彽�� ��� ��ư
    private int selectNum;

    void Start()
    {
        // ���彽�� �ؽ�Ʈ �Ҵ�
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
                saveSlotTexts[i].text = "�������";
                saveSlotTimeTexts[i].text = "";
            }
        }
        // �ڵ����� ���� ��ư
        saveAutoBt.onClick.AddListener(() =>
        {
            SelectSlot(0);
        });
        // ���� ���� 1�� ��ư
        saveSlot1.onClick.AddListener(() =>
        {
            SelectSlot(1);
        });
        // ���� ���� 2�� ��ư
        saveSlot2.onClick.AddListener(() =>
        {
            SelectSlot(2);
        });
        // ���� ���� 3�� ��ư
        saveSlot3.onClick.AddListener(() =>
        {
            SelectSlot(3);
        });
        // ������ ���� ��ư
        newGameYesBt.onClick.AddListener(() =>
        {
            newGame.gameObject.SetActive(true);
            newGamePanel.SetActive(false);
        });
        // ������ ��� ��ư
        newGameNoBt.onClick.AddListener(() =>
        {
            newGame.selectSlotNum = 0;
            newGamePanel.SetActive(false);
        });
        // ���彽�� ���� ��ư
        saveSlotSelectYesBt.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadSaveDataScene(selectNum);
            saveSlotSelect.SetActive(false);
        });
        // ���彽�� ��� ��ư
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

    //! ���� ���� �Լ�
    private void SelectSlot(int num)
    {
        // ���̺������� ���� ��� �������г� Ȱ��ȭ ����
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
