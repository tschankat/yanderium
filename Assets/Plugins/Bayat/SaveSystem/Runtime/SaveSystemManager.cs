using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Bayat.Core;

namespace Bayat.SaveSystem
{

    [AddComponentMenu("Bayat/Save System/Save System Manager")]
    [DisallowMultipleComponent]
    public class SaveSystemManager : MonoBehaviour
    {

        private static SaveSystemManager current;

        /// <summary>
        /// Gets the current scene reference resolver.
        /// </summary>
        public static SaveSystemManager Current
        {
            get
            {
                if (current == null)
                {
                    SaveSystemManager[] instances = FindObjectsOfType<SaveSystemManager>();
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
                        throw new InvalidOperationException("There is more than one SaveSystemManager in this scene, but there must only be one.");
                    }
                }
                return current;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="SceneReferenceResolver"/> in the current scene.
        /// </summary>
#if UNITY_EDITOR
        [MenuItem("GameObject/Bayat/Save System/Save System Manager", false, 10)]
#endif
        public static void CreateNewInstance()
        {
            GameObject go = new GameObject("Save System Manager");
            current = go.AddComponent<SaveSystemManager>();
            go.AddComponent<AutoSaveManager>();
            go.AddComponent<SceneReferenceResolver>();
#if UNITY_EDITOR
            Selection.activeGameObject = go;
#endif
        }

    }

}