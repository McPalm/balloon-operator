using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Unlocks
{
    static public int HighestCleared
    {
        private set =>PlayerPrefs.SetInt("highestCleared", value);
        get => PlayerPrefs.GetInt("highestCleared");
    }

    static public void ClearStage(int stage)
    {
        if (stage > HighestCleared)
            HighestCleared = stage;
    }
}
