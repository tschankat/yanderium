using UnityEngine;
using UnityEditor;
using System.Collections;
using HighlightingSystem;

[CustomEditor(typeof(HighlightingMobile))]
public class HighlightingMobileEditor : HighlightingBaseEditor
{
	// 
	public override void OnInspectorGUI()
	{
		EditorGUILayout.HelpBox("This component is hardly optimized for mobile devices and only partially compatible with other Image Effects. Use HighlightingRenderer and HighlightingBlitter components instead (by trading out the performance) if you have problems using it in combination with other Image Effects.", MessageType.Info);
		CommonGUI();
	}
}