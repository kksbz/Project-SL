using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SkinnedMeshController : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer _mannequinMesh;
    [SerializeField]
    SkinnedMeshRenderer _chestMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            tempEquip();
        }
    }
    void tempEquip()
    {
        if (_chestMesh != null)
        {
            //_chestMesh.rootBone = _mannequinMesh.rootBone;
            Transform[] childrens = transform.GetComponentsInChildren<Transform>(true);
            Transform[] bones = new Transform[_chestMesh.bones.Length];
            for(int boneOrder = 0; boneOrder < _chestMesh.bones.Length; boneOrder++)
            {
                Transform targetTransform = _mannequinMesh.bones[0];
                for(int i = 0; i < _mannequinMesh.bones.Length; i++)
                {
                    if (_chestMesh.bones[boneOrder].name == _mannequinMesh.bones[i].name)
                    {
                        targetTransform = _mannequinMesh.bones[i];
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
            _chestMesh.bones = bones;
            //_chestMesh.bones = 
            // _chestMesh.
            
            //_chestMesh.gameObject.SetActive(true);
        }
    }
}
