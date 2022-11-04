using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;
//using UnityStandardAssets.ImageEffects;

public class DDRManager : MonoBehaviour
{
    //Non-serializables
    public GameState GameState;

	public YandereScript Yandere;
	public Transform FinishLocation;
	public Renderer OriginalRenderer;

	public GameObject OverlayCanvas;
	public GameObject GameUI;

    [Header("General")]
    public DDRLevel LoadedLevel;
    [SerializeField] private DDRLevel[] levels;
    [SerializeField] private InputManagerScript inputManager;
    [SerializeField] private DDRMinigame ddrMinigame;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform standPoint;
    [SerializeField] private float transitionSpeed = 2;

    [Header("Camera")]
    [SerializeField] private Transform minigameCamera;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform screenPoint;
    [SerializeField] private Transform watchPoint;

    [Header("Animation")]
    [SerializeField] private Animation machineScreenAnimation;
    [SerializeField] private Animation yandereAnim;

    [Header("UI")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private RawImage[] overlayImages;
    [SerializeField] private VideoPlayer backgroundVideo;
    [SerializeField] private Transform levelSelect;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private Text continueText;
    [SerializeField] private ColorCorrectionCurves gameplayColorCorrection;

    private Transform target;
    private bool booted;
    public bool DebugMode;
	public bool CheckingForEnd;

    private void Start()
    {
		minigameCamera.position = startPoint.position;

		if (DebugMode)
        {
			BeginMinigame();
		}

        //BeginMinigame(); //You should remove it from here and call it from somewhere else when the minigame should begin
    }

    public void Update()
    {
        minigameCamera.position = Vector3.Slerp(minigameCamera.position, target.position, transitionSpeed * Time.deltaTime);
        minigameCamera.rotation = Quaternion.Slerp(minigameCamera.rotation, target.rotation, transitionSpeed * Time.deltaTime);

        if (target == null) return;
        Vector3 destination = standPoint.position;
        if (LoadedLevel != null)
        {
            ddrMinigame.UpdateGame(audioSource.time);
            GameState.Health -= Time.deltaTime;
            GameState.Health = Mathf.Clamp(GameState.Health, 0, 100);

            #region Play animation
            if (inputManager.TappedLeft)
            {
                yandereAnim["f02_danceLeft_00"].time = 0;
                yandereAnim.Play("f02_danceLeft_00");
            }
            else if (inputManager.TappedDown)
            {
                yandereAnim["f02_danceDown_00"].time = 0;
                yandereAnim.Play("f02_danceDown_00");
            }
            if (inputManager.TappedRight)
            {
                yandereAnim["f02_danceRight_00"].time = 0;
                yandereAnim.Play("f02_danceRight_00");
            }
            else if (inputManager.TappedUp)
            {
                yandereAnim["f02_danceUp_00"].time = 0;
                yandereAnim.Play("f02_danceUp_00");
            }
            #endregion
        }
        yandereAnim.transform.position = Vector3.Lerp(yandereAnim.transform.position, destination, 10 * Time.deltaTime);

        if (CheckingForEnd)
        {
			if (!audioSource.isPlaying)
	        {
				OverlayCanvas.SetActive(false);
				GameUI.SetActive(false);

				CheckingForEnd = false;

				Debug.Log("End() was called because song ended.");

				StartCoroutine(End());
			}
		}

		if (GameState.Health <= 0)
        {
			if (audioSource.pitch < .01f)
			{
				OverlayCanvas.SetActive(false);
				GameUI.SetActive(false);

				if (audioSource.isPlaying)
				{
					Debug.Log("End() was called because we ranout of health.");

					StartCoroutine(End());
				}
			}
		}
    }

    public void BeginMinigame()
    {
    	Debug.Log("BeginMinigame() was called.");

		yandereAnim["f02_danceMachineIdle_00"].layer = 0;
        yandereAnim["f02_danceRight_00"].layer = 1;
        yandereAnim["f02_danceLeft_00"].layer = 2;
        yandereAnim["f02_danceUp_00"].layer = 1;
        yandereAnim["f02_danceDown_00"].layer = 2;

		yandereAnim["f02_danceMachineIdle_00"].weight = 1;
		yandereAnim["f02_danceRight_00"].weight = 1;
		yandereAnim["f02_danceLeft_00"].weight = 1;
		yandereAnim["f02_danceUp_00"].weight = 1;
		yandereAnim["f02_danceDown_00"].weight = 1;

		OverlayCanvas.SetActive(true);
		GameUI.SetActive(true);

        ddrMinigame.LoadLevelSelect(levels);
        StartCoroutine(minigameFlow());
    }
    
    public void BootOut()
    {
		minigameCamera.position = startPoint.position;

        StartCoroutine(fade(true, fadeImage, 5));
        target = startPoint;
		ddrMinigame.UnloadLevelSelect();

        ReturnToNormalGameplay();
    }

    IEnumerator minigameFlow()
    {
        levelSelect.gameObject.SetActive(true);
        defeatScreen.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
        //gameplayColorCorrection.enabled = false;
        audioSource.pitch = 1;

        target = screenPoint;
        #region Machine boot animation
        if (!booted)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            StartCoroutine(fade(false, fadeImage));
            while (fadeImage.color.a > 0.4f)
            {
                yield return null;
            }
            machineScreenAnimation.Play();
            booted = true;
        }
        #endregion

        #region Wait for player to pick the level
        yield return new WaitForEndOfFrame();
        while (Input.GetAxis(InputNames.Xbox_A) != 0) yield return null;
        while (LoadedLevel == null)
        {
            ddrMinigame.UpdateLevelSelect();
            yield return null;
        }
        ddrMinigame.LoadLevel(LoadedLevel);
        #endregion

        GameState = new GameState();

        yield return new WaitForSecondsRealtime(0.2f);
        transitionSpeed *= 2;
        target = watchPoint;

        backgroundVideo.Play();
        backgroundVideo.playbackSpeed = 0;

        StartCoroutine(fadeGameUI(true));
        backgroundVideo.playbackSpeed = 1;
        audioSource.clip = LoadedLevel.Song;
        audioSource.Play();
		CheckingForEnd = true;

        #region Wait for the minigame to finish
        while (audioSource.time < audioSource.clip.length)
        {
            if (GameState.Health <= 0)
            {
                GameState.FinishStatus = DDRFinishStatus.Failed;
                //gameplayColorCorrection.enabled = true;
                while (audioSource.pitch > 0f)
                {
                    audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, 0, 0.2f * Time.deltaTime);

                    if (audioSource.pitch == 0)
                    {
						Debug.Log("Pitch reached zero.");

						audioSource.time = audioSource.clip.length;

						OverlayCanvas.SetActive(false);
						GameUI.SetActive(false);
					}

                    //gameplayColorCorrection.saturation = audioSource.pitch;
                    yield return null;
                }
                break;
            }
            yield return null;
        }
        #endregion
	}

	IEnumerator End()
	{
        audioSource.Stop();

        levelSelect.gameObject.SetActive(false);

        StopCoroutine(fadeGameUI(true)); //This makes sure we will never have two fade coroutines happening simultaneously
        StartCoroutine(fadeGameUI(false));

        if (GameState.FinishStatus == DDRFinishStatus.Complete)
        {
            endScreen.gameObject.SetActive(true);
            ddrMinigame.UpdateEndcard(GameState);
        }
        else
        {
            defeatScreen.SetActive(true);
        }

        target = screenPoint;
        LoadedLevel = null;
        ddrMinigame.UnloadLevelSelect();

        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(fade(true, continueText));
        while (true)
        {
            if (Input.anyKeyDown)
            {
                if (!(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
                {
                    break;
                }
            }
            yield return null;
        }

        ddrMinigame.Unload();
        onLevelFinish(GameState.FinishStatus);
    }

    #region Helper methods
    private IEnumerator fadeGameUI(bool fadein)
    {
        float destination = fadein ? 1 : 0;
        float amount = fadein ? 0 : 1;
        while (amount != destination)
        {
            amount = Mathf.Lerp(amount, destination, 10 * Time.deltaTime);
            foreach (RawImage overlay in overlayImages)
            {
                Color color = overlay.color;
                color.a = amount;
                overlay.color = color;
            }
            yield return null;
        }
    }
    private IEnumerator fade(bool fadein, MaskableGraphic graphic, float speed = 1)
    {
        float destination = fadein ? 1 : 0;
        float amount = fadein ? 0 : 1;
        while (amount!=destination)
        {
            amount = Mathf.Lerp(amount, destination, speed*Time.deltaTime);
            Color color = graphic.color;
            color.a = amount;
            graphic.color = color;
            yield return null;
        }
    }
    #endregion

    private void onLevelFinish(DDRFinishStatus status)
    {
        /* This function is called after the level has been completed and the player has exited out of the endscreen.
         * A DDRFinishStatus property is passed into this method accordingly:
         * status will be equal to 'DDRFinishStatus.Complete' when a level is completed succesfully
         * status will be equal to 'DDRFinishStatus.Failed' when a level is failed due to HP loss */

        //For example, you could either use the BeginMinigame() method to restart, or quit the minigame with BootOut();
        //BeginMinigame();
        BootOut();
    }

	public void ReturnToNormalGameplay()
	{
		Debug.Log("ReturnToNormalGameplay() was called.");

		yandereAnim["f02_danceMachineIdle_00"].weight = 0;
		yandereAnim["f02_danceRight_00"].weight = 0;
		yandereAnim["f02_danceLeft_00"].weight = 0;
		yandereAnim["f02_danceUp_00"].weight = 0;
		yandereAnim["f02_danceDown_00"].weight = 0;

		Yandere.transform.position = FinishLocation.position;
		Yandere.transform.rotation = FinishLocation.rotation;
		Yandere.StudentManager.Clock.StopTime = false;
		Yandere.MyController.enabled = true;
		Yandere.StudentManager.ComeBack();
		Yandere.CanMove = true;
		Yandere.enabled = true;

		Yandere.HeartCamera.enabled = true;
		Yandere.HUD.enabled = true;

		Yandere.HUD.transform.parent.gameObject.SetActive(true);
		Yandere.MainCamera.gameObject.SetActive(true);

		Yandere.Jukebox.Volume = Yandere.Jukebox.LastVolume;
		OriginalRenderer.enabled = true;

		Physics.SyncTransforms();

		transform.parent.gameObject.SetActive(false);
	}
}