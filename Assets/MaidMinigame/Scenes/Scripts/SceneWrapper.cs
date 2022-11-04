using MaidDereMinigame.Malee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidDereMinigame
{
    [CreateAssetMenu(fileName = "New Scene Wrapper", menuName = "Scenes/New Scene Wrapper")]
    public class SceneWrapper : ScriptableObject
    {
        [Reorderable]
        public SceneObjectMetaData m_Scenes;

        public SceneObject GetSceneByBuildIndex(int buildIndex)
        {
            foreach (SceneObject sceneOb in m_Scenes)
                if (sceneOb.sceneBuildNumber == buildIndex)
                    return sceneOb;

            return null;
        }

        public SceneObject GetSceneByName(string name)
        {
            foreach (SceneObject sceneOb in m_Scenes)
                if (sceneOb.name == name)
                    return sceneOb;

            return null;
        }

        public static void LoadScene(SceneObject sceneObject)
        {
            GameController.Scenes.LoadLevel(sceneObject);
        }

        public void LoadLevel(SceneObject sceneObject)
        {
            int sceneIndex = -1;
            for (int i = 0; i < m_Scenes.Length; i++)
                if (m_Scenes[i] == sceneObject)
                    sceneIndex = m_Scenes[i].sceneBuildNumber;

            if (sceneIndex == -1)
                Debug.LogError("Scene could not be found. Is it in the Scene Wrapper?");
            else
                SceneManager.LoadScene(sceneIndex);
        }

        public int GetSceneID(SceneObject scene)
        {
            for (int i = 0; i < m_Scenes.Count; i++)
            {
                if (m_Scenes[i] == scene)
                    return i;
            }

            return -1;
        }

        public SceneObject GetSceneByIndex(int scene)
        {
            return m_Scenes[scene];
        }
    }

    [System.Serializable]
    public class SceneObjectMetaData : ReorderableArray<SceneObject> { }
}