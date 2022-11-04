using MaidDereMinigame.Malee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    [CreateAssetMenu(fileName = "New Food Item", menuName = "Food")]
    public class Food : ScriptableObject
    {
        public Sprite largeSprite;
        public Sprite smallSprite;
        public float cookTimeMultiplier = 1f;
    }

    [System.Serializable]
    public class Foods : ReorderableArray<Food> { }
}