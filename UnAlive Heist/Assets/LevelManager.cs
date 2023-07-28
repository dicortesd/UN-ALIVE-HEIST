using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] levels;
    Score score;

    int currentLevelNumber;
    public static Level currentLevel;
    Level nextLevel;

    private void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    private void Start()
    {
        if (levels.Length < 1)
        {
            Debug.LogError("No levels set up");
        }
        else
        {
            currentLevelNumber = 0;
            currentLevel = levels[0];
        }
    }

    private void Update()
    {
        float currentScore = score.GetScore();
        if (nextLevel != null && currentScore >= nextLevel.requiredScore)
        {
            AdvanceLevel();
        }
    }

    private void AdvanceLevel()
    {
        currentLevelNumber++;
        currentLevel = levels[currentLevelNumber];
        nextLevel = levels[currentLevelNumber + 1];
    }


    [Serializable]
    public class Level
    {
        public float speed;
        public int requiredScore;
    }
}
