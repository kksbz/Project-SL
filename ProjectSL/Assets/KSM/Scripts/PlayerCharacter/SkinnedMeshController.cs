using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SkinnedMeshController : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer _mannequinMesh;

    [SerializeField]
    ModelChanger _helmetModelChanger;
    [SerializeField]
    ModelChanger _chestModelChanger;
    [SerializeField]
    ModelChanger _gloveModelChanger;
    [SerializeField]
    ModelChanger _pantModelChanger;
    

    private void Awake()
    {
        GameObject meshObj = transform.parent.gameObject;
        _mannequinMesh = meshObj.GetComponent<SkinnedMeshRenderer>();
        _helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
        _chestModelChanger = GetComponentInChildren<ChestModelChanger>();
        _gloveModelChanger = GetComponentInChildren<GloveModelChanger>();
        _pantModelChanger = GetComponentInChildren<PantModelChanger>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MatchingBoneAllArmorMesh();
        AllUnEquipModels();
        StartCoroutine(DefaultEquipAfterOneFrame());
    }

    // Update is called once per frame
    void Update()
    {
        Equip();
    }
    // 임시 장착?
    IEnumerator DefaultEquipAfterOneFrame() // * 시작 시 장비 검사 후 맞게 바꿔주기?
    {
        yield return null;

        _helmetModelChanger.EquipModelByName("NakedHead");
        _chestModelChanger.EquipModelByName("NakedChest");
        _gloveModelChanger.EquipModelByName("NakedHands");
        _pantModelChanger.EquipModelByName("NakedPant");
        _mannequinMesh.enabled = false;
    }
    void Equip()
    {
        if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            _helmetModelChanger.EquipModelByName("Helmet_02");
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            _chestModelChanger.EquipModelByName("Chest_02_Cloak");
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            _gloveModelChanger.EquipModelByName("Glove_02");
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            _pantModelChanger.EquipModelByName("Pant_02");
        }
    }
    //
    void AllUnEquipModels()
    {
        _helmetModelChanger.UnEquipAllModel();
        _chestModelChanger.UnEquipAllModel();
        _gloveModelChanger.UnEquipAllModel();
        _pantModelChanger.UnEquipAllModel();
    }
    void MatchingBoneAllArmorMesh()
    {
        List<SkinnedMeshRenderer> allMeshes = new List<SkinnedMeshRenderer>();
        //_helmetModelChanger._helmetMeshes
        allMeshes.AddRange(_helmetModelChanger.ModelMeshes);
        allMeshes.AddRange(_chestModelChanger.ModelMeshes);
        allMeshes.AddRange(_gloveModelChanger.ModelMeshes);
        allMeshes.AddRange(_pantModelChanger.ModelMeshes);

        foreach (var mesh in allMeshes)
        {
            Debug.Log($"Start Matching Mesh : {mesh.name}");
            MatchingBoneArmorMesh(mesh, _mannequinMesh);
        }
    }
    void MatchingBoneArmorMesh(SkinnedMeshRenderer matchMesh, SkinnedMeshRenderer targetMesh)
    {
        if (matchMesh == null)
            return;
        if (targetMesh == null)
            return;

        Transform[] bones = new Transform[matchMesh.bones.Length];
        for (int boneOrder = 0; boneOrder < matchMesh.bones.Length; boneOrder++)
        {
            Transform targetTransform = targetMesh.bones[0];
            for (int i = 0; i < targetMesh.bones.Length; i++)
            {
                Debug.LogWarning($"find bone : {matchMesh.bones[boneOrder].name}");
                Debug.LogWarning($"maching bone : {targetMesh.bones[i].name}");
                if (matchMesh.bones[boneOrder].name == targetMesh.bones[i].name)
                {
                    targetTransform = targetMesh.bones[i];
                    break;
                }
            }
            bones[boneOrder] = targetTransform;
            //bones[boneOrder] = Array.Find<Transform>(childrens, c => c.name == _chestMesh.bones[boneOrder].name);
        }
        /*
        for (int i = 0; i < _chestMesh.bones.Length; i++)
        {
            for (int j = 0; j < _mannequinMesh.bones.Length; j++)
            {
                if (_chestMesh.bones[i].name == _mannequinMesh.bones[j].name)
                {
                    Debug.Log($"Matching _chestMesh Bone : {_chestMesh.bones[i].name}, _mannequinMesh Bone : {_mannequinMesh.bones[j].name}");
                        
                    //_chestMesh.bones[i] = _mannequinMesh.bones[j];
                    break;
                }
            }
        }
        */
        matchMesh.bones = bones;
        
        //matchMesh.transform
        //_chestMesh.bones = 
        // _chestMesh.

        //_chestMesh.gameObject.SetActive(true);
    }
}
