using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Acceder a los prefabs que conforman el camino
    [SerializeField] private GameObject road;
    private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(road);
    }

    // Update is called once per frame
    void Update()
    {
        //Contar el tiempo que esta pasando
        timer = timer + Time.deltaTime;

        //Si pasaron 0.48 segundos instanciar otro camino
        if (timer >= 0.48f)
        {
            Instantiate(road);
            timer = 0.0f;
        }


    }
}
