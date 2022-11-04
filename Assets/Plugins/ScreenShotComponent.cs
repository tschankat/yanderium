using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotComponent : MonoBehaviour {
/*
    public float CameraLength = 0.5f;
    public float CameraOrthoFOV = 0.2f;
    public float CameraHeight = 1.5f;
    public int TextureWidth = 512;
    public int TextureHeight = 512;

    private GameObject CameraObject;
    private Camera CameraComponent;

    #region "Deprecate ASAP"
    private bool StillProcessing = false;
    private bool ReadyForScreenshot = false;
    private Transform backupCameraTransform;
    private bool backupOrthographic;
    private float backupOrthographicSize;
    private string ScreenshotPath;
    private int ResMultiplier = 1;
    #endregion

    void Start ()
    {
        CameraObject = new GameObject("TextureCamera");
        CameraComponent = CameraObject.AddComponent<Camera>();
        CameraComponent.enabled = false;
	}

    private void OnDestroy()
    {
        if (CameraObject != null)
        {
            Destroy(CameraObject);
        }
    }

    void Update()
    {
        #region "Deprecate ASAP"
        if (StillProcessing)
        {
            //Screenshot is taken, but not cropped into 512x512
            if (System.IO.File.Exists(ScreenshotPath))
            {
                Texture2D Screenshot = new Texture2D(1280,720);
                Screenshot.LoadImage(System.IO.File.ReadAllBytes(ScreenshotPath));

                int LeftOffset = (Screenshot.width - 512) / 2;
                int TopOffset = (Screenshot.height - 512) / 2;

                Texture2D CroppedShot = new Texture2D(512, 512);
                Color[] CroppedSelection = Screenshot.GetPixels(LeftOffset, TopOffset, 512, 512);
                CroppedShot.SetPixels(CroppedSelection);
                CroppedShot.Apply();

                byte[] PNGBlob = CroppedShot.EncodeToPNG();
                System.IO.File.WriteAllBytes(ScreenshotPath, PNGBlob);

                Camera.main.transform.position = backupCameraTransform.position;
                Camera.main.transform.rotation = backupCameraTransform.rotation;
                Camera.main.orthographic = backupOrthographic;
                Camera.main.orthographicSize = backupOrthographicSize;

                StillProcessing = false;
            }
        }
        #endregion
    }

    void LateUpdate ()
    {
        #region "Debug Key Bindings"
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeScreenshotFromDisplay();
        }
        #endregion

        #region "Deprecate ASAP"
        if (ReadyForScreenshot)
        {
            ReadyForScreenshot = false;

            string PathFolder = Application.dataPath + "/StreamingAssets";

            int CurrentFile = 1;
            while (System.IO.File.Exists(PathFolder + "/RandomPortrait_" + CurrentFile + ".png"))
            {
                CurrentFile += 1;
            }

            ScreenshotPath = PathFolder + "/RandomPortrait_" + CurrentFile + ".png";

            ScreenCapture.CaptureScreenshot(ScreenshotPath, ResMultiplier);

            if (Camera.main.transform.gameObject != null &&
            Camera.main.transform.gameObject.GetComponent<RPG_Camera>() != null)
            {
                Camera.main.transform.gameObject.GetComponent<RPG_Camera>().enabled = true;
            }
        }
        #endregion
    }

    #region "Deprecate ASAP"
    /**
     * Pulls a 512x512 texture from framebuffer. For vertical resolutions less than 512px,
     * Application.CaptureScreenshot superSize is 2.
     * 
     * @returns true if screenshot is queued. false if prior shot is incomplete; try again later.
     * 
     * FIXME: (Scott Michaud) TakeScreenshot() works in Unity 5.6 / volunteer project, but not
     * Unity 4.x / Yandere Simulator. This workaround rips from display framebuffer.
     */

     /*
    public bool TakeScreenshotFromDisplay()
    {

        if (!IsReadyToScreenshotDisplay())
        {
            return false;
        }

        //Step: Lock out future screenshots until we're done with this one.
        StillProcessing = true;

        //Step: Grab state to restore (ex: camera previous location).
        backupCameraTransform = Camera.main.transform;
        backupOrthographic = Camera.main.orthographic;
        backupOrthographicSize = Camera.main.orthographicSize;

        //Lock out RPG_Camera from messing with things
        if (Camera.main.transform.gameObject != null &&
            Camera.main.transform.gameObject.GetComponent<RPG_Camera>() != null)
        {
            Camera.main.transform.gameObject.GetComponent<RPG_Camera>().enabled = false;
        }

        //Step: Move camera to target
        GameObject CameraTarget = transform.gameObject;

        Vector3 FacePosition = CameraTarget.transform.position;
        FacePosition.y += CameraHeight;

        Vector3 CameraPosition = FacePosition + (CameraTarget.transform.forward * CameraLength);

        Camera.main.transform.position = CameraPosition;
        Camera.main.transform.LookAt(FacePosition);

        //Step: Figure out what orthosize is necessary to get the correct 512x512.
		int ResWidth = 1280;
		int ResHeight = 720;

        ResMultiplier = (ResHeight < 512) ? 2 : 1; //YS doesn't allow width < 640
        float OrthoMultiplier = (ResHeight * ResMultiplier) / 512.0f;

        Camera.main.orthographic = true;
        Camera.main.orthographicSize = CameraOrthoFOV * OrthoMultiplier;

        ReadyForScreenshot = true;

        //Step: Signal queue was successful
        return true;
    }

    public bool IsReadyToScreenshotDisplay()
    {
        if (StillProcessing)
        {
            return false;
        }

        return true;
    }
    #endregion

    public void TakeScreenshot()
    {
        if (CameraObject == null || CameraComponent == null)
        {
            Debug.LogError("ScreenShotComponent: Cannot access TextureCamera.");
            return;
        }

        RenderTexture CameraFrameBuffer = new RenderTexture(TextureWidth, TextureHeight, 0, RenderTextureFormat.Default);
        CameraComponent.targetTexture = CameraFrameBuffer;
        RenderTexture.active = CameraComponent.targetTexture;

        CameraComponent.orthographic = true;
        CameraComponent.orthographicSize = CameraOrthoFOV;
        CameraComponent.useOcclusionCulling = false;

        GameObject CameraTarget = transform.gameObject;

        Vector3 FacePosition = CameraTarget.transform.position;
        FacePosition.y += CameraHeight;

        Vector3 CameraPosition = FacePosition + (CameraTarget.transform.forward * CameraLength);

        CameraObject.transform.position = CameraPosition;
        CameraObject.transform.LookAt(FacePosition);

        CameraComponent.Render();

        Texture2D RenderOutput = new Texture2D(CameraFrameBuffer.width, CameraFrameBuffer.height, TextureFormat.RGB24, false);
        RenderOutput.ReadPixels(new Rect(0, 0, CameraFrameBuffer.width, CameraFrameBuffer.height), 0, 0);
        RenderOutput.Apply();

        RenderTexture.active = CameraFrameBuffer;

        string PathFolder = Application.dataPath + "/StreamingAssets";

        int CurrentFile = 1;
        while (System.IO.File.Exists(PathFolder + "/RandomPortrait_" + CurrentFile + ".png"))
        {
            CurrentFile += 1;
        }

        byte[] PNGBlob = RenderOutput.EncodeToPNG();

        System.IO.File.WriteAllBytes(PathFolder + "/RandomPortrait_" + CurrentFile + ".png", PNGBlob);
    }
    */
}
