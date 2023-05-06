using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInvenController : MonoBehaviour
{
    [SerializeField] private GameObject EquipSlots;
    public GameObject WarningPanel; // ¿Â¬¯ΩΩ∑‘ ∞Ê∞Ì√¢
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquipSlots.SetActive(true);
            gameObject.SetActive(false);
        }
        if (WarningPanel.activeInHierarchy == true)
        {
            if (Input.anyKeyDown)
            {
                WarningPanel.SetActive(false);
            }
        }
    } // Update

    private void OnDisable()
    {
        WarningPanel.SetActive(false);
    } // OnDisable
} // EquipInvenController
