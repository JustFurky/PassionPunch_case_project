using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] private List<Transform> SpawnPoints = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }
    public Transform GetRandomSpawnPoint()
    {
        return SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform;
    }
}
