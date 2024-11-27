using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] objectsToSpawn;
    public int objectCount = 10;
    public Vector3 spawnAreaSize = new Vector3(50f, 10f, 50f);

    [Header("Reset Settings")]
    public float positionResetInterval = 10f;
    public float maxDistanceFromPlayer = 30f;

    public Transform player;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        SpawnObjects();

        StartCoroutine(ResetObjectPositionsPeriodically());
    }

    void Update()
    {
        CheckAndResetObjectPositions();
        EnsureObjectCount();
        CheckAndRelocateFarObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            GameObject newObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
            spawnedObjects.Add(newObject);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
        return new Vector3(randomX, randomY, randomZ) + transform.position;
    }

    private Vector3 GetRandomPositionNearPlayer()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = spawnAreaSize.y;
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
        return new Vector3(randomX, randomY, randomZ) + player.position;
    }

    private IEnumerator ResetObjectPositionsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(positionResetInterval);
            ResetObjectPositions();
        }
    }

    private void ResetObjectPositions()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                obj.transform.position = GetRandomPosition();
            }
        }
    }

    private void CheckAndResetObjectPositions()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null && obj.transform.position.y < -2f)
            {
                obj.transform.position = GetRandomPosition();
            }
        }
    }

    private void CheckAndRelocateFarObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null && Vector3.Distance(player.position, obj.transform.position) > maxDistanceFromPlayer)
            {
                obj.transform.position = GetRandomPositionNearPlayer();
            }
        }
    }

    private void EnsureObjectCount()
    {
        int currentCount = spawnedObjects.Count(obj => obj != null);
        int missingCount = objectCount - currentCount;

        for (int i = 0; i < missingCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            GameObject newObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
            spawnedObjects.Add(newObject);
        }
    }
}
