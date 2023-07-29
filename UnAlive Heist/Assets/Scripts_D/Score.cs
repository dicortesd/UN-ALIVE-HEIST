using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoretextMesh;
    [SerializeField] TextMeshProUGUI recordtextMesh;
    private int record;
    private int score;
    private float timer;
    private bool count;

    // Start is called before the first frame update
    void Start()
    {
        scoretextMesh = GetComponent<TextMeshProUGUI>();
        record = PlayerPrefs.GetInt("Record");
        score = 0;
        count = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (count){
            scoretextMesh.text = $"score: {score.ToString()}";
            recordtextMesh.text = $"record: {record.ToString()}";
            timer = timer + Time.deltaTime;

        //Cada 0.5 segundos incrementar el score
            if (timer >= 0.5f)
            {
                score++;
                timer = 0.0f;

                //Verificar si se supero el record y actualizarlo en caso de ser necesario
                if (score > record) {
                    record = score;
                    PlayerPrefs.SetInt("Record", record);

                }
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
