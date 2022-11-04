using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SunShafts))]
public class SunShaftsEditor : Editor
{
	public SerializedObject serObj;

	public SerializedProperty sunTransform;
	public SerializedProperty radialBlurIterations;
	public SerializedProperty sunColor;
	public SerializedProperty sunShaftBlurRadius;
	public SerializedProperty sunShaftIntensity;
	public SerializedProperty useSkyBoxAlpha;
	public SerializedProperty useDepthTexture;
	public SerializedProperty resolution;
	public SerializedProperty screenBlendMode;
	public SerializedProperty maxRadius;

	void OnEnable()
	{
		this.serObj = new SerializedObject(this.target);

		this.screenBlendMode = this.serObj.FindProperty("screenBlendMode");

		this.sunTransform = this.serObj.FindProperty("sunTransform");
		this.sunColor = this.serObj.FindProperty("sunColor");

		this.sunShaftBlurRadius = this.serObj.FindProperty("sunShaftBlurRadius");
		this.radialBlurIterations = this.serObj.FindProperty("radialBlurIterations");

		this.sunShaftIntensity = this.serObj.FindProperty("sunShaftIntensity");
		this.useSkyBoxAlpha = this.serObj.FindProperty("useSkyBoxAlpha");

		this.resolution = this.serObj.FindProperty("resolution");

		this.maxRadius = this.serObj.FindProperty("maxRadius");

		this.useDepthTexture = this.serObj.FindProperty("useDepthTexture");
	}

	public override void OnInspectorGUI()
	{
		this.serObj.Update();

		EditorGUILayout.BeginHorizontal();

		bool oldVal = this.useDepthTexture.boolValue;
		EditorGUILayout.PropertyField(this.useDepthTexture, new GUIContent("Rely on Z Buffer?"));
		if ((this.target as SunShafts).GetComponent<Camera>())
			GUILayout.Label("Current camera mode: " + (this.target as SunShafts).GetComponent<Camera>().depthTextureMode,
				EditorStyles.miniBoldLabel);

		EditorGUILayout.EndHorizontal();

		// depth buffer need
		/*
		var newVal : boolean = useDepthTexture.boolValue;
		if (newVal != oldVal) {
			if(newVal)
				(target as SunShafts).camera.depthTextureMode |= DepthTextureMode.Depth;
			else
				(target as SunShafts).camera.depthTextureMode &= ~DepthTextureMode.Depth;
		}
		*/

		EditorGUILayout.PropertyField(this.resolution, new GUIContent("Resolution"));
		EditorGUILayout.PropertyField(this.screenBlendMode, new GUIContent("Blend mode"));

		EditorGUILayout.Separator();

		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.PropertyField(this.sunTransform,
			new GUIContent("Shafts caster", "Chose a transform that acts as a root point for the produced sun shafts"));
		if ((this.target as SunShafts).sunTransform && (this.target as SunShafts).GetComponent<Camera>())
		{
			if (GUILayout.Button("Center on " + (this.target as SunShafts).GetComponent<Camera>().name))
			{
				if (EditorUtility.DisplayDialog("Move sun shafts source?",
					"The SunShafts caster named " + (this.target as SunShafts).sunTransform.name +
					"\n will be centered along " + (this.target as SunShafts).GetComponent<Camera>().name +
					". Are you sure? ", "Please do", "Don't"))
				{
					Ray ray = (this.target as SunShafts).GetComponent<Camera>().ViewportPointToRay(new Vector3(0.50f, 0.50f, 0.0f));
					(this.target as SunShafts).sunTransform.position = ray.origin + (ray.direction * 500.0f);
					(this.target as SunShafts).sunTransform.LookAt((this.target as SunShafts).transform);
				}
			}
		}

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Separator();

		EditorGUILayout.PropertyField(sunColor, new GUIContent("Shafts color"));
		this.maxRadius.floatValue = 1.0f - EditorGUILayout.Slider(
			"Distance falloff", 1.0f - this.maxRadius.floatValue, 0.10f, 1.0f);

		EditorGUILayout.Separator();

		this.sunShaftBlurRadius.floatValue = EditorGUILayout.Slider(
			"Blur size", this.sunShaftBlurRadius.floatValue, 1.0f, 10.0f);
		this.radialBlurIterations.intValue = EditorGUILayout.IntSlider(
			"Blur iterations", this.radialBlurIterations.intValue, 1, 3);

		EditorGUILayout.Separator();

		EditorGUILayout.PropertyField(this.sunShaftIntensity, new GUIContent("Intensity"));
		this.useSkyBoxAlpha.floatValue = EditorGUILayout.Slider(
			"Use alpha mask", this.useSkyBoxAlpha.floatValue, 0.0f, 1.0f);

		this.serObj.ApplyModifiedProperties();
	}
}
