#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
public class DDREditorWindow : EditorWindow
{
    public DDRLevel OpenedLevel;
    public DDRWindowState WindowState;
    [MenuItem("Window/Yandere Simulator/DDR Editor")]
    public static void ShowWindow()
    {
        DDREditorWindow window = (DDREditorWindow)GetWindow(typeof(DDREditorWindow));
        window.WindowState = DDRWindowState.Menu;
        window.titleContent = new GUIContent("DDR Editor");
        window.minSize = new Vector2(800, 340);
        window.maxSize = new Vector2(1920, 340);
    }

    public static void ShowWindow(DDRLevel levelToLoad)
    {
        DDREditorWindow window = (DDREditorWindow)GetWindow(typeof(DDREditorWindow));
        window.OpenedLevel = levelToLoad;
        window.WindowState = DDRWindowState.Editor;
        window.titleContent = new GUIContent("DDR Editor");
        window.minSize = new Vector2(800, 340);
        window.maxSize = new Vector2(1920, 340);
    }

    [ExecuteInEditMode]
    void Update()
    {
        Repaint();
    }

    void OnGUI()
    {
        switch (WindowState)
        {
            case DDRWindowState.Menu:
                drawMenu();
                break;
            case DDRWindowState.Editor:
                drawEditor();
                break;
        }
    }

    private void OnDestroy()
    {
        stopAllClips();
    }

    #region Editor
    private bool record;

    private bool preview;
    private double startTime;
    private float zoom = 1;

    private Vector2 selectionStart;
    private Rect selectionRect;
    private bool selecting;

    private void drawEditor()
    {
        Color defBackground = GUI.backgroundColor;
        minSize = new Vector2(800, 340);
        maxSize = new Vector2(1920, 340);

        #region Name field
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level name", EditorStyles.boldLabel);
        string levelName = null;
        levelName = EditorGUILayout.TextField(OpenedLevel.LevelName);
        if (levelName != null && OpenedLevel.LevelName != levelName)
        {
            OpenedLevel.LevelName = levelName;
            Undo.RecordObject(OpenedLevel, "Level name change");
            EditorUtility.SetDirty(OpenedLevel);
        }
        GUILayout.EndHorizontal();
        #endregion

        #region Icon field
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level cover", EditorStyles.boldLabel);
        Sprite levelIcon = null;
        levelIcon = (Sprite)EditorGUILayout.ObjectField(OpenedLevel.LevelIcon, typeof(Sprite), true);
        if (levelIcon != null && OpenedLevel.LevelIcon != levelIcon)
        {
            OpenedLevel.LevelIcon = levelIcon;
            Undo.RecordObject(OpenedLevel, "Level icon change");
            EditorUtility.SetDirty(OpenedLevel);
        }
        GUILayout.EndHorizontal();
        #endregion

        #region Track field
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Background track", EditorStyles.boldLabel);
        AudioClip selectedClip = null;
        selectedClip = (AudioClip) EditorGUILayout.ObjectField(OpenedLevel.Song, typeof(AudioClip), true);
        if (selectedClip!=null && OpenedLevel.Song != selectedClip)
        {
            OpenedLevel.Song = selectedClip;
            Undo.RecordObject(OpenedLevel, "Level clip change");
            EditorUtility.SetDirty(OpenedLevel);
        }
        GUILayout.EndHorizontal();
        #endregion

        drawLines(200, 55);
        updateLines();
        GUILayout.BeginHorizontal();
        GUILayout.Space(22);
        GUILayout.Label(toTimeString(playbackTime), EditorStyles.boldLabel);

        zoom = EditorGUILayout.Slider(zoom, 0.1f, 1f);
        scale = 250 * zoom;

        GUILayout.EndHorizontal();

        if (record&&preview) recordKeypresses();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("▶"))
        {
            stopAllClips();
            playClip(OpenedLevel.Song, playbackTime);
        }
        if (GUILayout.Button("▐ ▌"))
        {
            stopAllClips();
        }

        GUI.backgroundColor = record ? Color.red : defBackground;
        if (GUILayout.Button("Recording is " + (record ? "enabled" : "disabled")))
        {
            record = !record;
        }
        GUI.backgroundColor = defBackground;

        GUILayout.EndHorizontal();

        string helpMessage = "Left click on one of the four lines in order to add new node. Use scroll to navigate the timeline. Right click on a node to delete it. Use the slider to zoom in or out the timeline." + Environment.NewLine;
        if (record)
        {
            helpMessage += "Recording is enabled. Your performance will be recorded as the audio plays, use WASD keys.";
        }
        EditorGUILayout.HelpBox(helpMessage, MessageType.Info);
    }

    #region Track rendering
    int whiteLinePosition = 50;
    int hoverLine = -1;
    string[] symbols = new string[] { "◀", "▼", "▲", "▶" };
    private void drawLines(int height, int offset=0)
    {
        hoverLine = -1;
        Vector2 mousePosition = Event.current.mousePosition;
        GUILayout.Space(height+5);
        int cellSpacing = height / 4;
        for (int spacing = 0; spacing < height; spacing += cellSpacing)
        {
            Color backColor = new Color(.15f, .15f, .15f);
            //AABB mouse hover check
            bool mouseOver = (mousePosition.y > spacing + offset && mousePosition.y < spacing + cellSpacing + offset)
                             && (mousePosition.x>whiteLinePosition&&mousePosition.x<Screen.width) &&!preview;
            if (mouseOver)
            {
                hoverLine = spacing / cellSpacing;
                backColor = new Color(.1f, .1f, .1f);
            }
            //The background
            EditorGUI.DrawRect(new Rect(0, spacing + offset, Screen.width, cellSpacing), backColor);
            //The black outline
            EditorGUI.DrawRect(new Rect(0, spacing + offset, Screen.width, 2), Color.black);
            EditorGUI.DrawRect(new Rect(0, spacing + cellSpacing + offset, Screen.width, 2), Color.black);
            //The white line
            EditorGUI.DrawRect(new Rect(whiteLinePosition, spacing + offset + 1, 2, cellSpacing), Color.white);
            //Arrow symbol
            GUIStyle symbolStyle = new GUIStyle();
            symbolStyle.fontSize = 36;
            symbolStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(5, spacing+offset, 100, 20), symbols[spacing/cellSpacing], symbolStyle);
        }
        drawNodes(cellSpacing, offset);
        if (Event.current.type == EventType.MouseDown && Event.current.button == 1 && !selecting)
        {
            selecting = true;
            selectionStart = mousePosition;
        }
        else if(Event.current.type == EventType.MouseUp)
        {
            selecting = false;
        }
        if (selecting)
        {
            float x = selectionStart.x < mousePosition.x ? selectionStart.x : mousePosition.x;
            float y = selectionStart.y < mousePosition.y ? selectionStart.y : mousePosition.y;
            float selWidth = selectionStart.x < mousePosition.x ? mousePosition.x - selectionStart.x : selectionStart.x - mousePosition.x;
            float selHeight = selectionStart.y < mousePosition.y ? mousePosition.y - selectionStart.y : selectionStart.y - mousePosition.y;
            selectionRect = new Rect(x, y, selWidth, selHeight);
        }
        else
        {
            Undo.RecordObject(OpenedLevel, "Mass node removal");
            for (int track = 0; track < 4; track++)
            {
                List<float> nodes = new List<float>();
                if (OpenedLevel.Tracks.Length >= track)
                {
                    if (OpenedLevel.Tracks[track].Nodes != null)
                    {
                        nodes = OpenedLevel.Tracks[track].Nodes.ToList();
                    }
                }
                if (nodes.Count == 0) continue;
                for (int i = 0; i < nodes.Count; i++)
                {
                    float node = nodes[i];
                    Vector2 position = new Vector2((whiteLinePosition) + (node - playbackTime) * scale, (track * cellSpacing) + offset);
                    position += new Vector2(-7.5f, 20);
                    Rect nodeRect = new Rect(position.x, position.y, 15, 15);
                    if (selectionRect.Contains(nodeRect.center))
                    {
                        OpenedLevel.Tracks[track].Nodes.Remove(node);
                    }
                }
            }
            selectionRect = Rect.zero;
            EditorUtility.SetDirty(OpenedLevel);
        }
        EditorGUI.DrawRect(selectionRect, new Color32(64, 64, 128, 128));
    }

    private float playbackTime;
    private float scale = 30;
    private int selectedNodeIndex = -1;
    private int selectedNodeTrack = -1;
    private void drawNodes(int cellSpacing, int offset)
    {
        for (int track = 0; track < 4; track++)
        {
            List<float> nodes = new List<float>();
            if (OpenedLevel.Tracks.Length >= track)
            {
                if (OpenedLevel.Tracks[track].Nodes != null)
                {
                    nodes = OpenedLevel.Tracks[track].Nodes.ToList();
                }
            }
            if (nodes.Count == 0) continue;
            for(int i = 0; i < nodes.Count; i++)
            {
                float node = nodes[i];
                Vector2 position = new Vector2((whiteLinePosition) + (node - playbackTime) * scale, (track * cellSpacing) + offset);
                position += new Vector2(-7.5f, 20);
                Rect nodeRect = new Rect(position.x, position.y, 15, 15);
                EditorGUI.DrawRect(nodeRect, Color.blue);

                #region Rect events
                if (nodeRect.Contains(Event.current.mousePosition))
                {
                    EditorGUI.DrawRect(nodeRect, new Color32(66, 102, 245, 255));
                    EditorGUIUtility.AddCursorRect(nodeRect, MouseCursor.Pan);
                    if (Event.current.type == EventType.MouseDown)
                    {
                        if (Event.current.button == 0)
                        {
                            selectedNodeIndex = i;
                            selectedNodeTrack = track;
                        }
                        else if (Event.current.button == 1)
                        {
                            OpenedLevel.Tracks[track].Nodes.RemoveAt(i);
                            Undo.RecordObject(OpenedLevel, "Node removal");
                            EditorUtility.SetDirty(OpenedLevel);
                        }
                    }
                }
                if (selectedNodeIndex >= 0 && selectedNodeTrack == track)
                {
                    OpenedLevel.Tracks[track].Nodes[selectedNodeIndex] = getMouseTime();
                    if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
                    {
                        selectedNodeIndex = -1;
                        selectedNodeTrack = -1;
                        OpenedLevel.Tracks[track].Nodes.Sort();
                        OpenedLevel.Tracks[track].Nodes = OpenedLevel.Tracks[track].Nodes.Distinct().ToList(); //remove doubles
                        Undo.RecordObject(OpenedLevel, "Node added");
                        EditorUtility.SetDirty(OpenedLevel);
                    }
                }
                #endregion

                nodeRect.width += 50;
                nodeRect.x -= 25;
                GUI.Label(nodeRect, node.ToString());
            }
        }
    }
    #endregion

    #region Track logic
    private void updateLines()
    {
        if (!preview)
        {
            if (Event.current.type == EventType.ScrollWheel)
            {
                if(Event.current.type == EventType.KeyDown && Event.current.keyCode == (KeyCode.A))
                {
                    scale = Mathf.Clamp(scale + Event.current.delta.y, 1, 50);
                }
                else
                {
                    playbackTime = (float)Math.Round(Mathf.Clamp(playbackTime + Event.current.delta.y, 0, float.MaxValue), 3);
                }
            }
        }
        else
        {
            playbackTime = (float)Math.Round((float)(EditorApplication.timeSinceStartup - startTime), 3);
        }
        Vector2 mousePosition = Event.current.mousePosition;
        if (hoverLine >= 0 && getMouseTime() >= 0 &&!preview)
        {
            TimeSpan time = TimeSpan.FromSeconds(getMouseTime());
            string message = toTimeString(time);

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                addNode((float)time.TotalSeconds);
                Undo.RecordObject(OpenedLevel, "Node added");
                EditorUtility.SetDirty(OpenedLevel);
            }

            GUI.Label(new Rect(mousePosition.x + 20, mousePosition.y + 10, 250, 20), message);
        }
    }

    private void recordKeypresses()
    {
        if (Event.current.type == EventType.KeyDown)
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.W:
                    addNode(playbackTime, 2);
                    break;
                case KeyCode.S:
                    addNode(playbackTime, 1);
                    break;
                case KeyCode.A:
                    addNode(playbackTime, 0);
                    break;
                case KeyCode.D:
                    addNode(playbackTime, 3);
                    break;
                case KeyCode.UpArrow:
                    addNode(playbackTime, 2);
                    break;
                case KeyCode.DownArrow:
                    addNode(playbackTime, 1);
                    break;
                case KeyCode.LeftArrow:
                    addNode(playbackTime, 0);
                    break;
                case KeyCode.RightArrow:
                    addNode(playbackTime, 3);
                    break;
            }
            EditorUtility.SetDirty(OpenedLevel);
        }
    }
    #endregion

    #region Utility methods
    private string toTimeString(float time)
    {
        TimeSpan span = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", span.Minutes, span.Seconds, span.Milliseconds);
    }
    private string toTimeString(TimeSpan span)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}", span.Minutes, span.Seconds, span.Milliseconds);
    }
    private float getMouseTime()
    {
        return (float)Math.Round((Event.current.mousePosition.x - whiteLinePosition)/scale+playbackTime, 5);
    }
    private void addNode(float value, int track=-1)
    {
        int targetTrack = track == -1 ? hoverLine : track;
        if (OpenedLevel.Tracks[targetTrack].Nodes == null)
        {
            OpenedLevel.Tracks[targetTrack].Nodes = new List<float>();
        }
        if (!OpenedLevel.Tracks[targetTrack].Nodes.Contains(value))
        {
            OpenedLevel.Tracks[targetTrack].Nodes.Add(value);
        }
    }
    private void save()
    {
        Undo.RecordObject(OpenedLevel, "DDR Level Save");
        EditorUtility.SetDirty(OpenedLevel);
        AssetDatabase.SaveAssets();
    }
    #endregion

    #endregion

    #region Menu
    private void drawMenu()
    {
        var centeredLabel = GUI.skin.GetStyle("Label");
        centeredLabel.alignment = TextAnchor.UpperCenter;
        centeredLabel.fontStyle = FontStyle.Bold;

        var buttonStyle = GUI.skin.GetStyle("Button");

        EditorGUILayout.LabelField("Welcome to the DDR minigame editor!", centeredLabel);
        EditorGUILayout.LabelField("Select an existing minigame level object to edit it, or create a new one", centeredLabel);

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, 110));
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if(GUILayout.Button("New level"))
        {
            string assetPath = EditorUtility.SaveFilePanelInProject("Level creation", "ddrlevel", "asset", "asset");

            if(assetPath != string.Empty)
            {
                OpenedLevel = new DDRLevel();
                OpenedLevel.Tracks = new DDRTrack[4];
                AssetDatabase.CreateAsset(OpenedLevel, assetPath);
                AssetDatabase.Refresh();

                WindowState = DDRWindowState.Editor;
            }
        }
        if (GUILayout.Button("Open level..."))
        {
            string assetPath = EditorUtility.OpenFilePanel("Level load", Application.dataPath, "asset");
            OpenedLevel = (DDRLevel)AssetDatabase.LoadAssetAtPath(assetPath.Replace(Application.dataPath, "Assets"), typeof(DDRLevel));
            WindowState = DDRWindowState.Editor;
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }
    #endregion

    private void playClip(AudioClip clip, float playTime)
    {
        stopAllClips();
        preview = true;
        int sampleStart = (int)(Math.Ceiling(clip.samples * ((playTime) / clip.length)));
        startTime = EditorApplication.timeSinceStartup - playTime;
        AudioUtility.PlayClip(clip, sampleStart, false);
        AudioUtility.SetClipSamplePosition(clip, sampleStart);
    }
    public void stopAllClips()
    {
        preview = false;
        AudioUtility.StopAllClips();
    }
}
#endif