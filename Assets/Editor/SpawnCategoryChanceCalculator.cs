using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnReferences))]
public class SpawnCategoryChanceCalculator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnReferences spawnRef = (SpawnReferences)target;

        /*
        foreach (SpawnTier tier in spawnRef.spawnTiers)
        {
            float percentage = 1.0f;
            float overflow = 0.0f;
            foreach (SpawnCategory cat in tier.categories)
            {
                EditorGUILayout.LabelField("Remaining Percentage: " + 1);
                if (cat.spawnChance > percentage)
                {
                    overflow = cat.spawnChance - percentage;
                    foreach (SpawnCategory catOther in tier.categories)
                    {

                    }
                }
            }
        }*/
    }
}
