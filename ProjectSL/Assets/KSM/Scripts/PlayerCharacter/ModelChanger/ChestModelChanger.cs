using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestModelChanger : MonoBehaviour
{
    public List<GameObject> _chestModels;
    public List<SkinnedMeshRenderer> _chestMeshes;
    private void Awake()
    {
        GetAllChestModels();
        GetAllChestMeshes();
    }
    private void Start()
    {
        UnEquipAllChestModels();
    }
    private void GetAllChestModels()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _chestModels.Add(transform.GetChild(i).gameObject);
        }
    }
    private void GetAllChestMeshes()
    {
        foreach (GameObject chestModel in _chestModels)
        {
            _chestMeshes.Add(chestModel.GetComponentInChildren<SkinnedMeshRenderer>());
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
        UnEquipAllChestModels();
        for (int i = 0; i < _chestModels.Count; i++)
        {
            if (_chestModels[i].name == chestName)
            {
                _chestModels[i].SetActive(true);
            }
        }
    }
}
