using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Acceder a los prefabs que conforman el camino
    [SerializeField] Road road;
    [SerializeField] float timeToSpawnRoad;
    [SerializeField] float roadSpeed = 20f;
    [SerializeField] float lastRoadMaxDistance;

    [SerializeField] int numberOfPregeneratedRoads;
    private float timer;
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
        //Contar el tiempo que esta pasando
        timer = timer + Time.deltaTime;
        //Si pasaron 0.48 segundos instanciar otro camino
        if (timer >= timeToSpawnRoad || DistanceToLastRoad() >= lastRoadMaxDistance)
        {
            GenerateRoad();
            DestroyOldRoad();
            timer = 0;
        }
    }

    private float DistanceToLastRoad()
    {
        Road oldestRoad = roadsQueue.Peek();
        return Vector3.Distance(oldestRoad.transform.position, transform.position);
    }

    private void DestroyOldRoad()
    {
        Road oldRoad = roadsQueue.Dequeue();
        Destroy(oldRoad.gameObject);
    }

    private Road GenerateRoad()
    {
        Road newRoad = Instantiate<Road>(road, nextRoadSpawn.position, Quaternion.identity, transform);
        newRoad.SetSpeed(roadSpeed);
        nextRoadSpawn = newRoad.roadEnd;
        roadsQueue.Enqueue(newRoad);

        return road;
    }
}
