using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInvenController : MonoBehaviour
{
    [SerializeField] private GameObject EquipSlots;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            EquipSlots.SetActive(true);
            gameObject.SetActive(false);
        }
    } // Update
} // EquipInvenController
