using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(FlowerPlacer))]

public class FlowerSpawnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FlowerPlacer spawner = (FlowerPlacer)target;

        if (GUILayout.Button("Generate Flowers"))
        {
            spawner.placeFlowers();
        }
    }



}
