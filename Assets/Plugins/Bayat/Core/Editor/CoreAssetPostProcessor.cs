using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Bayat.Core
{

    [InitializeOnLoad]
    public class CoreAssetPostProcessor : UnityEditor.AssetModificationProcessor
    {

        static CoreAssetPostProcessor()
        {
            EditorApplication.update -= Update;
            EditorApplication.update += Update;
            EditorSceneManager.sceneSaving -= EditorSceneManager_sceneSaving;
            EditorSceneManager.sceneSaving += EditorSceneManager_sceneSaving;
        }

        private static void EditorSceneManager_sceneSaving(UnityEngine.SceneManagement.Scene scene, string path)
        {

            // Collect scene dependencies before saving the scene.
            if (SceneReferenceResolver.Current != null && SceneReferenceResolver.Current.UpdateOnSceneSaving)
            {
                SceneReferenceResolver.Current.CollectSceneDependencies();
            }
        }

        static void Update()
        {
            if (EditorApplication.isPlaying)
            {
                return;
            }

            // Collect scene dependencies when entering playmode.
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (SceneReferenceResolver.Current != null && SceneReferenceResolver.Current.UpdateOnEnteringPlayMode)
                {
                    SceneReferenceResolver.Current.CollectSceneDependencies();
                }
                return;
            }
        }

    }

}