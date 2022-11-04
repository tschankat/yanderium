using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(YanSaveIdentifier))]
public class YanSaveIdentifierEditor : Editor
{
    private Component[] components;
    private Component managedComponent;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var identifier = (YanSaveIdentifier)serializedObject.targetObject;

        components = identifier.gameObject.GetComponents<Component>();

        if (!identifier.InitializedInspector && string.IsNullOrEmpty(identifier.ObjectID))
            identifier.ObjectID = identifier.gameObject.name.Replace(" ", "_");
        identifier.InitializedInspector = true;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectID"));

        // Component editor
        foreach(var component in components)
        {
            EditorGUILayout.BeginHorizontal();

            bool includeComponent = identifier.EnabledComponents.Contains(component);
            if(GUILayout.Button(includeComponent ? "Included" : "Skipped"))
            {
                if (identifier.EnabledComponents.Contains(component)) identifier.EnabledComponents.Remove(component);
                else identifier.EnabledComponents.Add(component);

                EditorUtility.SetDirty(identifier);
            }
            includeComponent = identifier.EnabledComponents.Contains(component);

            if (!includeComponent && managedComponent == component) managedComponent = null;

            bool managed = managedComponent == component;

            EditorGUI.BeginDisabledGroup(!includeComponent);
            if(GUILayout.Button("Manage")) managedComponent = managed ? null : component;
            EditorGUILayout.ObjectField(component, null, false);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            var subStyle = new GUIStyle(GUI.skin.button);
            subStyle.margin = new RectOffset(30, 0, 2, 0);

            if (managed)
            {
                GUILayout.BeginHorizontal();
                if(GUILayout.Button("Include all", subStyle))
                {
                    foreach(var disabledProp in identifier.DisabledProperties.ToArray())
                    {
                        if(disabledProp.Component == component)
                        {
                            identifier.DisabledProperties.Remove(disabledProp);
                        }
                    }
                    foreach (var disabledField in identifier.DisabledFields.ToArray())
                    {
                        if (disabledField.Component == component)
                        {
                            identifier.DisabledFields.Remove(disabledField);
                        }
                    }

                    EditorUtility.SetDirty(identifier);
                }
                if(GUILayout.Button("Skip all"))
                {
                    foreach (var disabledProp in identifier.DisabledProperties.ToArray())
                    {
                        if (disabledProp.Component == component)
                        {
                            identifier.DisabledProperties.Remove(disabledProp);
                        }
                    }
                    foreach (var disabledField in identifier.DisabledFields.ToArray())
                    {
                        if (disabledField.Component == component)
                        {
                            identifier.DisabledFields.Remove(disabledField);
                        }
                    }
                    string[] names = component.GetType().GetProperties().Select(x => x.Name).ToArray();
                    foreach(var name in names)
                    {
                        identifier.DisabledProperties.Add(new DisabledYanSaveMember
                        {
                            Name = name,
                            Component = component
                        });
                    }
                    names = component.GetType().GetFields().Select(x => x.Name).ToArray();
                    foreach (var name in names)
                    {
                        identifier.DisabledFields.Add(new DisabledYanSaveMember
                        {
                            Name = name,
                            Component = component
                        });
                    }

                    EditorUtility.SetDirty(identifier);
                }
                if(GUILayout.Button("Only system types [except strings]"))
                {
                    foreach (var disabledProp in identifier.DisabledProperties.ToArray())
                    {
                        if (disabledProp.Component == component)
                        {
                            identifier.DisabledProperties.Remove(disabledProp);
                        }
                    }
                    foreach (var disabledField in identifier.DisabledFields.ToArray())
                    {
                        if (disabledField.Component == component)
                        {
                            identifier.DisabledFields.Remove(disabledField);
                        }
                    }
                    string[] names = component.GetType().GetProperties().Select(x => x.Name).ToArray();
                    foreach (var name in names)
                    {
                        var prop = component.GetType().GetProperty(name);
                        if (prop.PropertyType.Namespace == "System" && prop.PropertyType != typeof(string)) continue;
                        identifier.DisabledProperties.Add(new DisabledYanSaveMember
                        {
                            Name = name,
                            Component = component
                        });
                    }
                    names = component.GetType().GetFields().Select(x => x.Name).ToArray();
                    foreach (var name in names)
                    {
                        var field = component.GetType().GetField(name);
                        if (field.FieldType.Namespace == "System" && field.FieldType != typeof(string)) continue;
                        identifier.DisabledFields.Add(new DisabledYanSaveMember
                        {
                            Name = name,
                            Component = component
                        });
                    }

                    EditorUtility.SetDirty(identifier);
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                // Properties
                foreach(var property in component.GetType().GetProperties())
                {
                    GUILayout.BeginHorizontal();

                    bool includeProperty = true;
                    foreach(var prop in identifier.DisabledProperties)
                    {
                        if (prop.Component == component && prop.Name == property.Name) includeProperty = false;
                    }

                    if (GUILayout.Button(includeProperty ? "Included" : "Skipped", subStyle))
                    {
                        includeProperty = !includeProperty;

                        if (includeProperty)
                        {
                            foreach (var prop in identifier.DisabledProperties.ToArray())
                            {
                                if (prop.Component == component && prop.Name == property.Name)
                                {
                                    identifier.DisabledProperties.Remove(prop);
                                }
                            }
                        }
                        else
                        {
                            bool found = false;
                            foreach (var prop in identifier.DisabledProperties.ToArray())
                            {
                                if (prop.Component == component && prop.Name == property.Name)
                                {
                                    found = true;
                                }
                            }
                            if (!found) identifier.DisabledProperties.Add(new DisabledYanSaveMember
                            {
                                Component = component,
                                Name = property.Name
                            });
                        }

                        EditorUtility.SetDirty(identifier);
                    }

                    includeProperty = true;
                    foreach (var prop in identifier.DisabledProperties)
                    {
                        if (prop.Component == component && prop.Name == property.Name) includeProperty = false;
                    }

                    EditorGUI.BeginDisabledGroup(!includeProperty);
                    GUILayout.Label(property.Name);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }

                // Fields
                foreach (var field in component.GetType().GetFields())
                {
                    GUILayout.BeginHorizontal();

                    bool includeProperty = true;
                    foreach (var prop in identifier.DisabledFields)
                    {
                        if (prop.Component == component && prop.Name == field.Name) includeProperty = false;
                    }

                    if (GUILayout.Button(includeProperty ? "Included" : "Skipped", subStyle))
                    {
                        includeProperty = !includeProperty;

                        if (includeProperty)
                        {
                            foreach (var prop in identifier.DisabledFields.ToArray())
                            {
                                if (prop.Component == component && prop.Name == field.Name)
                                {
                                    identifier.DisabledFields.Remove(prop);
                                }
                            }
                        }
                        else
                        {
                            bool found = false;
                            foreach (var prop in identifier.DisabledFields.ToArray())
                            {
                                if (prop.Component == component && prop.Name == field.Name)
                                {
                                    found = true;
                                }
                            }
                            if (!found) identifier.DisabledFields.Add(new DisabledYanSaveMember
                            {
                                Component = component,
                                Name = field.Name
                            });
                        }

                        EditorUtility.SetDirty(identifier);
                    }

                    includeProperty = true;
                    foreach (var prop in identifier.DisabledFields)
                    {
                        if (prop.Component == component && prop.Name == field.Name) includeProperty = false;
                    }

                    EditorGUI.BeginDisabledGroup(!includeProperty);
                    GUILayout.Label(field.Name);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.Space();
            }

            EditorGUI.EndDisabledGroup();
        }

        EditorGUI.BeginDisabledGroup(true);
        GUILayout.Label($"This identifier was {(identifier.AutoGenerated ? "automatically generated" : "manually set up")}.");
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}