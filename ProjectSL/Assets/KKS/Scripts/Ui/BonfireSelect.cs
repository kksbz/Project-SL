using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonfireSelect : MonoBehaviour
{
    [SerializeField] private Button selectBt;
    [SerializeField] private Button exitBt;
    [SerializeField] private GameObject warpPanel;
    // Start is called before the first frame update
    void Start()
    {
        selectBt.onClick.AddListener(() =>
        {
            warpPanel.SetActive(true);
            gameObject.SetActive(false);
        });

        exitBt.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    } // Start
} // BonfireSelect
