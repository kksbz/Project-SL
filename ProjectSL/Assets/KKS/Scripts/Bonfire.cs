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

    [SerializeField] private GameObject fireEffect; // 화톳불 이펙트
    private string activeSceneName; //화톳불이 존재하는 씬이름
    public sceneName thisSceneName;
    public string bonfireName; // 화톳불 이름
    public BonfireData bonfireData; // 화톳불 데이터
    private bool isEnterPlayer = false;

    private void Start()
    {
        activeSceneName = thisSceneName.ToString();
        bonfireData = new BonfireData(false, activeSceneName, bonfireName, transform.position);
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
                    // 화톳불을 처음 활성화시키면 화염이펙트 활성화
                    bonfireData.hasBonfire = true;
                    fireEffect.SetActive(true);
                    // 워프컨트롤러 화톳불리스트에 화톳불 추가 및 워프슬롯 생성
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
            UiManager.Instance.interactionText.text = "화톳불 사용 : E 키";
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
