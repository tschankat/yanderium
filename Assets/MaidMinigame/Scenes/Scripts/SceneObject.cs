using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Scene Object", menuName = "Scenes/New Scene Object")]
    public class SceneObject : ScriptableObject
    {
        public int sceneBuildNumber = -1;
    }
}