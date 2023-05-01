using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] List<Sprite> panelSprites; // ����г� ��������Ʈ ����Ʈ
    [SerializeField] private Image panelImage; // ����г� �̹���
    [SerializeField] private TMP_Text panelText; // ����г� �ؽ�Ʈ
    [SerializeField] private SaveAndLoadPanel saveAndLoadPanel; // ���̺�,�ε� �г�
    [SerializeField] private Button OptionBt;
    [SerializeField] private Button SaveBt;
    [SerializeField] private Button LoadBt;
    [SerializeField] private Button ExitBt;
    public GameObject goBackText; // �ϴ��г� �ڷΰ��� �ؽ�Ʈ

    private void Start()
    {
        // �ɼ� ��ư
        OptionBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[1];
            panelText.text = "����";
            goBackText.SetActive(true);
        });
        // ���̺� ��ư
        SaveBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[2];
            panelText.text = "���̺�";
            saveAndLoadPanel.isSave = true;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });
        // �ε� ��ư
        LoadBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[3];
            panelText.text = "�ε�";
            saveAndLoadPanel.isSave = false;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });
        // �������� ��ư
        ExitBt.onClick.AddListener(() =>
        {
            // �ڵ����� �� ��������
            DataManager.Instance.slotNum = 0;
            DataManager.Instance.SaveData();
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        });
    } // Start

    private void OnEnable()
    {
        panelImage.sprite = panelSprites[0];
        panelText.text = "�ɼ�";
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
                panelText.text = "�ɼ�";
                goBackText.SetActive(false);
            }
        }
    } // Update
} // OptionPanel
