using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Button continueBt; // �̾��ϱ� ��ư
    [SerializeField] private Button newGameBt; // ������ ��ư
    [SerializeField] private Button loadBt; // �ε� ��ư
    [SerializeField] private Button optionBt; // �ɼ� ��ư
    [SerializeField] private Button exitBt; // ���� ��ư
    [Header("Ÿ��Ʋ �г� ����")]
    [SerializeField] private GameObject newGamePanel; // �������г�
    [SerializeField] private GameObject loadPanel; // �ε��г�

    void Start()
    {
        //! �ڵ����� �����Ͱ� ������ ��Ƽ����ư Ȱ��ȭ
        if (DataManager.Instance.hasSavefile[0] == true)
        {
            continueBt.gameObject.SetActive(true);
        }
        // ����ϱ� ��ư
        continueBt.onClick.AddListener(() =>
        {
            // �ڵ����彽���� �����ͷ� �ٷ� ����
            GameManager.Instance.LoadSaveDataScene(0);
        });
        // ������ ��ư �÷��̾� �̸� �Է�â Ȱ��ȭ
        newGameBt.onClick.AddListener(() =>
        {
            newGamePanel.SetActive(true);
        });
        // �ε� ��ư ���̺� ���� �г� Ȱ��ȭ
        loadBt.onClick.AddListener(() =>
        {
            loadPanel.SetActive(true);
        });
        // �ɼ� ��ư
        optionBt.onClick.AddListener(() =>
        {

        });
        // ���� ��ư
        exitBt.onClick.AddListener(() =>
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        });
    } // Start
} // TitleController
