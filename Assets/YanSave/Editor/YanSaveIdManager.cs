using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System;
using System.Linq;

public class YanSaveIdManager : EditorWindow
{
    [MenuItem("Tools/Yandere Simulator/YanSave ID Manager")]
    public static void ShowWindow()
    {
        YanSaveIdManager window = (YanSaveIdManager)GetWindow(typeof(YanSaveIdManager));
        window.titleContent = new GUIContent("YanSave ID Manager");
        window.minSize = new Vector2(550, 300);
        window.position = new Rect(window.position.x, window.position.y, 600, 650);
        window.grabIdentifiers();
    }

    private const int ID_LIST_MAX = 100;
    private int identifierPage = 0;

    private ReferenceDraw referenceDraw;
    private string searchFilter;
    private Vector2 identifierScroll;
    private bool identifierReadonly = true;

    // Assign editor variables
    private List<string> componentFilters = new List<string>();
    private bool assignEditor;

    private List<YanSaveIdentifier> identifiersCache = new List<YanSaveIdentifier>();

    void OnGUI()
    {
        // Draw the header
        EditorGUILayout.Space();
        var centerStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

        centerStyle.fontSize = 20;
        EditorGUILayout.LabelField("YanSave ID Manager", centerStyle, GUILayout.ExpandWidth(true));

        EditorGUILayout.Space();
        centerStyle.fontSize = 14;
        EditorGUILayout.LabelField($"Managing IDs for object references in scene '{SceneManager.GetActiveScene().name}'", centerStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        if (!assignEditor) drawMain();
        else drawAssignEditor();
    }

    private static Dictionary<Type, List<string>> DisabledMembers = new Dictionary<Type, List<string>>();
    private Type managedType;
    private Vector2 propertyViewScroll;

    private void drawAssignEditor()
    {
        var labelMargin = new GUIStyle(GUI.skin.label);
        labelMargin.margin = new RectOffset(20, 0, 0, 0);

        var buttonMargin = new GUIStyle(GUI.skin.button);
        buttonMargin.margin = new RectOffset(20, 0, 2, 0);

        var textfieldMargin = new GUIStyle(EditorStyles.textField);
        textfieldMargin.margin = new RectOffset(20, 0, 2, 0);

        GUILayout.Label("Generate references for objects with criteria...", labelMargin);

        EditorGUILayout.Space();

        // Component filter
        GUILayout.Label("Object contains component of type:", labelMargin);
        for(int i = 0; i < componentFilters.Count; i++)
        {
            var targetColor = componentValid(componentFilters[i]) ? Color.green : Color.red;
            textfieldMargin.normal.textColor = targetColor;
            textfieldMargin.hover.textColor = targetColor;
            textfieldMargin.active.textColor = targetColor;
            textfieldMargin.focused.textColor = targetColor;

            GUILayout.BeginHorizontal();
            componentFilters[i] = EditorGUILayout.TextField(componentFilters[i], textfieldMargin);
            if (GUILayout.Button("-")) componentFilters.RemoveAt(i);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+", buttonMargin)) componentFilters.Add(string.Empty);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        var subStyle = new GUIStyle(GUI.skin.button);
        subStyle.margin = new RectOffset(30, 0, 2, 0);

        // Property-Field filter
        GUILayout.Label("Property settings:", labelMargin);
        foreach(var filter in componentFilters)
        {
            var type = YanSaveHelpers.GrabType(filter);
            if (type == null) continue;

            if (!DisabledMembers.ContainsKey(type))
                DisabledMembers.Add(type, new List<string>());

            bool managed = type == managedType;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Manage", buttonMargin)) managedType = managed ? null : type;
            GUILayout.Label(type.Name);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            if (managed)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Include all", subStyle))
                    DisabledMembers[type].Clear();
                if (GUILayout.Button("Skip all"))
                {
                    DisabledMembers[type] = type.GetProperties().Select(x => x.Name).ToList();
                    DisabledMembers[type].AddRange(type.GetFields().Select(x => x.Name));
                }
                if(GUILayout.Button("Only system types [except strings]"))
                {
                    DisabledMembers[type].Clear();
                    foreach(var property in type.GetProperties())
                    {
                        if (property.PropertyType.Namespace == "System" && property.PropertyType != typeof(string)) continue;
                        DisabledMembers[type].Add(property.Name);
                    }
                    foreach (var field in type.GetFields())
                    {
                        if (field.FieldType.Namespace == "System" && field.FieldType != typeof(string)) continue;
                        DisabledMembers[type].Add(field.Name);
                    }
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                propertyViewScroll = GUILayout.BeginScrollView(propertyViewScroll);

                // Properties
                foreach (var property in type.GetProperties())
                {
                    bool includeProperty = !DisabledMembers[type].Contains(property.Name);


                    GUILayout.BeginHorizontal();

                    if(GUILayout.Button(includeProperty ? "Included" : "Skipped", subStyle))
                    {
                        includeProperty = !includeProperty;
                        if (includeProperty && DisabledMembers[type].Contains(property.Name)) DisabledMembers[type].Remove(property.Name);
                        else if (!includeProperty && !DisabledMembers[type].Contains(property.Name)) DisabledMembers[type].Add(property.Name);
                    }

                    includeProperty = !DisabledMembers[type].Contains(property.Name);

                    EditorGUI.BeginDisabledGroup(!includeProperty);
                    GUILayout.Label(property.Name);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }

                // Fields
                foreach (var field in type.GetFields())
                {
                    bool includeField = !DisabledMembers[type].Contains(field.Name);


                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button(includeField ? "Included" : "Skipped", subStyle))
                    {
                        includeField = !includeField;
                        if (includeField && DisabledMembers[type].Contains(field.Name)) DisabledMembers[type].Remove(field.Name);
                        else if (!includeField && !DisabledMembers[type].Contains(field.Name)) DisabledMembers[type].Add(field.Name);
                    }

                    includeField = !DisabledMembers[type].Contains(field.Name);

                    EditorGUI.BeginDisabledGroup(!includeField);
                    GUILayout.Label(field.Name);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
            }
        }

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Generate", buttonMargin)) generateReferences();
        if(GUILayout.Button("Cancel")) assignEditor = false;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void drawMain()
    {
        // Draw the action buttons
        var buttonLeft = new GUIStyle(GUI.skin.button);
        var buttonMid = new GUIStyle(GUI.skin.button);
        var buttonRight = new GUIStyle(GUI.skin.button);
        buttonLeft.margin = new RectOffset(20, 0, 0, 0);
        buttonMid.margin = new RectOffset(0, 0, 0, 0);
        buttonRight.margin = new RectOffset(0, 20, 0, 0);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate references", buttonLeft)) assignEditor = true;
        if (GUILayout.Button("Remove all references", buttonMid)) removeReferences();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        // Draw the mode selection buttons
        Color bgColor = GUI.backgroundColor;
        EditorGUILayout.BeginHorizontal();
        if (referenceDraw == ReferenceDraw.Auto) GUI.backgroundColor = Color.gray;
        if (GUILayout.Button("Auto generated", buttonLeft)) referenceDraw = ReferenceDraw.Auto;
        GUI.backgroundColor = bgColor;

        if (referenceDraw == ReferenceDraw.User) GUI.backgroundColor = Color.gray;
        if (GUILayout.Button("Manually created", buttonMid)) referenceDraw = ReferenceDraw.User;
        GUI.backgroundColor = bgColor;

        if (referenceDraw == ReferenceDraw.All) GUI.backgroundColor = Color.gray;
        if (GUILayout.Button("Both", buttonRight)) referenceDraw = ReferenceDraw.All;
        GUI.backgroundColor = bgColor;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Draw the searchbar
        GUILayout.BeginHorizontal();
        var searchStyle = new GUIStyle(GUI.skin.FindStyle("ToolbarSeachTextField"));
        searchStyle.margin = new RectOffset(20, 2, 0, 0);
        searchFilter = GUILayout.TextField(searchFilter, searchStyle);
        if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
        {
            searchFilter = "";
            GUI.FocusControl(null);
        }
        GUILayout.EndHorizontal();

        // Draw the indentifiers
        EditorGUILayout.Space();
        drawReferences();
    }

    private bool componentValid(string name)
    {
        Type t = YanSaveHelpers.GrabType(name);
        return t != null && typeof(Component).IsAssignableFrom(t);
    }
    
    

    private void grabIdentifiers()
    {
        identifiersCache.Clear();
        identifiersCache.AddRange(Resources.FindObjectsOfTypeAll<YanSaveIdentifier>());
    }

    private void drawReferences()
    {
        var toggleStyle = new GUIStyle(GUI.skin.toggle);
        toggleStyle.margin = new RectOffset(20, 0, 0, 0);

        var removeStyle = new GUIStyle(GUI.skin.button);
        removeStyle.margin = new RectOffset(0, 20, 0, 0);

        var textStyle = new GUIStyle(GUI.skin.textField);
        textStyle.margin = new RectOffset(20, 0, 2, 0);

        var prevStyle = new GUIStyle(GUI.skin.button);
        prevStyle.fixedWidth = 100;
        prevStyle.margin = new RectOffset(20, 20, 0, 0);

        identifierReadonly = GUILayout.Toggle(identifierReadonly, "Read only", toggleStyle);

        EditorGUILayout.Space();

        if (GUILayout.Button("Refresh", prevStyle)) grabIdentifiers();

        // Filtering
        List<YanSaveIdentifier> identifiers = new List<YanSaveIdentifier>();
        foreach(YanSaveIdentifier identifier in identifiersCache)
        {
            if (identifier.AutoGenerated && referenceDraw == ReferenceDraw.User) continue;
            else if (!identifier.AutoGenerated && referenceDraw == ReferenceDraw.Auto) continue;

            if (!string.IsNullOrWhiteSpace(searchFilter))
            {
                if (identifier.ObjectID.ToLower().Contains(searchFilter)) identifiers.Insert(0, identifier);
            }
            else identifiers.Insert(0, identifier);
        }

        if (identifiers.Count <= 0) return;

        EditorGUILayout.Space();

        // Draw the page buttons
        int lastPage = Mathf.CeilToInt((float)identifiers.Count / ID_LIST_MAX) - 1;
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(identifierPage==0);
        if(GUILayout.Button("Previous", prevStyle)) identifierPage--;
        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();

        EditorGUI.BeginDisabledGroup(identifierPage == lastPage);
        if(GUILayout.Button("Next", prevStyle)) identifierPage++;
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        identifierPage = Mathf.Clamp(identifierPage, 0, lastPage);

        // Draw the properties
        identifierScroll = EditorGUILayout.BeginScrollView(identifierScroll);
        EditorGUI.BeginDisabledGroup(identifierReadonly);
        foreach (YanSaveIdentifier identifier in identifiers.GetRange(identifierPage * ID_LIST_MAX, Mathf.Min(ID_LIST_MAX, identifiers.Count - (identifierPage * ID_LIST_MAX))))
        {
            EditorGUILayout.BeginHorizontal();
            identifier.ObjectID = EditorGUILayout.TextField(identifier.ObjectID, textStyle);
            EditorGUILayout.ObjectField(identifier.gameObject, typeof(GameObject), false);
            if(GUILayout.Button("Remove", removeStyle))
            {
                DestroyImmediate(identifier);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndScrollView();
    }

    private void generateReferences()
    {
        Undo.RegisterSnapshot();
        grabIdentifiers();

        List<Type> filters = new List<Type>();
        foreach(var filter in componentFilters)
        {
            if (componentValid(filter))
                filters.Add(YanSaveHelpers.GrabType(filter));
        }

        foreach(GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if(gameObject.GetComponent<YanSaveIdentifier>() == null)
            {
                bool include = true;
                foreach(var filter in filters)
                {
                    if (gameObject.GetComponent(filter) == null) include = false;
                }
                if (include)
                {
                    YanSaveIdentifier identifier = gameObject.AddComponent<YanSaveIdentifier>();
                    identifier.ObjectID = $"auto.{Regex.Replace(gameObject.name, @"\s+", "")}.{Mathf.Abs((int)UnityEngine.Random.Range(100000000, 999999999)) + Mathf.Abs((int)gameObject.transform.position.magnitude)}";
                    identifier.AutoGenerated = true;

                    foreach(string filter in componentFilters)
                    {
                        var type = YanSaveHelpers.GrabType(filter);
                        var component = identifier.GetComponent(type);
                        if(component != null &&!identifier.EnabledComponents.Contains(component))
                            identifier.EnabledComponents.Add(component);

                        if (DisabledMembers.ContainsKey(type))
                        {
                            foreach(var disabledName in DisabledMembers[type])
                            {
                                var property = type.GetProperty(disabledName);
                                var field = type.GetField(disabledName);

                                DisabledYanSaveMember member = new DisabledYanSaveMember
                                {
                                    Component = component,
                                    Name = disabledName
                                };

                                if (property != null)
                                {
                                    if (!identifier.DisabledProperties.Contains(member))
                                        identifier.DisabledProperties.Add(member);
                                }
                                if (field != null)
                                {
                                    if (!identifier.DisabledFields.Contains(member))
                                        identifier.DisabledFields.Add(member);
                                }
                            }
                        }
                    }
                }
            }
        }

        grabIdentifiers();
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    private void removeReferences()
    {
        if(EditorUtility.DisplayDialogComplex("Remove all references", 
                                             $"This action will remove all ID references present in '{SceneManager.GetActiveScene().name}'. This will make all previous savefiles useless. Do you want to proceed?", 
                                             "Yes", "No", null) == 0)
        {
            foreach(YanSaveIdentifier identifier in Resources.FindObjectsOfTypeAll<YanSaveIdentifier>())
            {
                DestroyImmediate(identifier);
            }
        }
        grabIdentifiers();
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    private enum ReferenceDraw
    {
        Auto, User, All
    }
}