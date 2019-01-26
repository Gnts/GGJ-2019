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
    private float timePassed = 0f;
    public void Awake()
    {
        foreach (Transform child in SpawnPointRoot)
            spawnPoints.Add(child);
    }

    public void Start()
    {
        foreach (var t in spawnPoints)
        {
            SpawnPickable(t.position);
        }
    }

    public void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > spawnTimer)
        {
            timePassed = 0f;
            Transform t = spawnPoints[Random.Range(0, prefabs.Length)];
            SpawnPickable(t.position);
        }
    }

    void SpawnPickable(Vector3 position)
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], position + Vector3.up * Random.Range(0, 4), Random.rotation);
    }
}
