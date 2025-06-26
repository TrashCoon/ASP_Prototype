using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class FlowerPlacer : MonoBehaviour
{

    public GameObject flowerPrefab;
    public Terrain terrain;
    public int count = 500;
    public float minHeight = 0f;
    public float maxHeight = 9999f;


    public void placeFlowers()
    {
        if (!flowerPrefab || !terrain) return;

        #if UNITY_EDITOR
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(0f, 1f);
            float z = Random.Range(0f, 1f);

            float terrainX = x * terrain.terrainData.size.x;
            float terrainZ = z * terrain.terrainData.size.z;
            float terrainY = terrain.SampleHeight(new Vector3(terrainX, 0, terrainZ)) + terrain.GetPosition().y;

            if (terrainY < minHeight || terrainY > maxHeight) continue;

            Vector3 position = new Vector3(
                terrainX + terrain.GetPosition().x,
                terrainY,
                terrainZ + terrain.GetPosition().z
            );

            GameObject flower = (GameObject)PrefabUtility.InstantiatePrefab(flowerPrefab);
            flower.transform.position = position;
            flower.transform.SetParent(this.transform);
        }
        #endif
    }
}
