using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryGenerator : MonoBehaviour
{
    public GameObject[] sceneryPrefabs;
    private float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        int prefabIndex = Random.Range(0, sceneryPrefabs.Length);
        Instantiate(sceneryPrefabs[prefabIndex], transform.position, Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.back * speed * Time.deltaTime);
        //Instantiate(sceneryPrefabs[Random.Range(0, sceneryPrefabs.Length)], transform.position, Quaternion.identity);
    }
}
