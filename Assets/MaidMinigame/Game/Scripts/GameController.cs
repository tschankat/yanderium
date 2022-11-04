using MaidDereMinigame.Malee;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MaidDereMinigame
{
    public delegate void BoolParameterEvent(bool b);

    public class GameController : MonoBehaviour
    {
        #region Properties
        static GameController instance;
        public static GameController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<GameController>();
                return instance;
            }
        }

        public static SceneWrapper Scenes {
            get
            {
                return Instance.scenes;
            }
        }

        [Reorderable]
        public Sprites numbers;
        public SceneWrapper scenes;
        [Tooltip("Scene Object Reference to return to when the game ends.")]
        public SceneObject returnScene;
        public SetupVariables activeDifficultyVariables;
        public SetupVariables easyVariables;
        public SetupVariables mediumVariables;
        public SetupVariables hardVariables;

        List<float> tips;
        SpriteRenderer spriteRenderer;
        int angryCustomers;
        [HideInInspector] public TipPage tipPage;
        [HideInInspector] public float totalPayout;
        [HideInInspector] public SpriteRenderer whiteFadeOutPost;
        
        public static BoolParameterEvent PauseGame;
        #endregion

        #region Go To Main Game
        public static void GoToExitScene(bool fadeOut = true)
        {
            //Insert code for loading real game scene here
            //Also add code to transfer earnings over to the real game
            //totalPayout is set when the tips are tallied and is 0 otherwise
            //yandereChansMunnie += Instance.totalPayout;

            /* This makes the scene fade to white if fadeOut == true */
            Instance.StartCoroutine(Instance.FadeWithAction(() =>
            {
				PlayerGlobals.Money += Instance.totalPayout;

				if (SceneManager.GetActiveScene().name == SceneNames.MaidMenuScene)
				{
					SceneManager.LoadScene(SceneNames.StreetScene);
				}
				else
				{
					SceneManager.LoadScene(SceneNames.CalendarScene);
				}

            }, fadeOut, true));
        }
        #endregion

        #region Methods
        private void Awake()
        {
            if (Instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
            spriteRenderer = GetComponent<SpriteRenderer>();
            DontDestroyOnLoad(gameObject);
        }

        public static void SetPause(bool toPause)
        {
            if (PauseGame != null)
                PauseGame(toPause);
        }

        public void LoadScene(SceneObject scene)
        {
			StartCoroutine(FadeWithAction(() => SceneManager.LoadScene(SceneNames.MaidGameScene)));
        }

        IEnumerator FadeWithAction(Action PostFadeAction, bool doFadeOut = true, bool destroyGameController = false)
        {
            float timeToFade = 0f;
            if (doFadeOut)
            {
                while (timeToFade <= activeDifficultyVariables.transitionTime)
                {
                    spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timeToFade / activeDifficultyVariables.transitionTime));
                    timeToFade += Time.deltaTime;
                    yield return null;
                }
            spriteRenderer.color = new Color(1, 1, 1, 1);
            }
            else
                timeToFade = activeDifficultyVariables.transitionTime;

            PostFadeAction();

            if (destroyGameController)
            {
                if (Instance.whiteFadeOutPost != null && doFadeOut)
                    Instance.whiteFadeOutPost.color = Color.white;
                Destroy(Instance.gameObject);
                Camera.main.farClipPlane = 0;
                instance = null;
            }
            else
            {
                while (timeToFade >= 0)
                {
                    spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timeToFade / activeDifficultyVariables.transitionTime));
                    timeToFade -= Time.deltaTime;
                    yield return null;
                }

                spriteRenderer.color = new Color(1, 1, 1, 0);
            }
        }

        public static void TimeUp()
        {
            SetPause(true);
            Instance.tipPage.Init();
            Instance.tipPage.DisplayTips(Instance.tips);
			FindObjectOfType<GameStarter>().GetComponent<AudioSource>().Stop();
        }

        public static void AddTip(float tip)
        {
            if (Instance.tips == null)
                Instance.tips = new List<float>();
            tip = Mathf.Floor(tip * 100) / 100f;
            Instance.tips.Add(tip);
        }

        public static float GetTotalDollars()
        {
            float total = 0;
            foreach (float tip in Instance.tips)
                total += Mathf.Floor(tip * 100f) / 100f;
            return total + Instance.activeDifficultyVariables.basePay;
        }

        public static void AddAngryCustomer()
        {
            Instance.angryCustomers++;
            //FindObjectOfType<GameStarter>().SetAudioPitch(Mathf.Lerp(1, 0, (float)Instance.angryCustomers / (float)Instance.activeDifficultyVariables.failQuantity));

            if (Instance.angryCustomers >= Instance.activeDifficultyVariables.failQuantity)
            {
                FailGame.GameFailed();
                SetPause(true);
            }
        }

        #endregion
    }

    [System.Serializable]
    public class Sprites : ReorderableArray<Sprite> { }

    [System.Serializable]
    public class SetupVariables
    {
        [Tooltip("Transition time for white fade between scenes")]
        public float transitionTime = 1f;

        [Tooltip("Base rate at which customers spawn.")]
        public float customerSpawnRate = 5f;

        [Tooltip("Amount of variance on the customer spawn rate. A random value between this and negative this is added to the base spawn rate.")]
        public float customerSpawnVariance = 5f;

        [Tooltip("Speed the player can move.")]
        public float playerMoveSpeed = 2f;

        [Tooltip("Speed the customers can move.")]
        public float customerMoveSpeed = 2f;

        [Tooltip("Patience degradation multiplier. Patience degrades at 1 point per second times this value.")]
        public float customerPatienceDegradation = 5f;

        [Tooltip("Time the customer will show their order. Patience resets after this amount of time.")]
        public float timeSpentOrdering = 2f;

        [Tooltip("Time the customer will stay in their chair eating after receiving their food.")]
        public float timeSpentEatingFood = 5f;

        [Tooltip("Base cooking time for food. This value is multiplied by the dish's individual cooking time multiplier. By default, they are all set to 1.")]
        public float baseCookingTime = 3f;

        [Tooltip("Base pay for the minigame.")]
        public float basePay = 100f;

        [Tooltip("Base tip multiplier. Tip is customer's remaining patience multiplied by this value.")]
        public float baseTip = 0.1f;

        [Tooltip("Time in seconds for the game to last.")]
        public float gameTime = 60f;

        [Tooltip("Game will fail if this many customers leave without being served.")]
        public int failQuantity = 5;
    }
}