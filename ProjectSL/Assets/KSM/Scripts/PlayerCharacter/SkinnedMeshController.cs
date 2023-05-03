using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SkinnedMeshController : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer _mannequinMesh;

    [SerializeField]
    HelmetModelChanger _helmetModelChanger;
    [SerializeField]
    ChestModelChanger _chestModelChanger;
    [SerializeField]
    GloveModelChanger _gloveModelChanger;
    [SerializeField]
    PantModelChanger _pantModelChanger;

    private void Awake()
    {
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

        _helmetModelChanger.EquipHelmetModelByName("NakedHead");
        _chestModelChanger.EquipChestModelByName("NakedChest");
        _gloveModelChanger.EquipGloveModelByName("NakedHands");
        _pantModelChanger.EquipPantModelByName("NakedPant");
        _mannequinMesh.enabled = false;
    }
    void Equip()
    {
        if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            _helmetModelChanger.EquipHelmetModelByName("Helmet_01");
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            _chestModelChanger.EquipChestModelByName("Chest_01");
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            _gloveModelChanger.EquipGloveModelByName("Glove_01");
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            _pantModelChanger.EquipPantModelByName("Pant_01");
        }
    }
    //
    void AllUnEquipModels()
    {
        _helmetModelChanger.UnEquipAllHelmetModels();
        _chestModelChanger.UnEquipAllChestModels();
        _gloveModelChanger.UnEquipAllGloveModels();
        _pantModelChanger.UnEquipAllPantModels();
    }
    void MatchingBoneAllArmorMesh()
    {
        List<SkinnedMeshRenderer> allMeshes = new List<SkinnedMeshRenderer>();
        //_helmetModelChanger._helmetMeshes
        allMeshes.AddRange(_helmetModelChanger._helmetMeshes);
        allMeshes.AddRange(_chestModelChanger._chestMeshes);
        allMeshes.AddRange(_gloveModelChanger._gloveMeshes);
        allMeshes.AddRange(_pantModelChanger._pantMeshes);

        foreach (var mesh in allMeshes)
        {
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
