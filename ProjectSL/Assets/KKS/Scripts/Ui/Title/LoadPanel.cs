using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] private Button saveAutoBt; // �ڵ����� ����
    [SerializeField] private Button saveSlot1; // ���� ���� 1
    [SerializeField] private Button saveSlot2; // ���� ���� 2
    [SerializeField] private Button saveSlot3; // ���� ���� 3
    [SerializeField] private Button exitBt; // �ε��г� ���� ��ư
    // Start is called before the first frame update
    void Start()
    {
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
        // �ε��г� ���� ��ư
        exitBt.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    } // Start

    //! ���� ���� �Լ�
    private void SelectSlot(int num)
    {
        // ���̺������� ���� ��� ����
        if (DataManager.Instance.hasSavefile[num] == false)
        {
            return;
        }
        DataManager.Instance.slotNum = num;
        DataManager.Instance.LoadData();
    } // SelectSlot
} // LoadPanel
