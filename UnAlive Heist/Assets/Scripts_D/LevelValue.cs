
using System;
using UnityEngine;

[Serializable]
public class LevelValue<T>
{
    [SerializeField] T[] valuesByLevel;

    public T GetValue()
    {
        return valuesByLevel[LevelManager.GetCurrentLevelNumber() - 1];
    }
}
