using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Acceder a los prefabs que conforman el camino
    [SerializeField] Road road;
    [SerializeField] float lastRoadMaxDistance;

    [SerializeField] int numberOfPregeneratedRoads;
    private Transform nextRoadSpawn;

    Queue<Road> roadsQueue;

    // Start is called before the first frame update
    void Start()
    {
        roadsQueue = new Queue<Road>();
        nextRoadSpawn = transform;
        PreGenerateRoad();
    }

    private void PreGenerateRoad()
    {
        for (int i = 0; i < numberOfPregeneratedRoads; i++)
        {
            GenerateRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roadsQueue.Count > 0 && DistanceToLastRoad() >= lastRoadMaxDistance)
        {
            GenerateRoad();
            DestroyLastRoad();
        }
    }

    private float DistanceToLastRoad()
    {
        Road oldestRoad = roadsQueue.Peek();
        return Vector3.Distance(oldestRoad.transform.position, transform.position);
    }

    private void DestroyLastRoad()
    {
        Road oldRoad = roadsQueue.Dequeue();
        Destroy(oldRoad.gameObject);
    }

    private Road GenerateRoad()
    {
        Road newRoad = Instantiate<Road>(road, nextRoadSpawn.position, Quaternion.identity, transform);
        newRoad.SetSpeed(LevelManager.GetCurrentLevel().speed);
        nextRoadSpawn = newRoad.roadEnd;
        roadsQueue.Enqueue(newRoad);

        return road;
    }
}
