using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantModelChanger : MonoBehaviour
{
    public List<GameObject> _pantModels;
    public List<SkinnedMeshRenderer> _pantMeshes;
    private void Awake()
    {
        GetAllPantModels();
        GetAllPantMeshes();
    }
    private void Start()
    {
        UnEquipAllPantModels();
    }
    private void GetAllPantModels()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _pantModels.Add(transform.GetChild(i).gameObject);
        }
    }
    private void GetAllPantMeshes()
    {
        foreach (GameObject pantModel in _pantModels)
        {
            _pantMeshes.Add(pantModel.GetComponentInChildren<SkinnedMeshRenderer>());
        }
    }
    public void UnEquipAllPantModels()
    {
        foreach (GameObject pantModel in _pantModels)
        {
            pantModel.SetActive(false);
        }
    }
    public void EquipPantModelByName(string pantName)
    {
        UnEquipAllPantModels();
        for (int i = 0; i < _pantModels.Count; i++)
        {
            if (_pantModels[i].name == pantName)
            {
                _pantModels[i].SetActive(true);
            }
        }
    }
}
