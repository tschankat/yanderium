using UnityEngine;
using UnityEditor;
using System.Collections;
using HighlightingSystem;

[CustomEditor(typeof(HighlightingBlitter))]
public class HighlightingBlitterEditor : Editor
{
	protected HighlightingBlitter hb;
	
	// 	
	protected virtual void OnEnable()
	{
		hb = target as HighlightingBlitter;

		// Try to find HighlightingRenderer component in case it is not assigned
		if (hb.highlightingRenderer == null)
		{
			hb.highlightingRenderer = hb.GetComponent<HighlightingRenderer>();
		}
	}

	// 
	public override void OnInspectorGUI()
	{
		if (hb.highlightingRenderer == null)
		{
			EditorGUILayout.HelpBox("Please assign HighlightingRenderer component or remove this component if you use mobile version (HighlightingMobile component) of the Highlighting System.", MessageType.Error);
			hb.enabled = false;
		}
		else
		{
			EditorGUILayout.HelpBox("Use order of this component (relatively to other Image Effects applied to this camera) to control the point at which highlighting will be applied to the framebuffer. Click on a little gear icon to the right and choose Move Up / Move Down.", MessageType.Info);
			hb.enabled = hb.highlightingRenderer.enabled;
		}

		hb.highlightingRenderer = (HighlightingRenderer)EditorGUILayout.ObjectField("Highlighting Renderer", hb.highlightingRenderer, typeof(HighlightingRenderer), true);
	}
}
