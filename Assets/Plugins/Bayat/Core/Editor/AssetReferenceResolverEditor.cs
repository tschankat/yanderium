using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

using Bayat.Core.EditorWindows;

namespace Bayat.Core
{

    [CustomEditor(typeof(AssetReferenceResolver), true)]
    public class AssetReferenceResolverEditor : Editor
    {

        protected AssetReferenceResolver assetReferenceResolver;
        protected bool foldout = false;

        private void OnEnable()
        {
            this.assetReferenceResolver = (AssetReferenceResolver)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (assetReferenceResolver == null)
            {
                return;
            }

            int currentDependenciesCount = assetReferenceResolver.Dependencies.Count;
            GUILayout.Label(string.Format("Current: {0}", currentDependenciesCount));
            if (this.assetReferenceResolver.HasNullReferences())
            {
                EditorGUILayout.HelpBox("There are null references in the asset dependencies, remove them to stop causing further issues and errors.", MessageType.Warning);
                if (GUILayout.Button("Remove Null References"))
                {
                    this.assetReferenceResolver.RemoveNullReferences();
                }
            }
            if (GUILayout.Button("Reset"))
            {
                if (EditorUtility.DisplayDialog("Reset Asset Reference Database?", "This action will reset whole asset reference database and used GUIDs which makes the saved GUIDs obsolote, so there will be problems when loading previously saved data using this database.\n\nProceed at your own risk.", "Reset", "Cancel"))
                {
                    this.assetReferenceResolver.Guids.Clear();
                    this.assetReferenceResolver.Dependencies.Clear();
                    this.assetReferenceResolver.Reset();
                }
            }

            if (GUILayout.Button("Open Reference Manager"))
            {
                new AssetReferenceManagerWindow().Show();
            }
            if (GUILayout.Button("Collect Project Dependencies"))
            {
                this.assetReferenceResolver.CollectProjectDependencies();
            }
            if (GUILayout.Button("Collect Active Scene Dependencies"))
            {
                this.assetReferenceResolver.CollectActiveSceneDependencies();
            }
            if (GUILayout.Button("Collect Default Dependencies"))
            {
                this.assetReferenceResolver.CollectDefaultDependencies();
            }
            if (GUILayout.Button("Collect All Project Dependencies"))
            {
                this.assetReferenceResolver.CollectAllProjectDependencies();
            }
        }

    }

}