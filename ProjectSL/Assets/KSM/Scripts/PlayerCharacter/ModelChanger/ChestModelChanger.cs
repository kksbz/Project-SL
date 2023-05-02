using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestModelChanger : MonoBehaviour
{
    public List<GameObject> _chestModels;
    private void Awake()
    {
        GetAllChestModels();
    }
    private void GetAllChestModels()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _chestModels.Add(transform.GetChild(i).gameObject);
        }
    }
    public void UnEquipAllChestModels()
    {
        foreach (GameObject chestModel in _chestModels)
        {
            chestModel.SetActive(false);
        }
    }
    public void EquipChestModelByName(string chestName)
    {
        for (int i = 0; i < _chestModels.Count; i++)
        {
            if (_chestModels[i].name == chestName)
            {
                _chestModels[i].SetActive(true);
            }
        }
    }
}
