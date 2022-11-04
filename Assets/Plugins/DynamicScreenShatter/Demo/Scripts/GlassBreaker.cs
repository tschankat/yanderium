using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshRenderer))]
public class GlassBreaker : MonoBehaviour {

    /// <summary>
    /// a List of the glass breaking sound effects to play from
    /// </summary>
    public List<AudioClip> BreakSounds = new List<AudioClip>();
    
    /// <summary>
    /// a List of textures to cycle through as backgrounds
    /// </summary>
    public List<Texture2D> Textures = new List<Texture2D>();

    /// <summary>
    /// The GameObject with the ShatterSpawner script attached
    /// </summary>
    public GameObject ShatterPrefab;

    private AudioSource audioSource;
    private MeshRenderer meshRenderer;

    private Texture currentTexture;

    private GameObject lastCreatedGameObject;
    
    /// <summary>
    /// The index of the current texture
    /// </summary>
    private int currentTextureIndex = 0;

    // Use this for initialization
    void Start ()
	{
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        ResizeQuad();
    }

    private void ResizeQuad()
    {

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        var tf = gameObject.transform;

        tf.Translate(-tf.position);

        // Stretch out the quad to fill our screen
        tf.localScale = new Vector3(width, height, 1);

        // Assign the quad a random texture
        meshRenderer.materials[0].mainTexture = currentTexture = GetNextTexture();
    }
    
    private Texture2D GetNextTexture()
    {
        return Textures[++currentTextureIndex % Textures.Count];
    }

    private AudioClip GetNextSound()
    {
        return BreakSounds[Random.Range(0, BreakSounds.Count)];
    }

    // Update is called once per frame
    void Update ()
	{
        if (Input.GetMouseButtonDown(0))
        {
            // Play the sound effect
            audioSource.clip = GetNextSound();
            audioSource.Play();

            // Delete the old object if it exists
            if (lastCreatedGameObject != null)
                Destroy(lastCreatedGameObject);

            // Create a new shatter
            var shatter = Instantiate(ShatterPrefab);

            var shatterScript = shatter.GetComponent<ShatterSpawner>();

            // Set the texture to the current texture
            shatterScript.ScreenMaterial.mainTexture = currentTexture;

            // Set shatter origin to where the screen was clicked
            shatterScript.ShatterOrigin = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // Update the quad's texture
            meshRenderer.materials[0].mainTexture = currentTexture = GetNextTexture();

            lastCreatedGameObject = shatter;

			gameObject.SetActive(false);
        }
    }
}
