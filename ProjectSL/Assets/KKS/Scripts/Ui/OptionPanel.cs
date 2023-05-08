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
    [SerializeField] private GameObject checkPanel; // Ȯ�� �г�
    [SerializeField] private GameObject manualPanel;
    [SerializeField] private TMP_Text checkText; // Ȯ�� �г� �ؽ�Ʈ
    [SerializeField] private Button checkPanelSelectBt; // Ȯ�� �г� ���� ��ư
    [SerializeField] private Button checkPanelCancleBt; // Ȯ�� �г� ��� ��ư
    [SerializeField] private Button goTitleBt; // Ÿ��Ʋ�� ���� ��ư
    [SerializeField] private Button OptionBt; // �ɼ� ��ư
    [SerializeField] private Button SaveBt; // �����ϱ� ��ư
    [SerializeField] private Button LoadBt; // �ε��ϱ� ��ư
    [SerializeField] private Button ExitBt; // �������� ��ư
    public GameObject goBackText; // �ϴ��г� �ڷΰ��� �ؽ�Ʈ
    private bool isExitGame = false;

    private void Start()
    {
        // Ÿ��Ʋ�� ���� ��ư
        goTitleBt.onClick.AddListener(() =>
        {
            checkText.text = "Ÿ��Ʋȭ������ ���ðڽ��ϱ�?";
            isExitGame = false;
            checkPanel.SetActive(true);
        });
        // �������� ��ư
        ExitBt.onClick.AddListener(() =>
        {
            checkText.text = "������ �����Ͻðڽ��ϱ�?";
            isExitGame = true;
            checkPanel.SetActive(true);
        });
        // �ɼ� ��ư
        OptionBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[1];
            panelText.text = "���۹�";
            manualPanel.SetActive(true);
            goBackText.SetActive(true);
        });
        // ���̺� ��ư
        SaveBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[2];
            panelText.text = "�����ϱ�";
            saveAndLoadPanel.isSave = true;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });
        // �ε� ��ư
        LoadBt.onClick.AddListener(() =>
        {
            panelImage.sprite = panelSprites[3];
            panelText.text = "�ҷ�����";
            saveAndLoadPanel.isSave = false;
            saveAndLoadPanel.gameObject.SetActive(true);
            goBackText.SetActive(true);
        });

        // Ȯ�� �г� ���� ��ư
        checkPanelSelectBt.onClick.AddListener(() =>
        {
            if (isExitGame == true)
            {
                // �ڵ����� �� ��������
                DataManager.Instance.slotNum = 0;
                DataManager.Instance.SaveData();
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                // Ÿ��Ʋ������ �̵�
                checkPanel.SetActive(false);
                GameManager.Instance.GoTitleScene();
            }
        });
        // Ȯ�� �г� ��� ��ư
        checkPanelCancleBt.onClick.AddListener(() =>
        {
            checkPanel.SetActive(false);
        });
    } // Start

    private void OnEnable()
    {
        panelImage.sprite = panelSprites[0];
        panelText.text = "����";
        goBackText.SetActive(false);
        manualPanel.SetActive(false);
        saveAndLoadPanel.gameObject.SetActive(false);
    } // OnEnable

    private void Update()
    {
        if (goBackText.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                saveAndLoadPanel.gameObject.SetActive(false);
                manualPanel.SetActive(false);
                panelImage.sprite = panelSprites[0];
                panelText.text = "����";
                goBackText.SetActive(false);
            }
        }
    } // Update
} // OptionPanel
