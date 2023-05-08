using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WarpController : MonoBehaviour
{
    [SerializeField] private GameObject warpSlotPrefab; // �������� ������
    [SerializeField] private GameObject warpSlotList; // �������� �θ������Ʈ
    [SerializeField] GameObject selectPanel; // �����г�
    public GameObject warpPanel; // �����г�
    public GameObject warpSelect; // �������� �г�
    private Button warpSelectBt; // �������� �г� ���ù�ư
    private Button warpSelectExitBt; // �������� �г� �������ư
    private List<WarpSlot> warpSlots = new List<WarpSlot>();
    public List<BonfireData> bonfireList = new List<BonfireData>(); // ȭ��� ����Ʈ
    public WarpSlot selectWarp; // ������ ��������

    // Start is called before the first frame update
    void Start()
    {
        warpSelectBt = warpSelect.transform.Find("SelectBt").GetComponent<Button>();
        warpSelectExitBt = warpSelect.transform.Find("ExitBt").GetComponent<Button>();

        warpSelectBt.onClick.AddListener(() =>
        {
            // �÷��̾��� ��ġ�� ������ ȭ��� ��ġ�� �̵���Ŵ
            GameManager.Instance.LoadBonfire(selectWarp.bonfire);
            warpSelect.SetActive(false);
            warpPanel.SetActive(false);
            GameManager.Instance.player.StateMachine.ResetInput();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        });

        warpSelectExitBt.onClick.AddListener(() =>
        {
            warpSelect.SetActive(false);
        });
    } // Start

    //! �������� �����ϴ� �Լ�
    public void CreateWarpSlot(BonfireData _bonfire)
    {
        GameObject warpSlotObj = Instantiate(warpSlotPrefab);
        WarpSlot warpSlot = warpSlotObj.GetComponent<WarpSlot>();
        warpSlot.bonfire = _bonfire;
        warpSlots.Add(warpSlot);
        warpSlots = warpSlots.OrderBy(x => x.bonfire.bonfireID).ToList();
        for (int i = 0; i < warpSlots.Count; i++)
        {
            warpSlots[i].transform.SetParent(null);
            warpSlots[i].transform.parent = warpSlotList.transform;
        }
    } // CreateWarpSlot
} // WarpController
