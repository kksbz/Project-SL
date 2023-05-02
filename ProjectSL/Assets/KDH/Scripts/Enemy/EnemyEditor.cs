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

        Vector3 viewAngleA = enemy.DirFromAngle(-enemy.ResearchStatus.viewAngle / 2, false);
        Vector3 viewAngleB = enemy.DirFromAngle(enemy.ResearchStatus.viewAngle / 2, false);

        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleA * enemy.ResearchStatus.viewRadius);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleB * enemy.ResearchStatus.viewRadius);

        Handles.color = Color.red;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.Status.attackRange);


    }
}

