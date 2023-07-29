
using System;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] LevelValue<ObstaclesSpawnInfo> obstaclesToSpawn;
    [SerializeField] LevelValue<float> timeToSpawn;
    [SerializeField] float startToSpawnTime = 0f;
    [SerializeField] float distanceFromTrackStart;
    [SerializeField] float distanceToDestroy;
    [SerializeField] GameObject obstaclesContainerPrefab;


    Track track;

    float spawnTimer = 0;
    int numberOfIndestructibles;
    Queue<GameObject> containersQueue;

    [Serializable]
    private class ObstaclesSpawnInfo
    {
        public Obstacle[] obstacles;
        public float[] weights;
    }

    private void Awake()
    {
        containersQueue = new Queue<GameObject>();
        track = FindObjectOfType<Track>();
    }

    private void Start()
    {
        spawnTimer = timeToSpawn.GetValue();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeToSpawn.GetValue() + startToSpawnTime)
        {
            SpawnObstacles();
            spawnTimer = startToSpawnTime;
        }

        if (containersQueue.Count > 0)
        {
            GameObject lastContainer = containersQueue.Peek();
            if (lastContainer != null && IsContainerTooFar(lastContainer))
            {
                containersQueue.Dequeue();
                Destroy(lastContainer);
            }
        }
    }

    private void SpawnObstacles()
    {
        Vector3 vectorAwayFromTrackStart = track.transform.forward * distanceFromTrackStart;
        GameObject obstaclesContainer = Instantiate(obstaclesContainerPrefab, track.transform.position + vectorAwayFromTrackStart, Quaternion.identity, transform);
        containersQueue.Enqueue(obstaclesContainer);

        numberOfIndestructibles = 0;
        for (int laneNumber = 1; laneNumber <= track.GetNumberOfLanes(); laneNumber++)
        {
            Vector3 spawnPoint = track.GetLane(laneNumber).GetCenter() + vectorAwayFromTrackStart;
            Obstacle obstacle = SpawnObstacle();

            if (obstacle.CompareTag("Indestructible"))
            {
                numberOfIndestructibles++;
            }

            obstacle.transform.position = spawnPoint;
            obstacle.transform.SetParent(obstaclesContainer.transform);
        }
        print(numberOfIndestructibles);

        //Prevent that all lanes have indestructible obstacles
        if (LimitOfIndestructiblesReached())
        {
            int randomIndex = Random.Range(0, obstaclesContainer.transform.childCount - 1);
            obstaclesContainer.transform.GetChild(randomIndex).gameObject.SetActive(false);
        }

        obstaclesContainer.GetComponent<Rigidbody>().velocity = -vectorAwayFromTrackStart.normalized * LevelManager.GetCurrentLevel().speed;
    }

    private bool LimitOfIndestructiblesReached()
    {
        return numberOfIndestructibles > track.GetNumberOfLanes() - 1;
    }

    private Obstacle SpawnObstacle()
    {
        Obstacle obstaclePrefab = GetRandomObstacle();
        Obstacle obstacleInstance = Instantiate<Obstacle>(obstaclePrefab, Vector3.zero, Quaternion.identity, transform);
        return obstacleInstance;
    }

    private Obstacle GetRandomObstacle()
    {
        ObstaclesSpawnInfo currentObstaclesToSpawn = obstaclesToSpawn.GetValue();
        return ArrayExtensions.GetWeightedRandom<Obstacle>(currentObstaclesToSpawn.weights, currentObstaclesToSpawn.obstacles);
    }

    private bool IsContainerTooFar(GameObject container)
    {
        Vector3 fromTrackToContainer = container.transform.position - track.transform.position;
        return Vector3.Dot(fromTrackToContainer, track.transform.forward) < 0 && (fromTrackToContainer.sqrMagnitude > distanceToDestroy * distanceToDestroy);
    }
}
