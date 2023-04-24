using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpController : MonoBehaviour
{
    [SerializeField] private GameObject warpSlotPrefab; // 워프슬롯 프리팹
    [SerializeField] private Button exitBt; // 나가기 버튼
    [SerializeField] private GameObject warpSlotList; // 워프슬롯 부모오브젝트
    [SerializeField] GameObject selectPanel; // 선택패널
    [SerializeField] GameObject warpPanel; // 워프패널
    public GameObject warpSelect; // 워프선택 패널
    private Button warpSelectBt; // 워프선택 패널 선택버튼
    private Button warpSelectExitBt; // 워프선택 패널 나가기버튼
    public List<BonfireData> bonfireList = new List<BonfireData>(); // 화톳불 리스트
    public WarpSlot selectWarp; // 선택한 워프슬롯

    // Start is called before the first frame update
    void Start()
    {
        warpSelectBt = warpSelect.transform.Find("SelectBt").GetComponent<Button>();
        warpSelectExitBt = warpSelect.transform.Find("ExitBt").GetComponent<Button>();

        exitBt.onClick.AddListener(() =>
        {
            warpPanel.SetActive(false);
            selectPanel.SetActive(true);
        });

        warpSelectBt.onClick.AddListener(() =>
        {
            // 플레이어의 위치를 선택한 화톳불 위치로 이동시킴
            StartCoroutine(GameManager.Instance.LoadScene(selectWarp.bonfire));
            warpSelect.SetActive(false);
            warpPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        });

        warpSelectExitBt.onClick.AddListener(() =>
        {
            warpSelect.SetActive(false);
        });
    } // Start

    //! 워프슬롯 생성하는 함수
    public void CreateWarpSlot(BonfireData _bonfire)
    {
        GameObject warpSlot = Instantiate(warpSlotPrefab);
        warpSlot.transform.parent = warpSlotList.transform;
        warpSlot.GetComponent<WarpSlot>().bonfire = _bonfire;
    } // CreateWarpSlot
} // WarpController
