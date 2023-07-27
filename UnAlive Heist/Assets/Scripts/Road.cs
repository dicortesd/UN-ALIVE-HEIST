using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    //Variable publica por si se requiere acceder desde el generador de niveles
    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Iniciar en el origen 
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Mover hacia atras en cada frame segun la velocidad definida
        transform.Translate(Vector3.back * speed * Time.deltaTime);


        //Rutina para destruirse si ya llega muy atras en el mapa
        if (transform.position.z <= -200)
        {
            Destroy(this.gameObject);
        }

    }
}
