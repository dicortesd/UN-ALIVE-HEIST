
using System;
using ExtensionMethods;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] ObstacleSpawnInfo[] obstaclesToSpawn;
    [SerializeField] float timeToSpawn;
    [SerializeField] float startToSpawnTime = 0f;
    [SerializeField] float distanceFromTrackStart;

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
        for (int laneNumber = 1; laneNumber <= track.GetNumberOfLanes(); laneNumber++)
        {
            Vector3 spawnPoint = track.GetLane(laneNumber).GetCenter() + track.transform.forward * distanceFromTrackStart;
            Obstacle obstacle = SpawnObstacle();
            obstacle.transform.position = spawnPoint;
        }
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
