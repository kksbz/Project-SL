using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetModelChanger : ModelChanger
{
    /*
    public List<GameObject> _helmetModels;
    public List<SkinnedMeshRenderer> _helmetMeshes;
    private void Awake()
    {
        GetAllHelmetModels();
        GetAllHelmetMeshes();
    }
    private void Start()
    {
        UnEquipAllHelmetModels();
    }
    private void GetAllHelmetModels()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _helmetModels.Add(transform.GetChild(i).gameObject);
        }
    }
    private void GetAllHelmetMeshes()
    {
        foreach (GameObject helmetModel in _helmetModels)
        {
            _helmetMeshes.Add(helmetModel.GetComponentInChildren<SkinnedMeshRenderer>());
        }
    }
    public void UnEquipAllHelmetModels()
    {
        foreach (GameObject helmetModel in _helmetModels)
        {
            helmetModel.SetActive(false);
        }
    }
    public void EquipHelmetModelByName(string helmetName)
    {
        UnEquipAllHelmetModels();
        for (int i = 0; i < _helmetModels.Count; i++)
        {
            if (_helmetModels[i].name == helmetName)
            {
                _helmetModels[i].SetActive(true);
            }
        }
    }
    */
}
