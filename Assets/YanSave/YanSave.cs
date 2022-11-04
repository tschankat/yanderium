using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections;

public static class YanSave
{
    // This will determine where the savefiles are stored
    // In the future it might be wiser to store them in the AppData directory
    public static string SaveDataPath
    {
        get
        {
            return Path.Combine(Application.streamingAssetsPath, "SaveFiles");
        }
    }

    public const string SAVE_EXTENSION = "yansave";

    public static Action OnLoad;
    public static Action OnSave;

    public static void SaveData(string targetSave)
    {
        // Dynamic serialization
        YanSaveIdentifier[] targetIdentifiers = Resources.FindObjectsOfTypeAll<YanSaveIdentifier>();

        List<SerializedGameObject> serializedGameObjects = new List<SerializedGameObject>();

        foreach (var identifier in targetIdentifiers)
        {
            List<SerializedComponent> serializedComponents = new List<SerializedComponent>();
            foreach (var component in identifier.gameObject.GetComponents(typeof(Component)))
            {
                if (!identifier.EnabledComponents.Contains(component)) continue;

                SerializedComponent serializedComponent = new SerializedComponent();
                serializedComponent.TypePath = component.GetType().AssemblyQualifiedName;
                serializedComponent.PropertyReferences = new ReferenceDict();
                serializedComponent.PropertyValues = new ValueDict();
                serializedComponent.FieldReferences = new ReferenceDict();
                serializedComponent.FieldValues = new ValueDict();
                serializedComponent.FieldReferenceArrays = new ReferenceArrayDict();
                serializedComponent.PropertyReferenceArrays = new ReferenceArrayDict();

                if (typeof(MonoBehaviour).IsAssignableFrom(component.GetType()))
                {
                    serializedComponent.IsMonoBehaviour = true;
                    serializedComponent.IsEnabled = ((MonoBehaviour)component).enabled;
                }

                var type = component.GetType();

                // Save properties and their references
                foreach (var propertyInfo in GetCachedProperties(type))
                {
                    // We only want to read properties that are writeable
                    if (!propertyInfo.CanWrite) continue;
                    if (propertyInfo.IsDefined(typeof(ObsoleteAttribute), true)) continue;

                    // Property skipping per identifier
                    bool isSkipped = false;
                    foreach (var prop in identifier.DisabledProperties)
                        if (prop.Component == component && prop.Name == propertyInfo.Name)
                        {
                            isSkipped = true;
                            break;
                        }
                    if (isSkipped) continue;

                    var value = propertyInfo.GetValue(component);

                    var isComponent = typeof(Component).IsAssignableFrom(propertyInfo.PropertyType);
                    var isGameObject = propertyInfo.PropertyType == typeof(GameObject);
                    var isArray = propertyInfo.PropertyType.IsArray;
                    var isComponentArray = typeof(Component[]).IsAssignableFrom(propertyInfo.PropertyType);
                    var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(propertyInfo.PropertyType);

                    if (value == null) continue;

                    try
                    {
                        if (!isComponent && !isGameObject)
                            serializedComponent.PropertyValues.Add(propertyInfo.Name, value);
                        else if (isArray)
                        {
                            List<string> referenceIDs = new List<string>();
                            if (isComponentArray)
                                referenceIDs.AddRange(((Component[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            else if (isGameObjectArray)
                                referenceIDs.AddRange(((GameObject[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            serializedComponent.PropertyReferenceArrays.Add(propertyInfo.Name, referenceIDs);
                        }
                        else
                        {
                            var referenceID = isComponent ? ((Component)value).gameObject.GetComponent<YanSaveIdentifier>() : (isGameObject ? ((GameObject)value).GetComponent<YanSaveIdentifier>() : null);

                            if (referenceID != null) serializedComponent.PropertyReferences.Add(propertyInfo.Name, referenceID.ObjectID);
                            else serializedComponent.PropertyReferences.Add(propertyInfo.Name, null);
                        }
                    }
                    catch { } // This catches all deprecated properties
                }

                // Save fields and their references
                foreach (var fieldInfo in GetCachedFields(type))
                {
                    // We only want to read fields that are not constant
                    if (fieldInfo.IsLiteral) continue;
                    if (fieldInfo.IsDefined(typeof(ObsoleteAttribute), true)) continue;

                    // Field skipping per identifier
                    bool isSkipped = false;
                    foreach (var field in identifier.DisabledFields)
                        if (field.Component == component && field.Name == fieldInfo.Name)
                        {
                            isSkipped = true;
                            break;
                        }
                    if (isSkipped) continue;

                    var value = fieldInfo.GetValue(component);
                    var isComponent = typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
                    var isGameObject = fieldInfo.FieldType == typeof(GameObject);
                    var isArray = fieldInfo.FieldType.IsArray;
                    var isComponentArray = typeof(Component[]).IsAssignableFrom(fieldInfo.FieldType);
                    var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(fieldInfo.FieldType);

                    try
                    {
                        if (!isComponent && !isGameObject && !isComponentArray && !isGameObjectArray)
                            serializedComponent.FieldValues.Add(fieldInfo.Name, value);
                        else if (isArray)
                        {
                            List<string> referenceIDs = new List<string>();
                            if (isComponentArray)
                                referenceIDs.AddRange(((Component[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            else if (isGameObjectArray)
                                referenceIDs.AddRange(((GameObject[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            serializedComponent.FieldReferenceArrays.Add(fieldInfo.Name, referenceIDs);
                        }
                        else
                        {
                            var referenceID = isComponent ? ((Component)value).gameObject.GetComponent<YanSaveIdentifier>() : (isGameObject ? ((GameObject)value).GetComponent<YanSaveIdentifier>() : null);

                            if (referenceID != null) serializedComponent.FieldReferences.Add(fieldInfo.Name, referenceID.ObjectID);
                            else serializedComponent.FieldReferences.Add(fieldInfo.Name, null);
                        }
                    }
                    catch { } // This catches all deprecated fields
                }

                serializedComponent.OwnerID = identifier.ObjectID;
                serializedComponents.Add(serializedComponent);
            }

            SerializedGameObject serializedGameObject = new SerializedGameObject
            {
                ActiveInHierarchy = identifier.gameObject.activeInHierarchy,
                ActiveSelf = identifier.gameObject.activeSelf,
                IsStatic = identifier.gameObject.isStatic,
                Layer = identifier.gameObject.layer,
                Tag = identifier.gameObject.tag,
                Name = identifier.gameObject.name,
                SerializedComponents = serializedComponents.ToArray(),
                ObjectID = identifier.ObjectID
            };

            serializedGameObjects.Add(serializedGameObject);
        }

        // Static serialization
        YanSaveStaticIdentifier staticIdentifier = UnityEngine.Object.FindObjectOfType<YanSaveStaticIdentifier>();

        List<SerializedStaticClass> serializedClasses = new List<SerializedStaticClass>();
        ValueDict serializedPrefs = new ValueDict();
        if (staticIdentifier != null)
        {
            // Serialize static classes
            foreach (var className in staticIdentifier.StaticTypeNames)
            {
                var type = YanSaveHelpers.GrabType(className);
                if (!(type != null && type.IsAbstract && type.IsSealed)) continue;

                SerializedStaticClass serializedClass = new SerializedStaticClass();
                serializedClass.TypePath = type.AssemblyQualifiedName;
                serializedClass.PropertyReferences = new ReferenceDict();
                serializedClass.PropertyValues = new ValueDict();
                serializedClass.FieldReferences = new ReferenceDict();
                serializedClass.FieldValues = new ValueDict();
                serializedClass.FieldReferenceArrays = new ReferenceArrayDict();
                serializedClass.PropertyReferenceArrays = new ReferenceArrayDict();

                // Save properties and their references
                foreach (var propertyInfo in GetCachedProperties(type))
                {
                    // We only want to read properties that are writeable
                    if (!propertyInfo.CanWrite) continue;
                    if (propertyInfo.IsDefined(typeof(ObsoleteAttribute), true)) continue;

                    // Property skipping per identifier
                    bool isSkipped = false;
                    foreach (var member in staticIdentifier.DisabledMembers)
                        if (member.Value == propertyInfo.Name)
                        {
                            isSkipped = true;
                            break;
                        }
                    if (isSkipped) continue;

                    var value = propertyInfo.GetValue(null, null);

                    var isComponent = typeof(Component).IsAssignableFrom(propertyInfo.PropertyType);
                    var isGameObject = propertyInfo.PropertyType == typeof(GameObject);
                    var isArray = propertyInfo.PropertyType.IsArray;
                    var isComponentArray = typeof(Component[]).IsAssignableFrom(propertyInfo.PropertyType);
                    var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(propertyInfo.PropertyType);

                    if (value == null) continue;

                    try
                    {
                        if (!isComponent && !isGameObject)
                            serializedClass.PropertyValues.Add(propertyInfo.Name, value);
                        else if (isArray)
                        {
                            List<string> referenceIDs = new List<string>();
                            if (isComponentArray)
                                referenceIDs.AddRange(((Component[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            else if (isGameObjectArray)
                                referenceIDs.AddRange(((GameObject[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            serializedClass.PropertyReferenceArrays.Add(propertyInfo.Name, referenceIDs);
                        }
                        else
                        {
                            var referenceID = isComponent ? ((Component)value).gameObject.GetComponent<YanSaveIdentifier>() : (isGameObject ? ((GameObject)value).GetComponent<YanSaveIdentifier>() : null);

                            if (referenceID != null) serializedClass.PropertyReferences.Add(propertyInfo.Name, referenceID.ObjectID);
                            else serializedClass.PropertyReferences.Add(propertyInfo.Name, null);
                        }
                    }
                    catch { } // This catches all deprecated properties
                }

                // Save fields and their references
                foreach (var fieldInfo in GetCachedFields(type))
                {
                    // We only want to read fields that are not constant
                    if (fieldInfo.IsLiteral) continue;
                    if (fieldInfo.IsDefined(typeof(ObsoleteAttribute), true)) continue;

                    // Field skipping per identifier
                    bool isSkipped = false;
                    foreach (var member in staticIdentifier.DisabledMembers)
                        if (member.Value == fieldInfo.Name)
                        {
                            isSkipped = true;
                            break;
                        }
                    if (isSkipped) continue;

                    var value = fieldInfo.GetValue(null);
                    var isComponent = typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
                    var isGameObject = fieldInfo.FieldType == typeof(GameObject);
                    var isArray = fieldInfo.FieldType.IsArray;
                    var isComponentArray = typeof(Component[]).IsAssignableFrom(fieldInfo.FieldType);
                    var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(fieldInfo.FieldType);

                    try
                    {
                        if (!isComponent && !isGameObject && !isComponentArray && !isGameObjectArray)
                            serializedClass.FieldValues.Add(fieldInfo.Name, value);
                        else if (isArray)
                        {
                            List<string> referenceIDs = new List<string>();
                            if (isComponentArray)
                                referenceIDs.AddRange(((Component[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            else if (isGameObjectArray)
                                referenceIDs.AddRange(((GameObject[])value).Select(x => x.GetComponent<YanSaveIdentifier>() != null ? x.GetComponent<YanSaveIdentifier>().ObjectID : string.Empty));
                            serializedClass.FieldReferenceArrays.Add(fieldInfo.Name, referenceIDs);
                        }
                        else
                        {
                            var referenceID = isComponent ? ((Component)value).gameObject.GetComponent<YanSaveIdentifier>() : (isGameObject ? ((GameObject)value).GetComponent<YanSaveIdentifier>() : null);

                            if (referenceID != null) serializedClass.FieldReferences.Add(fieldInfo.Name, referenceID.ObjectID);
                            else serializedClass.FieldReferences.Add(fieldInfo.Name, null);
                        }
                    }
                    catch { } // This catches all deprecated fields
                }

                serializedClasses.Add(serializedClass);
            }

            // Serialize PlayerPrefs
            foreach(var prefTracker in staticIdentifier.PrefTrackers)
            {
                var name = prefTracker.PrefFormat;
                var type = prefTracker.PrefType;

                for(int i = 0; i < prefTracker.RangeMax+1; i++)
                {
                    for (int j = 0; j < prefTracker.PrefFormatValues.Count; j++)
                    {
                        var value = prefTracker.PrefFormatValues[j];

                        int separator = value.LastIndexOf('.');

                        var source = YanSaveHelpers.GrabType(value.Substring(0, separator));

                        var replacement = string.Empty;
                        var field = source.GetField(value.Substring(separator + 1));
                        var property = source.GetProperty(value.Substring(separator + 1));
                        if (property != null)
                            replacement = property.GetValue(null, null).ToString();
                        else if (field != null)
                            replacement = field.GetValue(null).ToString();
                        else
                            Debug.Log($"Couldn't grab replacement value of '{value}'");

                        name = name.Replace($"{{{j}}}", replacement);
                    }

                    var n = name.Replace("{i}", i.ToString());

                    switch (type)
                    {
                        case YanSavePlayerPrefsType.Float:
                            serializedPrefs.Add(n, PlayerPrefs.GetFloat(n));
                            break;
                        case YanSavePlayerPrefsType.Int:
                            serializedPrefs.Add(n, PlayerPrefs.GetInt(n));
                            break;
                        case YanSavePlayerPrefsType.String:
                            serializedPrefs.Add(n, PlayerPrefs.GetString(n));
                            break;
                    }
                }
            }
        }

        // Saving the data
        YanSaveData saveData = new YanSaveData
        {
            LoadedLevelName = SceneManager.GetActiveScene().name,
            SerializedGameObjects = serializedGameObjects.ToArray(),
            SerializedStaticClasses = serializedClasses.ToArray(),
            SerializedPlayerPrefs = serializedPrefs
        };

        string saveDataJson = JsonConvert.SerializeObject(saveData, new JsonSerializerSettings
        {
            ContractResolver = new YanSaveResolver(),
            Error = (s, e) => { e.ErrorContext.Handled = true; }
        });

        if (!Directory.Exists(SaveDataPath)) Directory.CreateDirectory(SaveDataPath);
        File.WriteAllText(Path.Combine(SaveDataPath, $"{targetSave}.{SAVE_EXTENSION}"), saveDataJson);

        OnSave?.Invoke();
    }

    public static void LoadData(string targetSave, bool recreateMissing = false)
    {
        if (!File.Exists(Path.Combine(SaveDataPath, $"{targetSave}.{SAVE_EXTENSION}"))) return;

        string saveDataJson = File.ReadAllText(Path.Combine(SaveDataPath, $"{targetSave}.{SAVE_EXTENSION}"));

        YanSaveData saveData = JsonConvert.DeserializeObject<YanSaveData>(saveDataJson);

        if (SceneManager.GetActiveScene().name != saveData.LoadedLevelName)
            SceneManager.LoadScene(saveData.LoadedLevelName);

        // Dynamic loading
        foreach (SerializedGameObject serializedGameObject in saveData.SerializedGameObjects)
        {
            var gameObject = YanSaveIdentifier.GetObject(serializedGameObject);
            if (gameObject == null)
            {
                if (recreateMissing)
                {
                    gameObject = new GameObject();
                    gameObject.AddComponent<YanSaveIdentifier>().ObjectID = serializedGameObject.ObjectID;
                    gameObject.SetActive(serializedGameObject.ActiveSelf);
                }
                else { continue; }
            }

            // Copy the properties of the serialized gameobject onto the target
            gameObject.isStatic = serializedGameObject.IsStatic;
            gameObject.layer = serializedGameObject.Layer;
            gameObject.tag = serializedGameObject.Tag;
            gameObject.name = serializedGameObject.Name;
            gameObject.SetActive(serializedGameObject.ActiveSelf);

            foreach (SerializedComponent serializedComponent in serializedGameObject.SerializedComponents)
            {
                if (gameObject != null)
                {
                    var type = GetType(serializedComponent.TypePath);
                    if (recreateMissing && gameObject.GetComponent(type) == null) gameObject.AddComponent(type);
                }
            }
        }

        foreach (SerializedGameObject serializedGameObject in saveData.SerializedGameObjects)
        {
            var gameObject = YanSaveIdentifier.GetObject(serializedGameObject);
            if (gameObject == null) continue;

            foreach (SerializedComponent serializedComponent in serializedGameObject.SerializedComponents)
            {
                var type = GetType(serializedComponent.TypePath);
                var component = gameObject.GetComponent(type);

                var identifier = gameObject.GetComponent<YanSaveIdentifier>();

                if (component == null) continue;

                if (serializedComponent.IsMonoBehaviour)
                    ((MonoBehaviour)component).enabled = serializedComponent.IsEnabled;

                // Load properties
                foreach (var propertyInfo in GetCachedProperties(type))
                {
                    // We can't load a property that cannot be written over
                    if (!propertyInfo.CanWrite) continue;

                    var isComponent = typeof(Component).IsAssignableFrom(propertyInfo.PropertyType);

                    if (!isComponent && propertyInfo.PropertyType != typeof(GameObject))
                    {
                        if (serializedComponent.PropertyValues.ContainsKey(propertyInfo.Name))
                        {
                            object value = serializedComponent.PropertyValues[propertyInfo.Name];

                            if (value == null)
                            {
                                propertyInfo.SetValue(component, null);
                                continue;
                            }
                            if (value.GetType() == typeof(JObject))
                            {
                                try { propertyInfo.SetValue(component, ((JObject)value).ToObject(propertyInfo.PropertyType)); }
                                catch { }
                            }
                            else if (value.GetType() == typeof(JArray))
                            {
                                try { propertyInfo.SetValue(component, ((JArray)value).ToObject(propertyInfo.PropertyType)); }
                                catch { }
                            }
                            else
                            {
                                bool isEnum = propertyInfo.PropertyType.IsEnum;
                                bool convertible = typeof(IConvertible).IsAssignableFrom(value.GetType());
                                propertyInfo.SetValue(component, isEnum ? Enum.ToObject(propertyInfo.PropertyType, value) : (convertible ? Convert.ChangeType(value, propertyInfo.PropertyType) : value));
                            }
                        }
                    }
                    else
                    {
                        if (serializedComponent.PropertyReferences.ContainsKey(propertyInfo.Name))
                        {
                            var isGameObject = propertyInfo.PropertyType == typeof(GameObject);
                            var identifiedObject = YanSaveIdentifier.GetObject(serializedComponent.FieldReferences[propertyInfo.Name]);
                            if (identifiedObject == null) continue;

                            if (isComponent)
                            {
                                propertyInfo.SetValue(component, identifiedObject.GetComponent(propertyInfo.PropertyType));
                            }
                            else if (isGameObject)
                            {
                                propertyInfo.SetValue(component, identifiedObject);
                            }
                        }
                        else if (serializedComponent.PropertyReferenceArrays.ContainsKey(propertyInfo.Name))
                        {
                            var isComponentArray = typeof(Component[]).IsAssignableFrom(propertyInfo.PropertyType);
                            var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(propertyInfo.PropertyType);

                            var value = serializedComponent.PropertyReferenceArrays[propertyInfo.Name];
                            var elementType = propertyInfo.PropertyType.GetElementType();
                            if (isComponentArray)
                            {
                                var array = (IList)Array.CreateInstance(elementType, value.Count);
                                for (int i = 0; i < value.Count; i++)
                                {
                                    var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                    var identifierComponent = identifierObject != null ? identifierObject.GetComponent(elementType) : null;
                                    array[i] = identifierComponent;
                                }
                                propertyInfo.SetValue(component, array);
                            }
                            else if (isGameObjectArray)
                            {
                                var array = (IList)Array.CreateInstance(elementType, value.Count);
                                for (int i = 0; i < value.Count; i++)
                                {
                                    var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                    array[i] = identifierObject;
                                }
                                propertyInfo.SetValue(component, array);
                            }
                        }
                    }
                }

                // Load fields
                foreach (var fieldInfo in GetCachedFields(type))
                {
                    var isComponent = typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
                    var isComponentArray = typeof(Component[]).IsAssignableFrom(fieldInfo.FieldType);
                    var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(fieldInfo.FieldType);

                    if (!isComponentArray && !isGameObjectArray && !isComponent && fieldInfo.FieldType != typeof(GameObject))
                    {
                        if (serializedComponent.FieldValues.ContainsKey(fieldInfo.Name))
                        {
                            object value = serializedComponent.FieldValues[fieldInfo.Name];
                            if (value == null)
                            {
                                fieldInfo.SetValue(component, null);
                                continue;
                            }
                            if (value.GetType() == typeof(JObject))
                            {
                                try { fieldInfo.SetValue(component, ((JObject)value).ToObject(fieldInfo.FieldType)); }
                                catch { }
                            }
                            else if (value.GetType() == typeof(JArray))
                            {
                                try { fieldInfo.SetValue(component, ((JArray)value).ToObject(fieldInfo.FieldType)); }
                                catch { }
                            }
                            else
                            {
                                bool isEnum = fieldInfo.FieldType.IsEnum;
                                bool convertible = typeof(IConvertible).IsAssignableFrom(value.GetType());
                                fieldInfo.SetValue(component, isEnum ? Enum.ToObject(fieldInfo.FieldType, value) : (convertible ? Convert.ChangeType(value, fieldInfo.FieldType) : value));
                            }
                        }
                    }
                    else
                    {
                        if (serializedComponent.FieldReferences.ContainsKey(fieldInfo.Name))
                        {
                            var isGameObject = fieldInfo.FieldType == typeof(GameObject);
                            var identifiedObject = YanSaveIdentifier.GetObject(serializedComponent.FieldReferences[fieldInfo.Name]);
                            if (identifiedObject == null) continue;

                            if (isComponent)
                            {
                                fieldInfo.SetValue(component, identifiedObject.GetComponent(fieldInfo.FieldType));
                            }
                            else if (isGameObject)
                            {
                                fieldInfo.SetValue(component, identifiedObject);
                            }
                        }
                        else if (serializedComponent.FieldReferenceArrays.ContainsKey(fieldInfo.Name))
                        {
                            var value = serializedComponent.FieldReferenceArrays[fieldInfo.Name];
                            var elementType = fieldInfo.FieldType.GetElementType();
                            if (isComponentArray)
                            {
                                var array = (IList)Array.CreateInstance(elementType, value.Count);
                                for (int i = 0; i < value.Count; i++)
                                {
                                    var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                    var identifierComponent = identifierObject != null ? identifierObject.GetComponent(elementType) : null;
                                    array[i] = identifierComponent;
                                }
                                fieldInfo.SetValue(component, array);
                            }
                            else if (isGameObjectArray)
                            {
                                var array = (IList)Array.CreateInstance(elementType, value.Count);
                                for (int i = 0; i < value.Count; i++)
                                {
                                    var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                    array[i] = identifierObject;
                                }
                                fieldInfo.SetValue(component, array);
                            }
                        }
                    }
                }
            }
        }

        // Static loading
        foreach (SerializedStaticClass serializedClass in saveData.SerializedStaticClasses)
        {
            var type = GetType(serializedClass.TypePath);

            // The class is not present
            if (type == null) continue;

            // Load properties
            foreach (var propertyInfo in type.GetProperties())
            {
                // We can't load a property that cannot be written over
                if (!propertyInfo.CanWrite) continue;

                var isComponent = typeof(Component).IsAssignableFrom(propertyInfo.PropertyType);

                if (!isComponent && propertyInfo.PropertyType != typeof(GameObject))
                {
                    if (serializedClass.PropertyValues.ContainsKey(propertyInfo.Name))
                    {
                        object value = serializedClass.PropertyValues[propertyInfo.Name];

                        if (value == null)
                        {
                            propertyInfo.SetValue(null, null);
                            continue;
                        }
                        if (value.GetType() == typeof(JObject))
                        {
                            try { propertyInfo.SetValue(null, ((JObject)value).ToObject(propertyInfo.PropertyType)); }
                            catch { }
                        }
                        else if (value.GetType() == typeof(JArray))
                        {
                            try { propertyInfo.SetValue(null, ((JArray)value).ToObject(propertyInfo.PropertyType)); }
                            catch { }
                        }
                        else
                        {
                            bool isEnum = propertyInfo.PropertyType.IsEnum;
                            bool convertible = typeof(IConvertible).IsAssignableFrom(value.GetType());
                            propertyInfo.SetValue(null, isEnum ? Enum.ToObject(propertyInfo.PropertyType, value) : (convertible ? Convert.ChangeType(value, propertyInfo.PropertyType) : value));
                        }
                    }
                }
                else
                {
                    if (serializedClass.PropertyReferences.ContainsKey(propertyInfo.Name))
                    {
                        var isGameObject = propertyInfo.PropertyType == typeof(GameObject);
                        var identifiedObject = YanSaveIdentifier.GetObject(serializedClass.FieldReferences[propertyInfo.Name]);
                        if (identifiedObject == null) continue;

                        if (isComponent)
                        {
                            propertyInfo.SetValue(null, identifiedObject.GetComponent(propertyInfo.PropertyType));
                        }
                        else if (isGameObject)
                        {
                            propertyInfo.SetValue(null, identifiedObject);
                        }
                    }
                    else if (serializedClass.PropertyReferenceArrays.ContainsKey(propertyInfo.Name))
                    {
                        var isComponentArray = typeof(Component[]).IsAssignableFrom(propertyInfo.PropertyType);
                        var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(propertyInfo.PropertyType);

                        var value = serializedClass.PropertyReferenceArrays[propertyInfo.Name];
                        var elementType = propertyInfo.PropertyType.GetElementType();
                        if (isComponentArray)
                        {
                            var array = (IList)Array.CreateInstance(elementType, value.Count);
                            for (int i = 0; i < value.Count; i++)
                            {
                                var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                var identifierComponent = identifierObject != null ? identifierObject.GetComponent(elementType) : null;
                                array[i] = identifierComponent;
                            }
                            propertyInfo.SetValue(null, array);
                        }
                        else if (isGameObjectArray)
                        {
                            var array = (IList)Array.CreateInstance(elementType, value.Count);
                            for (int i = 0; i < value.Count; i++)
                            {
                                var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                array[i] = identifierObject;
                            }
                            propertyInfo.SetValue(null, array);
                        }
                    }
                }
            }

            // Load fields
            foreach (var fieldInfo in type.GetFields())
            {
                var isComponent = typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
                var isComponentArray = typeof(Component[]).IsAssignableFrom(fieldInfo.FieldType);
                var isGameObjectArray = typeof(GameObject[]).IsAssignableFrom(fieldInfo.FieldType);

                if (!isComponentArray && !isGameObjectArray && !isComponent && fieldInfo.FieldType != typeof(GameObject))
                {
                    if (serializedClass.FieldValues.ContainsKey(fieldInfo.Name))
                    {
                        object value = serializedClass.FieldValues[fieldInfo.Name];
                        if (value == null)
                        {
                            fieldInfo.SetValue(null, null);
                            continue;
                        }
                        if (value.GetType() == typeof(JObject))
                        {
                            try { fieldInfo.SetValue(null, ((JObject)value).ToObject(fieldInfo.FieldType)); }
                            catch { }
                        }
                        else if (value.GetType() == typeof(JArray))
                        {
                            try { fieldInfo.SetValue(null, ((JArray)value).ToObject(fieldInfo.FieldType)); }
                            catch { }
                        }
                        else
                        {
                            bool isEnum = fieldInfo.FieldType.IsEnum;
                            bool convertible = typeof(IConvertible).IsAssignableFrom(value.GetType());
                            fieldInfo.SetValue(null, isEnum ? Enum.ToObject(fieldInfo.FieldType, value) : (convertible ? Convert.ChangeType(value, fieldInfo.FieldType) : value));
                        }
                    }
                }
                else
                {
                    if (serializedClass.FieldReferences.ContainsKey(fieldInfo.Name))
                    {
                        var isGameObject = fieldInfo.FieldType == typeof(GameObject);
                        var identifiedObject = YanSaveIdentifier.GetObject(serializedClass.FieldReferences[fieldInfo.Name]);
                        if (identifiedObject == null) continue;

                        if (isComponent)
                        {
                            fieldInfo.SetValue(null, identifiedObject.GetComponent(fieldInfo.FieldType));
                        }
                        else if (isGameObject)
                        {
                            fieldInfo.SetValue(null, identifiedObject);
                        }
                    }
                    else if (serializedClass.FieldReferenceArrays.ContainsKey(fieldInfo.Name))
                    {
                        var value = serializedClass.FieldReferenceArrays[fieldInfo.Name];
                        var elementType = fieldInfo.FieldType.GetElementType();
                        if (isComponentArray)
                        {
                            var array = (IList)Array.CreateInstance(elementType, value.Count);
                            for (int i = 0; i < value.Count; i++)
                            {
                                var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                var identifierComponent = identifierObject != null ? identifierObject.GetComponent(elementType) : null;
                                array[i] = identifierComponent;
                            }
                            fieldInfo.SetValue(null, array);
                        }
                        else if (isGameObjectArray)
                        {
                            var array = (IList)Array.CreateInstance(elementType, value.Count);
                            for (int i = 0; i < value.Count; i++)
                            {
                                var identifierObject = YanSaveIdentifier.GetObject(value[i]);
                                array[i] = identifierObject;
                            }
                            fieldInfo.SetValue(null, array);
                        }
                    }
                }
            }
        }

        OnLoad?.Invoke();
    }

    public static void LoadPrefs(string targetSave)
    {
        string saveDataJson = File.ReadAllText(Path.Combine(SaveDataPath, $"{targetSave}.{SAVE_EXTENSION}"));

        YanSaveData saveData = JsonConvert.DeserializeObject<YanSaveData>(saveDataJson);

        foreach (var serializedPref in saveData.SerializedPlayerPrefs)
        {
            var value = serializedPref.Value;
            var type = value.GetType();
            if (type == typeof(double) || type == typeof(float))
                PlayerPrefs.SetFloat(serializedPref.Key, (float)value);
            else if (type == typeof(string))
                PlayerPrefs.SetString(serializedPref.Key, (string)value);
            else if (type == typeof(short) || type == typeof(int) || type == typeof(long))
                PlayerPrefs.SetInt(serializedPref.Key, Convert.ToInt32(value));
        }
    }

    public static void LoadAll(string targetSave)
    {
        LoadData(targetSave);
        LoadPrefs(targetSave);
    }

    public static void RemoveData(string targetSave)
    {
        var path = Path.Combine(SaveDataPath, $"{targetSave}.{SAVE_EXTENSION}");
        try
        {
            if (File.Exists(path))
                File.Delete(path);
        }catch { }
    }

    private static Dictionary<Type, PropertyInfo[]> PropertyCache = new Dictionary<Type, PropertyInfo[]>();
    private static Dictionary<Type, FieldInfo[]> FieldCache = new Dictionary<Type, FieldInfo[]>();

    private static PropertyInfo[] GetCachedProperties(Type type)
    {
        if (PropertyCache.ContainsKey(type)) 
            return PropertyCache[type];

        PropertyCache.Add(type, type.GetProperties());

        return PropertyCache[type];
    }
    private static FieldInfo[] GetCachedFields(Type type)
    {
        if (FieldCache.ContainsKey(type))
            return FieldCache[type];

        FieldInfo[] fields = type.GetFields();
        FieldCache.Add(type, fields);

        return fields;
    }
    private static Type GetType(string typeName)
    {
        var type = Type.GetType(typeName);
        if (type != null)
            return type;

        var assemblyName = typeName.Substring(0, typeName.IndexOf('.'));

        var assembly = Assembly.Load(assemblyName);
        if (assembly == null)
            return null;

        return assembly.GetType(typeName);
    }
}