using UnityEngine;

public static class SaveProgress
{
    public static int LevelNumber
    {
        get { return PlayerPrefs.GetInt("LevelCompleted"); }
        set { PlayerPrefs.SetInt("LevelCompleted", value); }
    }
}
