using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveModelChanger : MonoBehaviour
{
    public List<GameObject> _gloveModels;
    public List<SkinnedMeshRenderer> _gloveMeshes;
    private void Awake()
    {
        GetAllGloveModels();
        GetAllGloveMeshes();
    }
    private void Start()
    {
        UnEquipAllGloveModels();
    }
    private void GetAllGloveModels()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _gloveModels.Add(transform.GetChild(i).gameObject);
        }
    }

    private void GetAllGloveMeshes()
    {
        foreach (GameObject gloveModel in _gloveModels)
        {
            _gloveMeshes.Add(gloveModel.GetComponentInChildren<SkinnedMeshRenderer>());
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
        UnEquipAllGloveModels();
        for (int i = 0; i < _gloveModels.Count; i++)
        {
            if (_gloveModels[i].name == pantName)
            {
                _gloveModels[i].SetActive(true);
            }
        }
    }
}
