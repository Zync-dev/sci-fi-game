using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapGeneration : MonoBehaviour
{
    [Header("MAP PREFABS")]
    public GameObject startSection;
    public GameObject endSection;

    public GameObject[] sections;

    [Header("Other Settings")]
    public float segmentsToSpawn = 5f;

    private const float SEGMENT_LENGTH = 46f;
    private const float SPAWN_THRESHOLD = 50f;

    private float playerProgression = 0f;
    private float mapCurrentDistance = 0f;

    private GameObject player;

    GameObject NavMeshObj;

    private void Start()
    {
        player = GameObject.Find("PlayerModel").gameObject;
        NavMeshObj = GameObject.Find("NavMesh");
        SpawnStartSegment();
        NavMeshObj.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void Update()
    {
        playerProgression = player.transform.position.x;
        if (mapCurrentDistance - playerProgression < SPAWN_THRESHOLD) { SpawnNextSegment();  }
    }

    private void SpawnNextSegment()
    {
        float chosenSegment = Random.Range(0f, sections.Length);
        GameObject spawnedSegment = Instantiate(sections[(int)chosenSegment]);
        spawnedSegment.transform.position = new Vector3(mapCurrentDistance, 0f, 0f);
        mapCurrentDistance += SEGMENT_LENGTH;

        NavMeshObj.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void SpawnStartSegment()
    {
        GameObject spawnedSegment = Instantiate(startSection);
        spawnedSegment.transform.position = new Vector3(mapCurrentDistance, 0f, 0f);
        NavMeshObj.GetComponent<NavMeshSurface>().BuildNavMesh();
        mapCurrentDistance += SEGMENT_LENGTH;
    }

    private void SpawnEndSegment()
    {
        GameObject spawnedSegment = Instantiate(endSection);
        spawnedSegment.transform.position = new Vector3(mapCurrentDistance, 0f, 0f);
        NavMeshObj.GetComponent<NavMeshSurface>().BuildNavMesh();
        mapCurrentDistance += SEGMENT_LENGTH;
    }
}