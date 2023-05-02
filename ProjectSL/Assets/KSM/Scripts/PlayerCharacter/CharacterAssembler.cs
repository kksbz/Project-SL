using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAssembler : MonoBehaviour
{
    void Start()
    {

        Vector3 originalPosition = this.transform.position;
        this.transform.position = Vector3.zero;

        SkinnedMeshRenderer[] smRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        int numSubs = 0;

        List<Transform> bones = new List<Transform>();

        List<BoneWeight> boneWeights = new List<BoneWeight>();
        List<CombineInstance> combineInstances = new List<CombineInstance>();


        foreach (SkinnedMeshRenderer smr in smRenderers)
            numSubs += smr.sharedMesh.subMeshCount;

        //int boneOffset = 0;

        SkinnedMeshRenderer baseMR = smRenderers[0];

        for (int s = 0; s < smRenderers.Length; s++)
        {
            SkinnedMeshRenderer smr = smRenderers[s];

            BoneWeight[] meshBoneweight = smr.sharedMesh.boneWeights;

            // May want to modify this if the renderer shares bones as unnecessary bones will get added.
            foreach (BoneWeight bw in meshBoneweight)
            {

                BoneWeight bWeight = bw;

                //bWeight.boneIndex0 += boneOffset;
                //bWeight.boneIndex1 += boneOffset;
                //bWeight.boneIndex2 += boneOffset;
                //bWeight.boneIndex3 += boneOffset;                

                boneWeights.Add(bWeight);
            }
            //boneOffset += smr.bones.Length;


            CombineInstance ci = new CombineInstance();
            ci.mesh = smr.sharedMesh;

            ci.transform = smr.transform.localToWorldMatrix;
            combineInstances.Add(ci);

            Object.Destroy(smr.gameObject);
        }


        Transform[] meshBones = baseMR.bones;
        foreach (Transform bone in meshBones)
            bones.Add(bone);


        List<Matrix4x4> bindposes = new List<Matrix4x4>();

        for (int b = 0; b < bones.Count; b++)
        {
            bindposes.Add(bones[b].worldToLocalMatrix * transform.worldToLocalMatrix);
        }

        SkinnedMeshRenderer r = gameObject.AddComponent<SkinnedMeshRenderer>();
        r.sharedMesh = new Mesh();
        r.sharedMesh.CombineMeshes(combineInstances.ToArray(), true, true);


        Material combinedMat = new Material(Shader.Find("Diffuse"));

        r.sharedMaterial = combinedMat;

        r.rootBone = bones[0];
        r.bones = bones.ToArray();
        r.sharedMesh.boneWeights = boneWeights.ToArray();
        r.sharedMesh.bindposes = bindposes.ToArray();

        r.sharedMesh.RecalculateBounds();

        this.transform.position = originalPosition;
    }
}
