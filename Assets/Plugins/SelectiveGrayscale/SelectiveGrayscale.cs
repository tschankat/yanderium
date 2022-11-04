using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class SelectiveGrayscale : MonoBehaviour
{
	public Color filterColor = Color.red;
	[Range(0,1)] public float desaturation;
	[Range(0,1)] public float sensitivity;
	[Range(0,1)] public float tolerance;

	public Material mat;

	void OnRenderImage(RenderTexture source, RenderTexture destination){
		mat.SetColor("_FilterColor", filterColor);
		mat.SetFloat("_Desaturation", desaturation);
		mat.SetFloat("_Sensitivity", sensitivity);
		mat.SetFloat("_Tolerance", tolerance);
		Graphics.Blit(source, destination, mat);
	}
}