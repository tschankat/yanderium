using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Bayat.Core
{

#if BAYAT_DEVELOPER
    [CreateAssetMenu(menuName = "Bayat/Core/Asset Reference Resolver")]
#endif
    public class AssetReferenceResolver : ScriptableObject
    {

        private static AssetReferenceResolver current;

        /// <summary>
        /// Gets the current asset reference resolver.
        /// </summary>
        public static AssetReferenceResolver Current
        {
            get
            {
                if (current == null)
                {
                    AssetReferenceResolver instance = null;
#if UNITY_EDITOR
                    string[] instanceGuids = AssetDatabase.FindAssets("t: AssetReferenceResolver");
                    if (instanceGuids.Length > 1)
                    {
                        Debug.LogError("There is more than one AssetReferenceResolver in this project, but there must only be one.");
                        Debug.Log("Deleting other instances of AssetReferenceResolver and keeping only one instance.");
                        Debug.LogFormat("The selected instance is: {0}", AssetDatabase.GUIDToAssetPath(instanceGuids[0]));
                        instance = AssetDatabase.LoadAssetAtPath<AssetReferenceResolver>(AssetDatabase.GUIDToAssetPath(instanceGuids[0]));

                        // Delete other instances
                        for (int i = 1; i < instanceGuids.Length; i++)
                        {
                            string instanceGuid = instanceGuids[i];
                            AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(instanceGuid));
                        }
                    }
                    else if (instanceGuids.Length == 0)
                    {
                        Debug.Log("No Asset Reference Resolver instance found, creating a new one at 'Assets/Resources/Bayat/Core'.");
                        instance = ScriptableObject.CreateInstance<AssetReferenceResolver>();
                        System.IO.Directory.CreateDirectory("Assets/Resources/Bayat/Core");
                        AssetDatabase.CreateAsset(instance, "Assets/Resources/Bayat/Core/AssetReferenceResolver.asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        instance = AssetDatabase.LoadAssetAtPath<AssetReferenceResolver>(AssetDatabase.GUIDToAssetPath(instanceGuids[0]));
                    }
#else
                    instance = Resources.Load<AssetReferenceResolver>("Bayat/Core/AssetReferenceResolver");
#endif
                    current = instance;
                }
                return current;
            }
        }

        [SerializeField]
        protected List<string> guids = new List<string>();
        [SerializeField]
        protected List<UnityEngine.Object> dependencies = new List<UnityEngine.Object>();

        /// <summary>
        /// Gets the GUIDs list.
        /// </summary>
        public virtual List<string> Guids
        {
            get
            {
                return this.guids;
            }
        }

        /// <summary>
        /// Gets the project dependencies list.
        /// </summary>
        public virtual List<UnityEngine.Object> Dependencies
        {
            get
            {
                return this.dependencies;
            }
        }

        public virtual void Reset()
        {
#if UNITY_EDITOR
            Debug.Log("Resetting Asset Reference Resolver");
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Collects the default dependencies.
        /// </summary>
        public virtual void CollectDefaultDependencies()
        {
            bool undoRecorded = false;

            // Remove NULL values from Dictionary.
            if (RemoveNullReferences() > 0)
            {
                Undo.RecordObject(this, "Update AssetReferenceResolver List");
                undoRecorded = true;
            }

            UnityEngine.Object[] builtInExtraAssets = AssetDatabase.LoadAllAssetsAtPath("Resources/unity_builtin_extra");
            UnityEngine.Object[] defaultAssets = AssetDatabase.LoadAllAssetsAtPath("Library/unity default resources");
            List<UnityEngine.Object> allAssets = new List<UnityEngine.Object>();
            for (int i = 0; i < builtInExtraAssets.Length; i++)
            {
                UnityEngine.Object assetObj = builtInExtraAssets[i];
                if (assetObj == null || allAssets.Contains(assetObj))
                {
                    continue;
                }
                allAssets.Add(assetObj);
            }
            for (int i = 0; i < defaultAssets.Length; i++)
            {
                UnityEngine.Object assetObj = defaultAssets[i];
                if (assetObj == null || allAssets.Contains(assetObj))
                {
                    continue;
                }
                allAssets.Add(assetObj);
            }

            foreach (var asset in allAssets)
            {
                if (asset == null || !CanBeSaved(asset))
                {
                    continue;
                }
                Type assetType = asset.GetType();
                if (typeof(MonoScript).IsAssignableFrom(assetType) || typeof(UnityEditor.DefaultAsset).IsAssignableFrom(assetType))
                {
                    continue;
                }

                // If we're adding a new item to the type list, make sure we've recorded an undo for the object.
                if (string.IsNullOrEmpty(ResolveGuid(asset)))
                {
                    if (!undoRecorded)
                    {
                        Undo.RecordObject(this, "Update AssetReferenceResolver List");
                        undoRecorded = true;
                    }
                    string guid = Guid.NewGuid().ToString("N");
                    Add(guid, asset);
                }
            }

            MaterialPropertiesResolver.Current.CollectMaterials();
        }

        /// <summary>
        /// Collects the project dependencies from build scenes.
        /// </summary>
        public virtual void CollectProjectDependencies()
        {
            bool undoRecorded = false;

            // Remove NULL values from Dictionary.
            if (RemoveNullReferences() > 0)
            {
                Undo.RecordObject(this, "Update AssetReferenceResolver List");
                undoRecorded = true;
            }

            List<UnityEngine.Object> allAssets = new List<UnityEngine.Object>();
            Scene activeScene = EditorSceneManager.GetActiveScene();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                EditorBuildSettingsScene editorScene = EditorBuildSettings.scenes[i];
                Scene scene;
                if (activeScene.path == editorScene.path)
                {
                    scene = activeScene;
                }
                else
                {
                    scene = EditorSceneManager.OpenScene(editorScene.path, OpenSceneMode.Additive);
                }
                UnityEngine.Object[] dependencies = EditorUtility.CollectDependencies(scene.GetRootGameObjects());
                foreach (var sceneDependency in dependencies)
                {
                    if (EditorUtility.IsPersistent(sceneDependency))
                    {
                        allAssets.Add(sceneDependency);
                    }
                }
                if (scene != activeScene)
                {
                    EditorSceneManager.CloseScene(scene, true);
                }
            }

            foreach (var asset in allAssets)
            {
                if (asset == null || !CanBeSaved(asset))
                {
                    continue;
                }

                Type assetType = asset.GetType();
                if (typeof(MonoScript).IsAssignableFrom(assetType) || typeof(UnityEditor.DefaultAsset).IsAssignableFrom(assetType))
                {
                    continue;
                }

                // If we're adding a new item to the type list, make sure we've recorded an undo for the object.
                if (string.IsNullOrEmpty(ResolveGuid(asset)))
                {
                    if (!undoRecorded)
                    {
                        Undo.RecordObject(this, "Update AssetReferenceResolver List");
                        undoRecorded = true;
                    }
                    string guid = Guid.NewGuid().ToString("N");
                    Add(guid, asset);
                }
            }

            MaterialPropertiesResolver.Current.CollectMaterials();
        }

        /// <summary>
        /// Collects the active scene dependencies from build scenes.
        /// </summary>
        public virtual void CollectActiveSceneDependencies()
        {
            bool undoRecorded = false;

            // Remove NULL values from Dictionary.
            if (RemoveNullReferences() > 0)
            {
                Undo.RecordObject(this, "Update AssetReferenceResolver List");
                undoRecorded = true;
            }

            List<UnityEngine.Object> allAssets = new List<UnityEngine.Object>();
            Scene activeScene = EditorSceneManager.GetActiveScene();
            UnityEngine.Object[] dependencies = EditorUtility.CollectDependencies(activeScene.GetRootGameObjects());
            foreach (var sceneDependency in dependencies)
            {
                if (EditorUtility.IsPersistent(sceneDependency))
                {
                    allAssets.Add(sceneDependency);
                }
            }

            foreach (var asset in allAssets)
            {
                if (asset == null || !CanBeSaved(asset))
                {
                    continue;
                }

                Type assetType = asset.GetType();
                if (typeof(MonoScript).IsAssignableFrom(assetType) || typeof(UnityEditor.DefaultAsset).IsAssignableFrom(assetType))
                {
                    continue;
                }

                // If we're adding a new item to the type list, make sure we've recorded an undo for the object.
                if (string.IsNullOrEmpty(ResolveGuid(asset)))
                {
                    if (!undoRecorded)
                    {
                        Undo.RecordObject(this, "Update AssetReferenceResolver List");
                        undoRecorded = true;
                    }
                    string guid = Guid.NewGuid().ToString("N");
                    Add(guid, asset);
                }
            }

            MaterialPropertiesResolver.Current.CollectMaterials();
        }

        /// <summary>
        /// Collects the whole project dependencies.
        /// </summary>
        public virtual void CollectAllProjectDependencies()
        {
            CollectDefaultDependencies();
            bool undoRecorded = false;

            // Remove NULL values from Dictionary.
            if (RemoveNullReferences() > 0)
            {
                Undo.RecordObject(this, "Update AssetReferenceResolver List");
                undoRecorded = true;
            }

            string[] assetGuids = AssetDatabase.FindAssets("t: Object");
            List<UnityEngine.Object> allAssets = new List<UnityEngine.Object>();

            for (int i = 0; i < assetGuids.Length; i++)
            {
                string assetGuid = assetGuids[i];
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                UnityEngine.Object assetObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
                if (assetObj == null || allAssets.Contains(assetObj))
                {
                    continue;
                }
                allAssets.Add(assetObj);
            }

            foreach (var asset in allAssets)
            {
                if (asset == null || !CanBeSaved(asset))
                {
                    continue;
                }
                Type assetType = asset.GetType();
                if (typeof(MonoScript).IsAssignableFrom(assetType) || typeof(UnityEditor.DefaultAsset).IsAssignableFrom(assetType))
                {
                    continue;
                }

                // If we're adding a new item to the type list, make sure we've recorded an undo for the object.
                if (string.IsNullOrEmpty(ResolveGuid(asset)))
                {
                    if (!undoRecorded)
                    {
                        Undo.RecordObject(this, "Update AssetReferenceResolver List");
                        undoRecorded = true;
                    }
                    string guid = Guid.NewGuid().ToString("N");
                    Add(guid, asset);
                }
            }

            MaterialPropertiesResolver.Current.CollectMaterials();
        }
#endif

        /// <summary>
        /// Checks whether if has any null references or not.
        /// </summary>
        /// <returns>True if has null references otherwise false</returns>
        public virtual bool HasNullReferences()
        {
            int index = 0;
            while (index < this.guids.Count)
            {
                if (this.dependencies[index] == null)
                {
                    return true;
                }
                else
                {
                    index++;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the null references.
        /// </summary>
        /// <returns>The count of removed references</returns>
        public virtual int RemoveNullReferences()
        {
            int index = 0;
            int removedCount = 0;
            while (index < this.guids.Count)
            {
                if (this.dependencies[index] == null)
                {
                    this.guids.RemoveAt(index);
                    this.dependencies.RemoveAt(index);
                    removedCount++;
                }
                else
                {
                    index++;
                }
            }
            return removedCount;
        }

        /// <summary>
        /// Checks whether contains the GUID or not.
        /// </summary>
        /// <param name="guid">The GUID</param>
        /// <returns>True if has GUID otherwise false</returns>
        public virtual bool Contains(string guid)
        {
            return this.guids.Contains(guid);
        }

        /// <summary>
        /// Checks whether contains the object or not.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>True if has object otherwise false</returns>
        public virtual bool Contains(UnityEngine.Object obj)
        {
            return this.dependencies.Contains(obj);
        }

        /// <summary>
        /// Resolves the GUID and gets the object associated to it.
        /// </summary>
        /// <param name="guid">The GUID</param>
        /// <returns>The object associated to this GUID</returns>
        public virtual UnityEngine.Object ResolveReference(string guid)
        {
            int index = this.guids.IndexOf(guid);
            if (index == -1)
            {
                return null;
            }
            else
            {
                return this.dependencies[index];
            }
        }

        /// <summary>
        /// Resolves the object and gets the GUID associated to it.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The GUID associated to the object</returns>
        public virtual string ResolveGuid(UnityEngine.Object obj)
        {
            int index = this.dependencies.IndexOf(obj);
            if (index == -1)
            {
                return null;
            }
            else
            {
                return this.guids[index];
            }
        }

        /// <summary>
        /// Adds a new reference.
        /// </summary>
        /// <param name="guid">The guid</param>
        /// <param name="obj">The object</param>
        /// <returns>The GUID or the generated GUID if the given GUID is null or empty</returns>
        public virtual string Add(string guid, UnityEngine.Object obj)
        {
            if (this.dependencies.Contains(obj))
            {
                return null;
            }
            if (this.guids.Contains(guid))
            {
                return null;
            }
            string newGuid = guid;
            if (string.IsNullOrEmpty(guid))
            {
                newGuid = Guid.NewGuid().ToString("N");
                this.guids.Add(newGuid);
            }
            else
            {
                this.guids.Add(guid);
            }
            this.dependencies.Add(obj);
            return newGuid;
        }

        /// <summary>
        /// Adds a new object reference by generating a new GUID.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The generated GUID</returns>
        public virtual string Add(UnityEngine.Object obj)
        {
            if (this.dependencies.Contains(obj))
            {
                return null;
            }
            Guid guid = Guid.NewGuid();
            this.guids.Add(guid.ToString("N"));
            this.dependencies.Add(obj);
            return guid.ToString("N");
        }

#if UNITY_EDITOR
        /// <summary>
        /// Checks whether the object can be saved or not.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>True if can be saved otherwise false</returns>
        public static bool CanBeSaved(UnityEngine.Object obj)
        {
            // Check if any of the hide flags determine that it should not be saved.
            if ((((obj.hideFlags & HideFlags.DontSave) == HideFlags.DontSave) ||
                 ((obj.hideFlags & HideFlags.DontSaveInBuild) == HideFlags.DontSaveInBuild) ||
                 ((obj.hideFlags & HideFlags.DontSaveInEditor) == HideFlags.DontSaveInEditor) ||
                 ((obj.hideFlags & HideFlags.HideAndDontSave) == HideFlags.HideAndDontSave)))
            {
                var type = obj.GetType();
                // Meshes are marked with HideAndDontSave, but shouldn't be ignored.
                if (type != typeof(Mesh) && type != typeof(Material))
                {
                    return false;
                }
            }
            return true;
        }
#endif

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        public static void UpdateLocation()
        {
            string path = AssetDatabase.GetAssetPath(AssetReferenceResolver.Current);
            if (path.StartsWith("Assets/Plugins/Bayat"))
            {
                if (EditorUtility.DisplayDialog("Updating AssetReferenceResolver Location",
                    "The save system requires the AssetReferenceResolver to reside at Assets/Resources/Bayat/Core folder, " +
                    "press Update to update and move the AssetReferenceResolver to the desired folder.", "Update", "Cancel"))
                {
                    if (!Directory.Exists("Assets/Resources"))
                    {
                        AssetDatabase.CreateFolder("Assets", "Resources");
                    }
                    if (!Directory.Exists("Assets/Resources/Bayat"))
                    {
                        AssetDatabase.CreateFolder("Assets/Resources", "Bayat");
                    }
                    if (!Directory.Exists("Assets/Resources/Bayat/Core"))
                    {
                        AssetDatabase.CreateFolder("Assets/Resources/Bayat", "Core");
                    }
                    string message = AssetDatabase.MoveAsset(path, "Assets/Resources/Bayat/Core/AssetReferenceResolver.asset");
                    if (string.IsNullOrEmpty(message))
                    {
                        Debug.Log("The Asset Reference Resolver has been moved to 'Assets/Resources/Bayat/Core' successfully");
                    }
                    else
                    {
                        Debug.LogError(message);
                    }
                }
            }
        }
#endif

    }

}