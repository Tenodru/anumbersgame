using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnReferences))]
public class SpawnTierChanceCalculator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnReferences spawnRef = (SpawnReferences)target;

        spawnRef.UICalculateSpawnChances();

        EditorGUILayout.LabelField("Tier 1 Chance: " + spawnRef.UIGetTier1Chance());
        EditorGUILayout.LabelField("Tier 2 Chance: " + spawnRef.UIGetTier2Chance());
        EditorGUILayout.LabelField("Tier 3 Chance: " + spawnRef.UIGetTier3Chance());
        EditorGUILayout.LabelField("Tier 4 Chance: " + spawnRef.UIGetTier4Chance());
    }
}
