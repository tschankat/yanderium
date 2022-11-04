using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MaidDereMinigame
{
    [CustomEditor(typeof(FoodMenu))]
    public class FoodMenuEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Set Sprites"))
            {
                FoodMenu menu = (FoodMenu)target;
                Undo.RecordObject(menu, "Set Icons");
                menu.SetMenuIcons();
            }
        }
    }
}