using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    public Transform spawnRoot;          // parent of spawn points
    public GameObject[] npcPrefabs;       // 10 characters
    [Range(0f, 1f)] public float density = 0.6f;

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        List<Transform> points = new List<Transform>();

        foreach (Transform t in spawnRoot)
            points.Add(t);

        Shuffle(points);

        int spawnCount = Mathf.RoundToInt(points.Count * density);

        for (int i = 0; i < spawnCount; i++)
        {
            Transform point = points[i];
            GameObject npc = Instantiate(
                npcPrefabs[Random.Range(0, npcPrefabs.Length)],
                point.position,
                point.rotation,
                transform
            );

            SnapToGround(npc.transform);
            AddVariation(npc.transform);
        }
    }

    void SnapToGround(Transform npc)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(npc.position, out hit, 2f, NavMesh.AllAreas))
        {
            npc.position = hit.position;
        }
    }


    void AddVariation(Transform npc)
    {
        npc.Rotate(0f, Random.Range(-25f, 25f), 0f);
        float scale = Random.Range(0.95f, 1.05f);
        npc.localScale *= scale;
    }

    void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }
}
