using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    //Variable publica por si se requiere acceder desde el generador de niveles
    //public float speed = 20.0f;
    LevelGenerator levelGenerator;
    [SerializeField] public Transform roadEnd;
    [SerializeField] GameObject obstaclePrefab;

    private float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Iniciar en el origen 
        //transform.position = new Vector3(0, 0, 100);
        levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();
        //speed = levelGenerator.roadSpeed;
        //SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        //Mover hacia atras en cada frame segun la velocidad definida
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(4, 8);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }
}
