using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraMotionBlur))]
public class CameraMotionBlurEditor : Editor
{
	SerializedObject serObj;

	SerializedProperty filterType;
	SerializedProperty preview;
	SerializedProperty previewScale;
	SerializedProperty movementScale;
	SerializedProperty jitter;
	SerializedProperty rotationScale;
	SerializedProperty maxVelocity;
	SerializedProperty minVelocity;

	// [af] Unused private member.
	//SerializedProperty maxNumSamples;

	SerializedProperty velocityScale;
	SerializedProperty velocityDownsample;
	SerializedProperty noiseTexture;
	SerializedProperty showVelocity;
	SerializedProperty showVelocityScale;
	SerializedProperty excludeLayers;
	//var dynamicLayers : SerializedProperty; // [af] Commented in JS code.

	void OnEnable()
	{
		this.serObj = new SerializedObject(this.target);

		this.filterType = this.serObj.FindProperty("filterType");

		this.preview = this.serObj.FindProperty("preview");
		this.previewScale = this.serObj.FindProperty("previewScale");

		this.movementScale = this.serObj.FindProperty("movementScale");
		this.rotationScale = this.serObj.FindProperty("rotationScale");

		this.maxVelocity = this.serObj.FindProperty("maxVelocity");
		this.minVelocity = this.serObj.FindProperty("minVelocity");

		// [af] Unused private member.
		//this.maxNumSamples = this.serObj.FindProperty("maxNumSamples");

		this.jitter = this.serObj.FindProperty("jitter");

		this.excludeLayers = this.serObj.FindProperty("excludeLayers");

		// [af] Commented in JS code.
		//dynamicLayers = this.serObj.FindProperty ("dynamicLayers");

		this.velocityScale = this.serObj.FindProperty("velocityScale");
		this.velocityDownsample = this.serObj.FindProperty("velocityDownsample");

		this.noiseTexture = this.serObj.FindProperty("noiseTexture");
	}

	public override void OnInspectorGUI()
	{
		this.serObj.Update();

		EditorGUILayout.LabelField("Simulates camera based motion blur", EditorStyles.miniLabel);

		EditorGUILayout.PropertyField(this.filterType, new GUIContent("Technique"));
		if ((this.filterType.enumValueIndex == 3) && !(this.target as CameraMotionBlur).Dx11Support())
		{
			EditorGUILayout.HelpBox("DX11 mode not supported (need shader model 5)", MessageType.Info);
		}

		EditorGUILayout.PropertyField(this.velocityScale, new GUIContent(" Velocity Scale"));
		if (this.filterType.enumValueIndex >= 2)
		{
			EditorGUILayout.LabelField(" Tile size used during reconstruction filter:", EditorStyles.miniLabel);
			EditorGUILayout.PropertyField(this.maxVelocity, new GUIContent("  Velocity Max"));
		}
		else
		{
			EditorGUILayout.PropertyField(this.maxVelocity, new GUIContent(" Velocity Max"));
		}

		EditorGUILayout.PropertyField(this.minVelocity, new GUIContent(" Velocity Min"));

		EditorGUILayout.Separator();

		EditorGUILayout.LabelField("Technique Specific");

		if (this.filterType.enumValueIndex == 0)
		{
			// portal style motion blur
			EditorGUILayout.PropertyField(this.rotationScale, new GUIContent(" Camera Rotation"));
			EditorGUILayout.PropertyField(this.movementScale, new GUIContent(" Camera Movement"));
		}
		else
		{
			// "plausible" blur or cheap, local blur
			EditorGUILayout.PropertyField(this.excludeLayers, new GUIContent(" Exclude Layers"));
			EditorGUILayout.PropertyField(this.velocityDownsample, new GUIContent(" Velocity Downsample"));
			this.velocityDownsample.intValue = (this.velocityDownsample.intValue < 1) ?
				1 : this.velocityDownsample.intValue;

			if (this.filterType.enumValueIndex >= 2)
			{
				// only display jitter for reconstruction
				EditorGUILayout.PropertyField(this.noiseTexture, new GUIContent(" Sample Jitter"));
				EditorGUILayout.PropertyField(this.jitter, new GUIContent("  Jitter Strength"));
			}
		}

		EditorGUILayout.Separator();

		EditorGUILayout.PropertyField(this.preview, new GUIContent("Preview"));

		if (this.preview.boolValue)
		{
			EditorGUILayout.PropertyField(this.previewScale, new GUIContent(string.Empty));
		}

		this.serObj.ApplyModifiedProperties();
	}
}
