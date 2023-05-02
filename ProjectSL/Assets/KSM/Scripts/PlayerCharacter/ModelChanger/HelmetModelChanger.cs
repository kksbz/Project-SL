using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetModelChanger : MonoBehaviour
{
    public List<GameObject> _helmetModels;
    private void Awake()
    {
        GetAllHelmetModels();
    }
    private void GetAllHelmetModels()
    {
        int childrenGameobjects = transform.childCount;
        for(int i = 0; i < childrenGameobjects; i++)
        {
            _helmetModels.Add(transform.GetChild(i).gameObject);
        }
    }
    public void UnEquipAllHelmetModels()
    {
        foreach(GameObject helmetModel in _helmetModels)
        {
            helmetModel.SetActive(false);
        }
    }
    public void EquipHelmetModelByName(string helmetName)
    {
        for(int i = 0; i < _helmetModels.Count; i++)
        {
            if (_helmetModels[i].name == helmetName)
            {
                _helmetModels[i].SetActive(true);
            }
        }
    }
}
