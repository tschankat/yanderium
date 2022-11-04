using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using HighlightingSystem;

public class HighlightingBaseEditor : Editor
{
	protected string[] downsampleOptions = new string[] { "None", "Half", "Quarter" };
	
	protected HighlightingBase hb;

	// 
	protected virtual void OnEnable()
	{
		hb = target as HighlightingBase;
		hb.CheckInstance();
	}

	// 
	protected void CommonGUI()
	{
		EditorGUILayout.HelpBox("This component should be always on top of all other Image Effects applied to this camera.", MessageType.Warning);

		#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
		if (!Handheld.use32BitDisplayBuffer)
		{
			EditorGUILayout.HelpBox("Highlighting System requires 32-bit display buffer. Set the 'Use 32-bit Display Buffer' checkbox under the 'Resolution and Presentation' section of Player Settings.", MessageType.Error);
		}
		#endif

		EditorGUILayout.HelpBox("Depth Offset properties should be used only when Dynamic Batching is enabled in Player Settings. Otherwise set them to 0's to avoid rendering artifacts.", MessageType.Info);
		hb.offsetFactor = EditorGUILayout.Slider("Depth Offset Factor:", hb.offsetFactor, -1f, 0f);
		hb.offsetUnits = EditorGUILayout.Slider("Depth Offset Units:", hb.offsetUnits, -100f, 0f);

		EditorGUILayout.LabelField("Preset:");
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Default"))
		{
			hb.downsampleFactor = 2;
			hb.iterations = 2;
			hb.blurMinSpread = 0.65f;
			hb.blurSpread = 0.25f;
			hb.blurIntensity = 0.3f;
		}
		if (GUILayout.Button("Strong"))
		{
			hb.downsampleFactor = 2;
			hb.iterations = 2;
			hb.blurMinSpread = 0.5f;
			hb.blurSpread = 0.15f;
			hb.blurIntensity = 0.325f;
		}
		if (GUILayout.Button("Speed"))
		{
			hb.downsampleFactor = 2;
			hb.iterations = 1;
			hb.blurMinSpread = 0.75f;
			hb.blurSpread = 0.0f;
			hb.blurIntensity = 0.35f;
		}
		if (GUILayout.Button("Quality"))
		{
			hb.downsampleFactor = 1;
			hb.iterations = 3;
			hb.blurMinSpread = 0.5f;
			hb.blurSpread = 0.5f;
			hb.blurIntensity = 0.28f;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		hb.downsampleFactor = EditorGUILayout.Popup("Downsampling:", hb.downsampleFactor, downsampleOptions);
		hb.iterations = Mathf.Clamp(EditorGUILayout.IntField("Iterations:", hb.iterations), 0, 50);
		hb.blurMinSpread = EditorGUILayout.Slider("Min Spread:", hb.blurMinSpread, 0f, 3f);
		hb.blurSpread = EditorGUILayout.Slider("Spread:", hb.blurSpread, 0f, 3f);
		hb.blurIntensity = EditorGUILayout.Slider("Intensity:", hb.blurIntensity, 0f, 1f);
	}
}
