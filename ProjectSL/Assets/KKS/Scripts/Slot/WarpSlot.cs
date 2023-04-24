using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarpSlot : MonoBehaviour
{
    [SerializeField] private WarpController warpController;
    [SerializeField] private Button warpBt;
    [SerializeField] private TMP_Text warpText;
    public BonfireData bonfire;
    // Start is called before the first frame update
    void Start()
    {
        warpController = UiManager.Instance.warp;
        warpText.text = bonfire.bonfireName;

        warpBt.onClick.AddListener(() =>
        {
            warpController.selectWarp = this;
            warpController.warpSelect.SetActive(true);
        });
    } // Start
} // WarpSlot
