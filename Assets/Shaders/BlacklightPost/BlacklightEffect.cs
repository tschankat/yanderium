using UnityEngine;

[ExecuteInEditMode]
public class BlacklightEffect : MonoBehaviour
{
    [SerializeField] private Color fogColorDark = new Color32(14, 11, 31, 255);
    [SerializeField] private Color fogColorLight = new Color32(87, 89, 111, 255);
    [SerializeField] [Range(0, 1)] private float fogOpacity = 1;
    [SerializeField] private float fogDepth = 8;

    [Space(5)]
    [Header("Glow")]

    [SerializeField] [ColorUsage(true, true, 0, 3, 0, 3)] private Color glowColor = new Color(0, 123f/255, 191f/255)*9;
    [SerializeField] [ColorUsage(true, true, 0, 3, 0, 3)] private Color glowColorSecondary = new Color(191f/255, 0, 173f/255)*9;
    [SerializeField] private float glowBias = 13;
    [SerializeField] [Range(0f, 1f)] private float glowFlip = 0;
    [SerializeField] private bool glow = true;

    [Space(5)]
    [Header("Targetted highlighting")]
    [SerializeField] private HighlightTarget[] highlightTargets;
    [SerializeField] [Range(0f, 1f)] private float smoothDropoff = 0;

    [Space(5)]
    [Header("Edge")]

    [SerializeField] private Color edgeColor = new Color32(7, 255, 83, 255);
    [SerializeField] [Range(0.01f, 1f)] private float threshold = 0.45f;
    [SerializeField] [Range(0f, 1f)] private float edgeOpacity = 1f;

    [Space(5)]
    [Header("Overlay")]

    [SerializeField] private Color overlayTop = new Color32(233, 0, 255, 255);
    [SerializeField] private Color overlayBottom = new Color32(0, 38, 255, 255);
    [SerializeField] [Range(0f, 1f)] private float overlayOpacity = 0.06f;

    private Color[] hTargets = new Color[100];
    private float[] hThresholds = new float[100];
    private Color[] hColors = new Color[100];
    private float[] hColorInterpolations = new float[100];

    private Camera camera;
    private Material post;

    void Update()
    {
        if(camera!=null) camera.depthTextureMode = DepthTextureMode.Depth | DepthTextureMode.DepthNormals;
        if(post != null)
        {
            // Set generic properties
            post.SetFloat("_DepthDistance", fogDepth);
            post.SetColor("_FogColorDark", fogColorDark);
            post.SetColor("_FogColorLight", fogColorLight);
            post.SetFloat("_FogOpacity", fogOpacity);
            post.SetFloat("_GlowBias", glowBias);
            post.SetColor("_GlowColor", glowColor);
            post.SetColor("_GlowColor2", glowColorSecondary);
            post.SetFloat("_GlowAmount", glow ? 1 : 0);
            post.SetColor("_EdgeColor", edgeColor);
            post.SetFloat("_EdgeThreshold", threshold);
            post.SetFloat("_EdgeStrength", edgeOpacity);
            post.SetColor("_OverlayTop", overlayTop);
            post.SetColor("_OverlayBottom", overlayBottom);
            post.SetFloat("_OverlayOpacity", overlayOpacity);
            post.SetFloat("_HighlightFlip", glowFlip);
            post.SetFloat("_HighlightTargetSmooth", smoothDropoff);

            // Grab highlight targets and thresholds
            if (highlightTargets != null)
            {
                for (int i = 0; i < highlightTargets.Length; i++)
                {
                    hTargets[i] = highlightTargets[i].TargetColor;
                    hThresholds[i] = highlightTargets[i].Threshold;
                    hColors[i] = highlightTargets[i].ReplacementColor;
                    hColorInterpolations[i] = highlightTargets[i].SmoothColorInterpolation;
                    /*if (highlightTargets!=null && highlightTargets.Length > i) hTargets[i] = highlightTargets[i];
                    if (highlightThresholds!=null && highlightThresholds.Length > i) hThresholds[i] = highlightThresholds[i];
                    if (highlightColors != null && highlightColors.Length > i) hColors[i] = highlightColors[i];*/
                }
            }

            // Set highlight targets
            if (highlightTargets != null && highlightTargets.Length != 0) post.SetInt("_HighlightTargetsLength", Mathf.Clamp(highlightTargets.Length, 0, 100));
            post.SetColorArray("_HighlightTargets", hTargets);

            // Set highlight thresholds
            post.SetFloatArray("_HighlightTargetThresholds", hThresholds);

            // Set highlight colors
            post.SetColorArray("_HighlightColors", hColors);

            // Set highlight color interpolations
            post.SetFloatArray("_SmoothColorInterpolations", hColorInterpolations);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (camera == null) camera = GetComponent<Camera>();
        else
        {
            if (post == null) post = new Material(Shader.Find("Abcight/BlacklightPost"));
            Graphics.Blit(source, destination, post);
        }
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        DestroyImmediate(post);
        post = null;
    }
}

[System.Serializable]
public struct HighlightTarget
{
    public Color TargetColor;
    [ColorUsage(true, true, 0, 3, 0, 3)] public Color ReplacementColor;
    [Range(0f, 1f)] public float Threshold;
    [Range(0f, 1f)] public float SmoothColorInterpolation;
}