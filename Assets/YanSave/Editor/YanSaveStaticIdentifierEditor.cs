using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(YanSaveStaticIdentifier))]
public class YanSaveStaticIdentifierEditor : Editor
{
    private Type ManagedType;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var identifier = (YanSaveStaticIdentifier)serializedObject.targetObject;

        var labelMargin = new GUIStyle(GUI.skin.label);
        labelMargin.margin = new RectOffset(20, 0, 0, 0);

        var buttonMargin = new GUIStyle(GUI.skin.button);
        buttonMargin.margin = new RectOffset(20, 0, 2, 0);

        var textfieldMargin = new GUIStyle(EditorStyles.textField);
        textfieldMargin.margin = new RectOffset(20, 0, 2, 0);

        var subStyle = new GUIStyle(GUI.skin.button);
        subStyle.margin = new RectOffset(30, 0, 2, 0);

        var subLabel = new GUIStyle(GUI.skin.label);
        subLabel.margin = new RectOffset(30, 0, 0, 0);

        var intStyle = new GUIStyle(GUI.skin.textField);
        intStyle.fixedWidth = 50;

        GUILayout.Label("Static classes", labelMargin);

        for(int i = 0; i < identifier.StaticTypeNames.Count; i++)
        {
            var valid = classValid(identifier.StaticTypeNames[i], identifier);
            var type = YanSaveHelpers.GrabType(identifier.StaticTypeNames[i]);

            var targetColor = valid ? Color.green : Color.red;
            textfieldMargin.normal.textColor = targetColor;
            textfieldMargin.hover.textColor = targetColor;
            textfieldMargin.active.textColor = targetColor;
            textfieldMargin.focused.textColor = targetColor;

            GUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!valid);
            if (GUILayout.Button("Manage", buttonMargin))
                ManagedType = ManagedType == type ? null : type;
            EditorGUI.EndDisabledGroup();
            identifier.StaticTypeNames[i] = EditorGUILayout.TextField(identifier.StaticTypeNames[i], textfieldMargin);
            if (GUILayout.Button("-")) identifier.StaticTypeNames.RemoveAt(i);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            if (ManagedType == type)
                drawClassInspector(type);
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+", buttonMargin)) identifier.StaticTypeNames.Add(string.Empty);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        GUILayout.Label("PlayerPrefs values", labelMargin);

        for(int i = 0; i < identifier.PrefTrackers.Count; i++)
        {
            YanSavePlayerPrefTracker tracker = identifier.PrefTrackers[i];
            EditorGUILayout.BeginHorizontal();
            tracker.PrefFormat = EditorGUILayout.TextField(tracker.PrefFormat);

            tracker.PrefType = (YanSavePlayerPrefsType)EditorGUILayout.EnumPopup(tracker.PrefType);

            tracker.RangeMax = EditorGUILayout.IntField(tracker.RangeMax, intStyle);

            identifier.PrefTrackers[i] = tracker;

            if (GUILayout.Button("-"))
            {
                identifier.PrefTrackers.Remove(tracker);
                EditorUtility.SetDirty(identifier);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            // Parameters

            if (tracker.PrefFormatValues == null)
                tracker.PrefFormatValues = new List<string>();

            if (tracker.PrefFormatValues.Count != 0)
            {
                for (int j = 0; j < tracker.PrefFormatValues.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label($"{{{j}}}", subLabel);
                    tracker.PrefFormatValues[j] = EditorGUILayout.TextField(tracker.PrefFormatValues[j]);
                    if (GUILayout.Button("-"))
                    {
                        tracker.PrefFormatValues.RemoveAt(j);
                        EditorUtility.SetDirty(identifier);
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.BeginHorizontal();
            string content = tracker.PrefFormat;
            if(GUILayout.Button("Add static parameter", subStyle))
            {
                tracker.PrefFormatValues.Add(string.Empty);
                EditorUtility.SetDirty(identifier);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            identifier.PrefTrackers.Add(new YanSavePlayerPrefTracker { PrefFormatValues = new List<string>() });
            EditorUtility.SetDirty(identifier);
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void drawClassInspector(Type type)
    {
        if (type == null) return;

        var subStyle = new GUIStyle(GUI.skin.button);
        subStyle.margin = new RectOffset(30, 0, 2, 0);

        var identifier = (YanSaveStaticIdentifier)serializedObject.targetObject;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Include all", subStyle))
        {
            foreach (var pair in identifier.DisabledMembers.Where(x => x.Key == type).ToArray())
                identifier.DisabledMembers.Remove(pair);
            EditorUtility.SetDirty(identifier);
        }
        if (GUILayout.Button("Skip all"))
        {
            // Clear
            foreach (var pair in identifier.DisabledMembers.Where(x => x.Key == type).ToArray())
                identifier.DisabledMembers.Remove(pair);

            // Add properties
            foreach (var propertyInfo in type.GetProperties().ToArray())
                identifier.DisabledMembers.Add(new KeyValuePair<Type, string>(type, propertyInfo.Name));

            // Add fields
            foreach (var fieldInfo in type.GetFields().ToArray())
                identifier.DisabledMembers.Add(new KeyValuePair<Type, string>(type, fieldInfo.Name));

            EditorUtility.SetDirty(identifier);
        }
        if (GUILayout.Button("Only system types [except strings]"))
        {
            // if (prop.PropertyType.Namespace == "System" && prop.PropertyType != typeof(string)) continue;
            // Clear
            foreach (var pair in identifier.DisabledMembers.Where(x => x.Key == type).ToArray())
                identifier.DisabledMembers.Remove(pair);

            // Add properties
            foreach (var propertyInfo in type.GetProperties().ToArray())
                if(!(propertyInfo.PropertyType.Namespace == "System" && propertyInfo.PropertyType != typeof(string))) 
                    identifier.DisabledMembers.Add(new KeyValuePair<Type, string>(type, propertyInfo.Name));

            // Add fields
            foreach (var fieldInfo in type.GetFields().ToArray())
                if (!(fieldInfo.FieldType.Namespace == "System" && fieldInfo.FieldType != typeof(string)))
                    identifier.DisabledMembers.Add(new KeyValuePair<Type, string>(type, fieldInfo.Name));

            EditorUtility.SetDirty(identifier);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        // Properties
        foreach (var property in type.GetProperties())
        {
            GUILayout.BeginHorizontal();

            bool includeProperty = true;
            foreach (var member in identifier.DisabledMembers)
            {
                if (member.Key == type && member.Value == property.Name) includeProperty = false;
            }

            if (GUILayout.Button(includeProperty ? "Included" : "Skipped", subStyle))
            {
                includeProperty = !includeProperty;

                if (includeProperty)
                {
                    foreach (var member in identifier.DisabledMembers.ToArray())
                    {
                        if (member.Key == type && member.Value == property.Name)
                        {
                            identifier.DisabledMembers.Remove(member);
                        }
                    }
                }
                else
                {
                    bool found = false;
                    foreach (var member in identifier.DisabledMembers.ToArray())
                    {
                        if (member.Key == type && member.Value == property.Name)
                        {
                            found = true;
                        }
                    }
                    if (!found) identifier.DisabledMembers.Add(new KeyValuePair<Type, string>(type, property.Name));
                }

                EditorUtility.SetDirty(identifier);
            }

            includeProperty = true;
            foreach (var member in identifier.DisabledMembers)
            {
                if (member.Key == type && member.Value == property.Name) includeProperty = false;
            }

            EditorGUI.BeginDisabledGroup(!includeProperty);
            GUILayout.Label(property.Name);
            EditorGUI.EndDisabledGroup();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // Fields
        foreach (var field in type.GetFields())
        {
            GUILayout.BeginHorizontal();

            bool includeProperty = true;
            foreach (var member in identifier.DisabledMembers)
            {
                if (member.Key == type && member.Value == field.Name) includeProperty = false;
            }

            if (GUILayout.Button(includeProperty ? "Included" : "Skipped", subStyle))
            {
                includeProperty = !includeProperty;

                if (includeProperty)
                {
                    foreach (var member in identifier.DisabledMembers.ToArray())
                    {
                        if (member.Key == type && member.Value == field.Name)
                        {
                            identifier.DisabledMembers.Remove(member);
                        }
                    }
                }
                else
                {
                    bool found = false;
                    foreach (var member in identifier.DisabledMembers.ToArray())
                    {
                        if (member.Key == type && member.Value == field.Name)
                        {
                            found = true;
                        }
                    }
                    if (!found) identifier.DisabledMembers.Add(new KeyValuePair<Type, string>(type, field.Name));
                }

                EditorUtility.SetDirty(identifier);
            }

            includeProperty = true;
            foreach (var member in identifier.DisabledMembers)
            {
                if (member.Key == type && member.Value == field.Name) includeProperty = false;
            }

            EditorGUI.BeginDisabledGroup(!includeProperty);
            GUILayout.Label(field.Name);
            EditorGUI.EndDisabledGroup();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }

    private bool classValid(string name, YanSaveStaticIdentifier identifier)
    {
        Type t = YanSaveHelpers.GrabType(name);
        int count = identifier.StaticTypeNames.Where(x => x == name).ToList().Count;
        return t != null && t.IsAbstract && t.IsSealed && count == 1;
    }
}
