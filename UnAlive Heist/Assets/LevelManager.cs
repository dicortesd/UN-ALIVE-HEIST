using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] levels;
    Score score;

    static int currentLevelNumber;
    static int numberOfLevels;
    static Level currentLevel;
    Level nextLevel;

    private void Awake()
    {
        if (levels.Length < 1)
        {
            Debug.LogError("No levels set up");
        }
        else
        {
            currentLevelNumber = 1;
            currentLevel = levels[currentLevelNumber - 1];
            if (levels.Length > 1) nextLevel = levels[currentLevelNumber];
            numberOfLevels = levels.Length;
        }

        score = FindObjectOfType<Score>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        float currentScore = score.GetScore();
        if (currentLevelNumber < levels.Length && currentScore >= nextLevel.requiredScore)
        {
            AdvanceLevel();
        }
    }

    public static int GetCurrentLevelNumber()
    {
        return currentLevelNumber;
    }

    public static Level GetCurrentLevel()
    {
        return currentLevel;
    }

    public static int GetNumberOfLevels()
    {
        return numberOfLevels;
    }

    private void AdvanceLevel()
    {
        currentLevelNumber++;
        currentLevel = levels[currentLevelNumber - 1];
        if (currentLevelNumber < levels.Length) nextLevel = levels[currentLevelNumber];
    }


    [Serializable]
    public class Level
    {
        public float speed;
        public int requiredScore;
    }
}

