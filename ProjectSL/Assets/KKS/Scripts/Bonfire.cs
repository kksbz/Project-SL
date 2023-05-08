using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private GameObject fireEffect; // 화톳불 이펙트
    public int bonfireID; // 화톳불 인덱스
    public string bonfireName; // 화톳불 이름
    public BonfireData bonfireData; // 화톳불 데이터
    private bool isEnterPlayer = false;

    private void Start()
    {
        bonfireData = new BonfireData(bonfireID, false, bonfireName, transform.position);
    } // Start

    private void Update()
    {
        if (isEnterPlayer == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.player.StateMachine.LockInput();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (bonfireData.hasBonfire == false)
                {
                    // 화톳불 활성화 메시지 표시
                    UiManager.Instance.messagePanel.BonfireMessage();
                    // 화톳불을 처음 활성화시키면 화염이펙트 활성화
                    bonfireData.hasBonfire = true;
                    fireEffect.SetActive(true);
                    // 워프컨트롤러 화톳불리스트에 화톳불 추가 및 워프슬롯 생성
                    UiManager.Instance.warp.bonfireList.Add(bonfireData);
                    UiManager.Instance.warp.bonfireList.OrderBy(x => x.bonfireID).ToList();
                    UiManager.Instance.warp.CreateWarpSlot(bonfireData);
                }
                UiManager.Instance.bonfirePanel.SetActive(true);
                UiManager.Instance.interactionBar.SetActive(false);
                UiManager.Instance.interactionText.text = null;

                // 플레이어 HP, MP, ST 풀로 회복
                GameManager.Instance.player.HealthSys.HealHP(GameManager.Instance.player.GetPlayerData()._healthSystemData.MaxHP);
                GameManager.Instance.player.HealthSys.HealMP(GameManager.Instance.player.GetPlayerData()._healthSystemData.MaxMP);

                // 화톳불 사용시 에스트병 보유 수량 최대로 회복
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

    // 화톳불 데이터 초기화하는 함수
    public void InitBonfireData()
    {
        foreach (BonfireData _bonfireData in UiManager.Instance.warp.bonfireList)
        {
            // 세이브데이터 로드 시 저장했던 화톳불이 자신과 같으면 저장했던 데이터로 덮어씀
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
            UiManager.Instance.interactionText.text = "화톳불 사용 : E 키";
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
