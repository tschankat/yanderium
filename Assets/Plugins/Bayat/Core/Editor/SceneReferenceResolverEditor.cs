using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityObject = UnityEngine.Object;

using Bayat.Core.EditorWindows;

namespace Bayat.Core
{

    [CustomEditor(typeof(SceneReferenceResolver), true)]
    public class SceneReferenceResolverEditor : Editor
    {

        protected SceneReferenceResolver sceneReferenceResolver;
        protected bool foldout = false;

        private void OnEnable()
        {
            this.sceneReferenceResolver = (SceneReferenceResolver)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (sceneReferenceResolver == null)
            {
                return;
            }

            var availableDependencies = sceneReferenceResolver.AvailableDependencies;
            var currentDependencies = sceneReferenceResolver.SceneDependencies;
            GUILayout.Label(string.Format("Available: {0}", availableDependencies.Count));
            GUILayout.Label(string.Format("Current: {0}", currentDependencies.Count));
            if (availableDependencies.Count > 0)
            {
                EditorGUILayout.HelpBox(string.Format("There are {0} more dependencies available to reference.", availableDependencies.Count), MessageType.Warning);
            }
            if (GUILayout.Button("Open Reference Manager"))
            {
                new SceneReferenceManagerWindow().Show();
            }
            if (GUILayout.Button("Collect Scene Dependencies"))
            {
                this.sceneReferenceResolver.CollectSceneDependencies();
            }
            if (GUILayout.Button("Refresh Available Dependencies"))
            {
                this.sceneReferenceResolver.GetAvailableDependencies();
            }
            if (this.sceneReferenceResolver.HasNullReferences())
            {
                EditorGUILayout.HelpBox("There are null references in the scene dependencies, remove them to stop causing further issues and errors.", MessageType.Warning);
                if (GUILayout.Button("Remove Null References"))
                {
                    this.sceneReferenceResolver.RemoveNullReferences();
                }
            }
        }

        public List<UnityObject> GetAvailableDependencies()
        {
            var sceneDependencies = new List<UnityObject>();
            var sceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            var dependencies = EditorUtility.CollectDependencies(sceneObjects);
            foreach (var dependency in dependencies)
            {
                if (EditorUtility.IsPersistent(dependency))
                {
                    continue;
                }
                sceneDependencies.Add(dependency);
            }
            return sceneDependencies;
        }

        public UnityObject[] GetAvailableHierarchyDependencies()
        {
            var sceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            return EditorUtility.CollectDeepHierarchy(sceneObjects);
        }

    }

}