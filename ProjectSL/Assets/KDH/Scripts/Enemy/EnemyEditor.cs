using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyBase), true)]
[CanEditMultipleObjects]
public class EnemyEditor : Editor
{
    public void OnSceneGUI()
    {
        EnemyBase enemy = (EnemyBase)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.Status.detectionRange);
        Handles.color = Color.red;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.Status.attackRange);
    }
}

