using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Button newGameBt; // ������ ��ư
    [SerializeField] private Button loadBt; // �ε� ��ư
    [SerializeField] private Button optionBt; // �ɼ� ��ư
    [SerializeField] private Button exitBt; // ���� ��ư
    [Header("Ÿ��Ʋ �г� ����")]
    [SerializeField] private GameObject loadPanel; // �ε��г�
    // Start is called before the first frame update
    void Start()
    {
        // ������ ��ư
        newGameBt.onClick.AddListener(() =>
        {

        });
        // �ε� ��ư
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
        });
    } // Start
} // TitleController
