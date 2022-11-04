using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Bayat.Core
{

    /// <summary>
    /// The scene reference resolver.
    /// </summary>
    [AddComponentMenu("Bayat/Core/Scene Reference Resolver")]
    [DisallowMultipleComponent]
    public class SceneReferenceResolver : MonoBehaviour, ISerializationCallbackReceiver
    {

        private static SceneReferenceResolver current;

        /// <summary>
        /// Gets the current scene reference resolver.
        /// </summary>
        public static SceneReferenceResolver Current
        {
            get
            {
                if (current == null)
                {
                    SceneReferenceResolver[] instances = FindObjectsOfType<SceneReferenceResolver>();
#if !UNITY_EDITOR
                    if (instances.Length == 0)
                    {
                        CreateNewInstance();
                    }
#endif
                    if (instances.Length == 1)
                    {
                        current = instances[0];
                    }
                    else if (instances.Length > 1)
                    {
                        throw new InvalidOperationException("There is more than one SceneReferenceResolver in this scene, but there must only be one.");
                    }
                }
                return current;
            }
        }

        [SerializeField]
        protected List<string> guids = new List<string>();
        [SerializeField]
        protected List<UnityEngine.Object> sceneDependencies = new List<UnityEngine.Object>();
#if UNITY_EDITOR
        [SerializeField]
        protected List<UnityEngine.Object> availableDependencies = new List<UnityEngine.Object>();
        [SerializeField]
        protected bool updateOnSceneSaving = true;
        [SerializeField]
        protected bool updateOnEnteringPlayMode = true;
#endif

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
        /// Gets the scene dependencies list.
        /// </summary>
        public virtual List<UnityEngine.Object> SceneDependencies
        {
            get
            {
                return this.sceneDependencies;
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Gets the available scene dependencies list.
        /// </summary>
        public virtual List<UnityEngine.Object> AvailableDependencies
        {
            get
            {
                return this.availableDependencies;
            }
        }

        public virtual bool UpdateOnSceneSaving
        {
            get
            {
                return this.updateOnSceneSaving;
            }
        }

        public virtual bool UpdateOnEnteringPlayMode
        {
            get
            {
                return this.updateOnEnteringPlayMode;
            }
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            // This is called before building or when things are being serialised before pressing play.
            if (BuildPipeline.isBuildingPlayer || (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying))
            {
                RemoveNullReferences();
            }
#endif
        }

        public void OnAfterDeserialize() { }

        public virtual void Reset()
        {
#if UNITY_EDITOR
            CollectSceneDependencies();
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Adds the specified objects and their dependencies to the reference resolver.
        /// </summary>
        /// <param name="objs"></param>
        public void AddDependencies(UnityEngine.Object[] objs)
        {
            bool assetsModified = false;
            foreach (var obj in objs)
            {
                var dependencies = EditorUtility.CollectDependencies(new UnityEngine.Object[] { obj });

                foreach (var dependency in dependencies)
                {
                    if (EditorUtility.IsPersistent(dependency))
                    {
                        if (!AssetReferenceResolver.Current.Contains(dependency))
                        {
                            AssetReferenceResolver.Current.Add(dependency);
                        }
                        continue;
                    }

                    if (dependency == null || !CanBeSaved(dependency) || AssetReferenceResolver.Current.Contains(dependency))
                    {
                        continue;
                    }

                    Add(dependency);
                }
            }
            if (assetsModified)
            {
                AssetDatabase.SaveAssets();
            }
            Undo.RecordObject(this, "Update SceneReferenceResolver List");
        }

        /// <summary>
        /// Collects the scene dependencies.
        /// </summary>
        public void CollectSceneDependencies()
        {
            bool undoRecorded = false;

            // Remove NULL values from Dictionary.
            if (RemoveNullReferences() > 0)
            {
                Undo.RecordObject(this, "Update SceneReferenceResolver List");
                undoRecorded = true;
            }

            var sceneObjects = this.gameObject.scene.GetRootGameObjects();
            var dependencies = EditorUtility.CollectDependencies(sceneObjects);
            //var deepHierarchy = new List<UnityEngine.Object>(EditorUtility.CollectDeepHierarchy(sceneObjects));
            bool assetsModified = false;

            for (int i = 0; i < dependencies.Length; i++)
            {
                var obj = dependencies[i];

                if (EditorUtility.IsPersistent(obj))
                {
                    if (!AssetReferenceResolver.Current.Contains(obj))
                    {
                        AssetReferenceResolver.Current.Add(obj);
                        assetsModified = true;
                    }
                    continue;
                }

                if (obj == null || !CanBeSaved(obj) || AssetReferenceResolver.Current.Contains(obj))
                {
                    continue;
                }

                // If we're adding a new item to the type list, make sure we've recorded an undo for the object.
                if (string.IsNullOrEmpty(ResolveGuid(obj)))
                {
                    if (!undoRecorded)
                    {
                        Undo.RecordObject(this, "Update SceneReferenceResolver List");
                        undoRecorded = true;
                    }
                    Add(obj);
                }
            }

            MaterialPropertiesResolver.Current.CollectMaterials();
            if (assetsModified)
            {
                AssetDatabase.SaveAssets();
            }
            GetAvailableDependencies();
        }

        public void GetAvailableDependencies()
        {
            var availableSceneDependencies = new List<UnityEngine.Object>();
            var sceneObjects = this.gameObject.scene.GetRootGameObjects();
            var dependencies = EditorUtility.CollectDependencies(sceneObjects);
            foreach (var dependency in dependencies)
            {
                if (EditorUtility.IsPersistent(dependency) || this.sceneDependencies.Contains(dependency))
                {
                    continue;
                }
                availableSceneDependencies.Add(dependency);
            }
            this.availableDependencies = availableSceneDependencies;
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
                if (this.sceneDependencies[index] == null)
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
                if (this.sceneDependencies[index] == null)
                {
                    this.guids.RemoveAt(index);
                    this.sceneDependencies.RemoveAt(index);
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
        /// <param name="obj">The scene object</param>
        /// <returns>True if has object otherwise false</returns>
        public virtual bool Contains(UnityEngine.Object obj)
        {
            return this.sceneDependencies.Contains(obj);
        }

        /// <summary>
        /// Resolves the GUID and gets the scene object associated to it.
        /// </summary>
        /// <param name="guid">The GUID</param>
        /// <returns>The scene object associated to this GUID</returns>
        public virtual UnityEngine.Object ResolveReference(string guid)
        {
            int index = this.guids.IndexOf(guid);
            if (index == -1)
            {
                return null;
            }
            else
            {
                return this.sceneDependencies[index];
            }
        }

        /// <summary>
        /// Resolves the object and gets the GUID associated to it.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The GUID associated to the object</returns>
        public virtual string ResolveGuid(UnityEngine.Object obj)
        {
            int index = this.sceneDependencies.IndexOf(obj);
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
            if (this.sceneDependencies.Contains(obj))
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
            this.sceneDependencies.Add(obj);
            return newGuid;
        }

        /// <summary>
        /// Adds a new object reference by generating a new GUID.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The generated GUID</returns>
        public virtual string Add(UnityEngine.Object obj)
        {
            if (this.sceneDependencies.Contains(obj))
            {
                return null;
            }
            Guid guid = Guid.NewGuid();
            this.guids.Add(guid.ToString("N"));
            this.sceneDependencies.Add(obj);
            return guid.ToString("N");
        }

        /// <summary>
        /// Sets the reference with the guid.
        /// </summary>
        /// <param name="guid">The guid</param>
        /// <param name="obj">The object</param>
        /// <returns>True if succeed, otherwise false</returns>
        public virtual bool Set(string guid, UnityEngine.Object obj)
        {
            if (!this.guids.Contains(guid) || obj == null)
            {
                return false;
            }
            int index = this.guids.IndexOf(guid);
            this.sceneDependencies[index] = obj;
            return true;
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

        /// <summary>
        /// Creates a new instance of <see cref="SceneReferenceResolver"/> in the current scene.
        /// </summary>
#if UNITY_EDITOR
        [MenuItem("GameObject/Bayat/Core/Scene Reference Resolver", false, 10)]
#endif
        public static void CreateNewInstance()
        {
            GameObject go = new GameObject("Scene Reference Resolver");
            current = go.AddComponent<SceneReferenceResolver>();
#if UNITY_EDITOR
            Selection.activeGameObject = go;
#endif
        }

    }

}