using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Tooltip("Root of spawn points. It will use it to populate.")]
    public Transform SpawnPointRoot;
    [HideInInspector]
    public List<Transform> spawnPoints = new List<Transform>();
    [Tooltip("Assign pickables.")]
    public GameObject[] prefabs;
    public float spawnTimer = 5;

    public void Awake()
    {
        foreach(Transform child in SpawnPointRoot)
            spawnPoints.Add(child);
    }

    public void Start()
    {
        foreach(var t in spawnPoints)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], t.position, Quaternion.Euler(-90, 0, 0));
        }
    }

}
