using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    private TextMeshProUGUI levelTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        levelTextMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int levelNumber = LevelManager.GetCurrentLevelNumber();
        if (levelNumber != LevelManager.GetNumberOfLevels())
        {
            levelTextMesh.text = "Level:" + levelNumber;
        }
        else
        {
            levelTextMesh.text = "Level: MAX";
        }
    }
}
