using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantModelChanger : ModelChanger
{
    //public List<GameObject> _pantModels;
    //public List<SkinnedMeshRenderer> _pantMeshes;

    /*
     * Legacy Code
    protected override void GetAllModel()
    {
        
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
    */
}
