using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float score;
    private float timer;
    private bool count;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        score = 0;
        count = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (count){
            textMesh.text = score.ToString();
            timer = timer + Time.deltaTime;

        //Cada 0.5 segundos incrementar el score
            if (timer >= 0.5f)
            {
                score++;
                timer = 0.0f;
            }
        }
    }

    public float GetScore()
    {
        return score;
    }

    //Detiene el contador
    public void stop(){
        count = false;
    }
}
