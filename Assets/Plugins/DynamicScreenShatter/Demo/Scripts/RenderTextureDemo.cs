using UnityEngine;
using System.Collections;

public class RenderTextureDemo : MonoBehaviour
{

    public RenderTexture renderTexture;
    public GameObject shatterPrefab;

	// Use this for initialization
	void Start ()
	{
        // Create the shatter
	    var shatter = Instantiate(shatterPrefab);

        // Get the ShatterSpawner Component
	    var shatterScript = shatter.GetComponent<ShatterSpawner>();

	    shatterScript.MaxPlayTime = 0f;

        // Randomize the shatter origin
	    shatterScript.RandomizeShatterOrigin = true;

        // Make the background opaque and set the renderTexture as the material texture
	    shatterScript.ClearCamera = true;
        shatterScript.BackgroundColor = Color.black;
	    shatterScript.ScreenMaterial.mainTexture = renderTexture;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
