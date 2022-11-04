using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
#endif

public class DDRLevel : ScriptableObject
{
    public string LevelName;
    public AudioClip Song = null;
    public Sprite LevelIcon;
    public DDRTrack[] Tracks;
    [Header("Points per score")]
    public int PerfectScorePoints;
    public int GreatScorePoints;
    public int GoodScorePoints;
    public int OkScorePoints;
    public int EarlyScorePoints;
    public int MissScorePoints;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DDRLevel))]
public class DDRLevelInspector : Editor
{
    private bool defaultSettings;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Open in DDR level editor"))
        {
            DDREditorWindow.ShowWindow((DDRLevel)target);
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear all nodes"))
        {
            if (EditorUtility.DisplayDialog("Delete all nodes", "This action will remove all of the nodes in the level object.", "Delete", "Cancel"))
            {
                ((DDRLevel)target).Tracks = new DDRTrack[4];
            }
        }
    }
}
#endif