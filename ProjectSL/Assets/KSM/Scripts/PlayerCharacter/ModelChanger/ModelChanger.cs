using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChanger : MonoBehaviour
{
    [SerializeField] protected List<GameObject> _modelObjs;
    [SerializeField] protected List<SkinnedMeshRenderer> _modelMeshes;

    // Property
    public List<SkinnedMeshRenderer> ModelMeshes { get { return _modelMeshes; } }
    //
    protected void Awake()
    {
        GetAllModel();
        GetAllMesh();
    }
    // Start is called before the first frame update
    void Start()
    {
        UnEquipAllModel();
    }

    protected virtual void GetAllModel()
    {
        int childrenGameobjects = transform.childCount;
        for (int i = 0; i < childrenGameobjects; i++)
        {
            _modelObjs.Add(transform.GetChild(i).gameObject);
        }
    }
    protected virtual void GetAllMesh()
    {
        foreach (GameObject modelObj in _modelObjs)
        {
            _modelMeshes.AddRange(modelObj.GetComponentsInChildren<SkinnedMeshRenderer>());
        }
    }
    public virtual void UnEquipAllModel()
    {
        foreach (GameObject modelObj in _modelObjs)
        {
            modelObj.SetActive(false);
        }
    }
    public virtual void EquipModelByName(string pantName)
    {
        UnEquipAllModel();
        for (int i = 0; i < _modelObjs.Count; i++)
        {
            if (_modelObjs[i].name == pantName)
            {
                _modelObjs[i].SetActive(true);
            }
        }
    }
}
