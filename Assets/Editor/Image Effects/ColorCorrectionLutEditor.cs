using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorCorrectionLut))]
public class ColorCorrectionLutEditor : Editor
{
	public SerializedObject serObj;

	void OnEnable()
	{
		this.serObj = new SerializedObject(this.target);
	}

	private Texture2D tempClutTex2D;

	public override void OnInspectorGUI()
	{
		this.serObj.Update();

		EditorGUILayout.LabelField("Converts textures into color lookup volumes (for grading)",
			EditorStyles.miniLabel);

		//EditorGUILayout.LabelField("Change Lookup Texture (LUT):");
		//EditorGUILayout.BeginHorizontal ();
		//var r : Rect = GUILayoutUtility.GetAspectRect(1.0f);

		Rect r;
		Texture2D t;

		//EditorGUILayout.Space();
		this.tempClutTex2D = EditorGUILayout.ObjectField(
			" Based on", this.tempClutTex2D, typeof(Texture2D), false) as Texture2D;

		if (this.tempClutTex2D == null)
		{
			t = AssetDatabase.LoadMainAssetAtPath((this.target as ColorCorrectionLut).basedOnTempTex) as Texture2D;

			if (t)
			{
				this.tempClutTex2D = t;
			}
		}

		Texture2D tex = this.tempClutTex2D;

		if (tex && ((this.target as ColorCorrectionLut).basedOnTempTex != AssetDatabase.GetAssetPath(tex)))
		{
			EditorGUILayout.Separator();
			if (!(this.target as ColorCorrectionLut).ValidDimensions(tex))
			{
				EditorGUILayout.HelpBox("Invalid texture dimensions!\nPick another texture or adjust dimension to e.g. 256x16.",
					MessageType.Warning);
			}
			else if (GUILayout.Button("Convert and Apply"))
			{
				string path = AssetDatabase.GetAssetPath(tex);
				TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
				bool doImport = false;

				if (!textureImporter.isReadable)
				{
					doImport = true;
				}

				if (textureImporter.mipmapEnabled)
				{
					doImport = true;
				}

				// [af] Changed "textureFormat" to "textureCompression" for Unity 5.
				if (textureImporter.textureCompression != TextureImporterCompression.Uncompressed)
				{
					doImport = true;
				}

				if (doImport)
				{
					textureImporter.isReadable = true;
					textureImporter.mipmapEnabled = false;
					textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
					AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
					//tex = AssetDatabase.LoadMainAssetAtPath(path);  
				}

			  (this.target as ColorCorrectionLut).Convert(tex, path);
			}
		}

		if ((this.target as ColorCorrectionLut).basedOnTempTex != string.Empty)
		{
			EditorGUILayout.HelpBox("Using " + (this.target as ColorCorrectionLut).basedOnTempTex, MessageType.Info);
			t = AssetDatabase.LoadMainAssetAtPath((this.target as ColorCorrectionLut).basedOnTempTex) as Texture2D;
			if (t)
			{
				r = GUILayoutUtility.GetLastRect();
				r = GUILayoutUtility.GetRect(r.width, 20);
				r.x += r.width * 0.05f / 2.0f;
				r.width *= 0.95f;
				GUI.DrawTexture(r, t);
				GUILayoutUtility.GetRect(r.width, 4);
			}
		}

		//EditorGUILayout.EndHorizontal ();    

		this.serObj.ApplyModifiedProperties();
	}
}
