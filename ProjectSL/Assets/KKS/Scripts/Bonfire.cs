using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public enum sceneName
    {
        Lobby,
        Test
    }

    [SerializeField] private GameObject fireEffect; // ȭ��� ����Ʈ
    private string activeSceneName; //ȭ����� �����ϴ� ���̸�
    public sceneName thisSceneName;
    public string bonfireName; // ȭ��� �̸�
    public BonfireData bonfireData; // ȭ��� ������
    private bool isEnterPlayer = false;

    private void Start()
    {
        activeSceneName = thisSceneName.ToString();
        bonfireData = new BonfireData(false, activeSceneName, bonfireName, transform.position);
        foreach (BonfireData _bonfireData in UiManager.Instance.warp.bonfireList)
        {
            // ���̺굥���� �ε� �� �����ߴ� ȭ����� �ڽŰ� ������ �����ߴ� �����ͷ� ���
            if (_bonfireData.bonfireName == bonfireName)
            {
                bonfireData = _bonfireData;
                fireEffect.SetActive(true);
                break;
            }
        }
    } // Start

    private void Update()
    {
        if (isEnterPlayer == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (bonfireData.hasBonfire == false)
                {
                    // ȭ����� ó�� Ȱ��ȭ��Ű�� ȭ������Ʈ Ȱ��ȭ
                    bonfireData.hasBonfire = true;
                    fireEffect.SetActive(true);
                    // ������Ʈ�ѷ� ȭ��Ҹ���Ʈ�� ȭ��� �߰� �� �������� ����
                    UiManager.Instance.warp.bonfireList.Add(bonfireData);
                    UiManager.Instance.warp.CreateWarpSlot(bonfireData);
                }
                UiManager.Instance.bonfirePanel.SetActive(true);
                UiManager.Instance.interactionBar.SetActive(false);
                UiManager.Instance.interactionText.text = null;
            }
        }
    } // Update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionText.text = "ȭ��� ��� : E Ű";
            UiManager.Instance.interactionBar.SetActive(true);
            isEnterPlayer = true;
        }
    } // OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionBar.SetActive(false);
            UiManager.Instance.interactionText.text = null;
            isEnterPlayer = false;
        }
    } // OnTriggerExit
} // Bonfire
