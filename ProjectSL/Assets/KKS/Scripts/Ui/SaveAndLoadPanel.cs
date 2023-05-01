using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoadPanel : MonoBehaviour
{
    public bool isSave; // ���̺�, �ε� ���� ����
    [SerializeField] List<TMP_Text> saveSlotTexts;
    [SerializeField] List<TMP_Text> saveSlotTimeTexts;
    [SerializeField] private Button saveAutoBt; // �ڵ����� ����
    [SerializeField] private Button saveSlot1; // ���� ���� 1
    [SerializeField] private Button saveSlot2; // ���� ���� 2
    [SerializeField] private Button saveSlot3; // ���� ���� 3
    [SerializeField] private GameObject warning; // ���â
    [SerializeField] private GameObject selectSlot; // ���Լ���â
    [SerializeField] private TMP_Text selectSlotText; // ���Լ���â �ؽ�Ʈ
    [SerializeField] private Button selectSlotYesBt; // ���Լ���â ���ù�ư
    [SerializeField] private Button selectSlotNoBt; // ���Լ���â ��ҹ�ư
    private string saveText = "������ ���Կ� �����Ͻðڽ��ϱ�?";
    private string hasSaveText = "����� �����Ͱ� �ֽ��ϴ�. �����ðڽ��ϱ�?";
    private string loadText = "������ �������� �����Ͻðڽ��ϱ�?";
    private int selectSlotNum;
    void Start()
    {
        // �ڵ����� ���� ���� ��ư
        saveAutoBt.onClick.AddListener(() =>
        {
            SelectSlot(0);
        });

        // ���彽��1 ���� ��ư
        saveSlot1.onClick.AddListener(() =>
        {
            SelectSlot(1);
        });

        // ���彽��2 ���� ��ư
        saveSlot2.onClick.AddListener(() =>
        {
            SelectSlot(2);
        });

        // ���彽��3 ���� ��ư
        saveSlot3.onClick.AddListener(() =>
        {
            SelectSlot(3);
        });

        // ���Լ���â ���� ��ư
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

        // ���Լ���â ��� ��ư
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
            // ���â�� Ȱ��ȭ�Ǿ� ���� �� �ƹ�Ű�� �Է½� ���â ��Ȱ��ȭ
            if (Input.anyKeyDown)
            {
                warning.SetActive(false);
            }
        }
    } // Update

    //! ���彽�� �ؽ�Ʈ �����ϴ� �Լ�
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
                saveSlotTexts[i].text = "�������";
                saveSlotTimeTexts[i].text = "";
            }
        }
    } // RenewalSlotText

    //! ���� ���� �Լ�
    private void SelectSlot(int num)
    {
        // ���̺��г� ���� ��
        if (isSave == true)
        {
            if (DataManager.Instance.hasSavefile[num] == false)
            {
                // ������ ���Կ� ���̺������� ���� ���
                selectSlotNum = num;
                selectSlotText.text = saveText;
                selectSlot.SetActive(true);
                return;
            }
            else
            {
                // ������ ���Կ� ���̺������� ���� ���
                selectSlotNum = num;
                selectSlotText.text = hasSaveText;
                selectSlot.SetActive(true);
                return;
            }
        }
        else // �ε��г� ���� ��
        {
            if (DataManager.Instance.hasSavefile[num] == false)
            {
                // ������ ���Կ� ���̺������� ���� ���
                warning.SetActive(true);
                return;
            }
            else
            {
                // ������ ���Կ� ���̺������� ���� ���
                selectSlotNum = num;
                selectSlotText.text = loadText;
                selectSlot.SetActive(true);
                return;
            }
        }
    } // SelectSlot
} // SaveAndLoadPanel
