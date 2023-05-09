using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBar : MonoBehaviour
{
    public Button equip; // 장비창
    public Button inven; // 통합인벤
    public Button stat; // 플레이어 스텟
    public Button warp; // 화톳불 이동창
    public Button option; // 옵션
    [SerializeField] private QuickWarpPanel quickWarpPanel;
    public GameObject inventory;
    public Sprite equipInven;
    public Sprite totalInven;
    void Start()
    {
        equip.onClick.AddListener(() =>
        {
            Debug.Log("장비 인벤 선택함");
            Inventory.Instance.topImage.sprite = equipInven;
            Inventory.Instance.topText.text = "장비";
            inventory.SetActive(true);
            gameObject.SetActive(false);
            UiManager.Instance.RenewalInvenStatusPanel();
        });

        inven.onClick.AddListener(() =>
        {
            Debug.Log("통합 인벤 선택함");
            Inventory.Instance.topImage.sprite = totalInven;
            Inventory.Instance.topText.text = "인벤토리";
            UiManager.Instance.RenewalInvenStatusPanel();
            inventory.SetActive(true);
            Inventory.Instance.equipSlotPanel.SetActive(false);
            Inventory.Instance.totalInvenPanel.SetActive(true);
            gameObject.SetActive(false);
        });

        stat.onClick.AddListener(() =>
        {
            Debug.Log("스테이터스 선택함");
            UiManager.Instance.RenewalStatusPanel();
            UiManager.Instance.statusPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });

        warp.onClick.AddListener(() =>
        {
            Debug.Log("화톳불 이동창 선택함");
            foreach (BonfireData _bonfire in UiManager.Instance.warp.bonfireList)
            {
                if (_bonfire.bonfireID == 0)
                {
                    quickWarpPanel.quickWarpPanel.SetActive(true);
                    quickWarpPanel.gameObject.SetActive(true);
                    return;
                }
            }
            quickWarpPanel.WarningPanel.SetActive(true);
            quickWarpPanel.gameObject.SetActive(true);
        });

        option.onClick.AddListener(() =>
        {
            Debug.Log("옵션창 선택함");
            UiManager.Instance.optionPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    } // Start

    private void OnEnable()
    {
        quickWarpPanel.gameObject.SetActive(false);
    } // OnEnable
} // QuickSlotBar
