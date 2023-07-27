using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Acceder a los prefabs que conforman el camino
    [SerializeField] private GameObject road;
    private float timer ;
    public float speed = 20f;
    private Vector3 nextRoadSpawn;

    public void GenerateRoad()
    {
        GameObject temp = Instantiate(road, nextRoadSpawn, Quaternion.identity);
        nextRoadSpawn = temp.transform.GetChild(3).transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

       GenerateRoad();

    }

    // Update is called once per frame
    void Update()
    {

        //Contar el tiempo que esta pasando
        timer = timer + Time.deltaTime;

        //Si pasaron 0.48 segundos instanciar otro camino
        if ((timer % 5) == 0)
        {
            speed++;
        }
    }
}
