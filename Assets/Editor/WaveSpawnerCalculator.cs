using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnManager))]
public class WaveSpawnerCalculator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnManager waveSpawner = (SpawnManager)target;
        waveSpawner.UpdateCalculatorUI();

        EditorGUILayout.LabelField("Spawn Tier: " + waveSpawner.UISpawnTier());
        EditorGUILayout.LabelField("Time Scale: " + waveSpawner.UITimeScale());
        EditorGUILayout.LabelField("Spawn Budget: " + waveSpawner.UISpawnBudget());
    }
}