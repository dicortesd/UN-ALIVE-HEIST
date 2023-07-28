using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float score;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = score.ToString();
        timer = timer + Time.deltaTime;

        //Cada 0.5 segundos incrementar el score
        if (timer >= 0.5f)
        {
            score++;
            timer = 0.0f;
        }
    }

    public float GetScore()
    {
        return score;
    }
}
