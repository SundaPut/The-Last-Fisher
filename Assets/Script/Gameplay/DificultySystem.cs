using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishDifficulty { Easy, Normal, Hard, Legendary }

[System.Serializable]
public class DifficultySettings
{
    public FishDifficulty difficulty;
    public int extraQTE;
    public float hungerGain;
    public float hungerPenalty;
    public Color uiColor;
    [Range(0f, 1f)]
    public float successRate = 0.7f;
    [Header("UI Setting")]
    public int sliderValue;
}

