using System.Collections.Generic;
using System;

[Serializable]
public struct YanSaveData
{
    public string LoadedLevelName;

    public SerializedGameObject[] SerializedGameObjects;
    public SerializedStaticClass[] SerializedStaticClasses;
    public ValueDict SerializedPlayerPrefs;
}

[Serializable]
public struct SerializedComponent
{
    public string OwnerID;
    public string TypePath;
    public ValueDict PropertyValues;
    public ReferenceDict PropertyReferences;
    public ValueDict FieldValues;
    public ReferenceDict FieldReferences;
    public ReferenceArrayDict PropertyReferenceArrays;
    public ReferenceArrayDict FieldReferenceArrays;
    public bool IsEnabled;
    public bool IsMonoBehaviour;
}

[Serializable]
public struct SerializedGameObject
{
    public bool ActiveInHierarchy;
    public bool ActiveSelf;
    public bool IsStatic;
    public int Layer;
    public string Tag;
    public string Name;

    public string ObjectID;

    public SerializedComponent[] SerializedComponents;
}

[Serializable]
public struct SerializedStaticClass
{
    public string TypePath;
    public ValueDict PropertyValues;
    public ValueDict FieldValues;
    public ReferenceDict PropertyReferences;
    public ReferenceDict FieldReferences;
    public ReferenceArrayDict PropertyReferenceArrays;
    public ReferenceArrayDict FieldReferenceArrays;
}

[Serializable] public class ValueDict : SerializableDictionary<string, object> { }
[Serializable] public class ReferenceDict : SerializableDictionary<string, string> { }
[Serializable] public class ReferenceArrayDict : SerializableDictionary<string, List<string>> { }