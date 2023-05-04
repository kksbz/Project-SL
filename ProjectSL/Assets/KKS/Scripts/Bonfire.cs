using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private GameObject fireEffect; // ȭ��� ����Ʈ
    public string bonfireName; // ȭ��� �̸�
    public BonfireData bonfireData; // ȭ��� ������
    private bool isEnterPlayer = false;

    private void Start()
    {
        bonfireData = new BonfireData(false, bonfireName, transform.position);

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
                for (int i = 0; i < Inventory.Instance.inventory.Count; i++)
                {
                    if (Inventory.Instance.inventory[i].itemID == 1)
                    {
                        Inventory.Instance.inventory[i].Quantity = Inventory.Instance.inventory[i].maxQuantity;
                    }
                    if (Inventory.Instance.inventory[i].itemID == 2)
                    {
                        Inventory.Instance.inventory[i].Quantity = Inventory.Instance.inventory[i].maxQuantity;
                        break;
                    }
                }
            }
        }
    } // Update

    // ȭ��� ������ �ʱ�ȭ�ϴ� �Լ�
    public void InitBonfireData()
    {
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
    } // InitBonfireData

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
            UiManager.Instance.bonfirePanel.SetActive(false);
            UiManager.Instance.interactionBar.SetActive(false);
            UiManager.Instance.interactionText.text = null;
            isEnterPlayer = false;
        }
    } // OnTriggerExit
} // Bonfire
