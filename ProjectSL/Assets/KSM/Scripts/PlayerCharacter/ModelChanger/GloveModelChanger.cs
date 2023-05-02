using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveModelChanger : MonoBehaviour
{
    public List<GameObject> _gloveModels;
    private void Awake()
    {
        GetAllGloveModels();
    }
    private void GetAllGloveModels()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _gloveModels.Add(transform.GetChild(i).gameObject);
        }
    }
    public void UnEquipAllGloveModels()
    {
        foreach (GameObject gloveModel in _gloveModels)
        {
            gloveModel.SetActive(false);
        }
    }
    public void EquipGloveModelByName(string pantName)
    {
        for (int i = 0; i < _gloveModels.Count; i++)
        {
            if (_gloveModels[i].name == pantName)
            {
                _gloveModels[i].SetActive(true);
            }
        }
    }
}
