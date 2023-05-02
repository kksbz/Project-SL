using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantModelChanger : MonoBehaviour
{
    public List<GameObject> _pantModels;
    private void Awake()
    {
        GetAllPantModels();
    }
    private void GetAllPantModels()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _pantModels.Add(transform.GetChild(i).gameObject);
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
        for (int i = 0; i < _pantModels.Count; i++)
        {
            if (_pantModels[i].name == pantName)
            {
                _pantModels[i].SetActive(true);
            }
        }
    }
}
