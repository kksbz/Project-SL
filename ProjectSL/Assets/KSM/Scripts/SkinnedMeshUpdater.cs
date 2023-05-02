using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshUpdater : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer original;

    #region UNITYC_CALLBACK

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            UpdateMeshRenderer(original);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        var meshrenderer = original;//GetComponentInChildren<SkinnedMeshRenderer>();
        Vector3 before = meshrenderer.bones[0].position;
        for (int i = 0; i < meshrenderer.bones.Length; i++)
        {
            Gizmos.DrawLine(meshrenderer.bones[i].position, before);
            UnityEditor.Handles.Label(meshrenderer.bones[i].transform.position, i.ToString());
            UnityEditor.Handles.Label(meshrenderer.bones[i].transform.position, meshrenderer.bones[i].name);
            before = meshrenderer.bones[i].position;
        }
    }
#endif

    #endregion

    public void UpdateMeshRenderer(SkinnedMeshRenderer newMeshRenderer)
    {
        // update mesh
        var meshrenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshrenderer.sharedMesh = newMeshRenderer.sharedMesh;

        Transform[] childrens = transform.GetComponentsInChildren<Transform>(true);

        // sort bones.
        Transform[] bones = new Transform[newMeshRenderer.bones.Length];
        for (int boneOrder = 0; boneOrder < newMeshRenderer.bones.Length; boneOrder++)
        {
            bones[boneOrder] = Array.Find<Transform>(childrens, c => c.name == newMeshRenderer.bones[boneOrder].name);
        }
        meshrenderer.bones = bones;
    }
}
