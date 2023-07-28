
using System;
using ExtensionMethods;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] ObstacleSpawnInfo[] obstaclesToSpawn;
    [SerializeField] float timeToSpawn;
    [SerializeField] float startToSpawnTime = 0f;
    [SerializeField] float distanceFromTrackStart;
    [SerializeField] float distanceToDestroy;
    [SerializeField] float obstaclesSpeed;
    [SerializeField] GameObject obstaclesContainerPrefab;

    Track track;

    float spawnTimer = 0;

    float[] weights;
    Obstacle[] obstacles;


    [Serializable]
    private class ObstacleSpawnInfo
    {
        public Obstacle obstacle;
        public float weight;
    }

    private void Awake()
    {
        track = FindObjectOfType<Track>();
        weights = new float[obstaclesToSpawn.Length];
        obstacles = new Obstacle[obstaclesToSpawn.Length];
        for (int i = 0; i < obstaclesToSpawn.Length; i++)
        {
            weights[i] = obstaclesToSpawn[i].weight;
            obstacles[i] = obstaclesToSpawn[i].obstacle;
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeToSpawn + startToSpawnTime)
        {
            SpawnObstacles();
            spawnTimer = startToSpawnTime;
        }
    }

    private void SpawnObstacles()
    {
        Vector3 vectorAwayFromTrackStart = track.transform.forward * distanceFromTrackStart;
        GameObject obstaclesContainer = Instantiate(obstaclesContainerPrefab, track.transform.position + vectorAwayFromTrackStart, Quaternion.identity, transform);
        for (int laneNumber = 1; laneNumber <= track.GetNumberOfLanes(); laneNumber++)
        {
            Vector3 spawnPoint = track.GetLane(laneNumber).GetCenter() + vectorAwayFromTrackStart;
            Obstacle obstacle = SpawnObstacle();
            obstacle.transform.position = spawnPoint;
            obstacle.transform.SetParent(obstaclesContainer.transform);
        }
        obstaclesContainer.GetComponent<Rigidbody>().velocity = -vectorAwayFromTrackStart.normalized * obstaclesSpeed;
    }

    private Obstacle SpawnObstacle()
    {
        Obstacle obstaclePrefab = GetRandomObstacle();
        Obstacle obstacleInstance = Instantiate<Obstacle>(obstaclePrefab, Vector3.zero, Quaternion.identity, transform);
        return obstacleInstance;
    }

    private Obstacle GetRandomObstacle()
    {
        return ArrayExtensions.GetWeightedRandom<Obstacle>(weights, obstacles);
    }
}
