using System.Collections.Generic;

#if UNITY_EDITOR
public enum DDRWindowState
{
    Menu, Editor
}
#endif

public enum DDRRating
{
    Perfect, Great, Good, Ok, Miss, Early
}

public enum DDRFinishStatus
{
    Complete, Failed
}

[System.Serializable]
public class DDRTrack
{
    public List<float> Nodes;
}
[System.Serializable]
public class GameState
{
    public int Score;
    public float Health;
    public int LongestCombo;
    public int Combo;
    public Dictionary<DDRRating, int> Ratings;
    public DDRFinishStatus FinishStatus;

    public GameState()
    {
        Health = 100;
        Ratings = new Dictionary<DDRRating, int>();
        Ratings.Add(DDRRating.Early, 0);
        Ratings.Add(DDRRating.Good, 0);
        Ratings.Add(DDRRating.Great, 0);
        Ratings.Add(DDRRating.Miss, 0);
        Ratings.Add(DDRRating.Ok, 0);
        Ratings.Add(DDRRating.Perfect, 0);
    }
}
[System.Serializable]
public struct DDRScoreData
{
    public DDRRating Rating;
    public float Points;
}