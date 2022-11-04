using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class FailGame : MonoBehaviour
    {
        static FailGame instance;
        public static FailGame Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<FailGame>();
                return instance;
            }
        }

        public float fadeMultiplier = 2f;

        SpriteRenderer spriteRenderer;
        SpriteRenderer textRenderer;
        float targetTransitionTime;
        float transitionTime;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            textRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            targetTransitionTime = GameController.Instance.activeDifficultyVariables.transitionTime * fadeMultiplier;
        }

        public static void GameFailed()
        {
            Instance.StartCoroutine(Instance.GameFailedRoutine());
            Instance.StartCoroutine(Instance.SlowPitch());
            SFXController.PlaySound(SFXController.Sounds.GameFail);
        }

        IEnumerator GameFailedRoutine()
        {
            FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
            yield return null;

            textRenderer.color = Color.white;

            while (transitionTime < targetTransitionTime)
            {
                transitionTime += Time.deltaTime;
                yield return null;
            }

            transform.GetChild(1).gameObject.SetActive(true);

            while (!Input.anyKeyDown)
                yield return null;


            while (transitionTime < targetTransitionTime)
            {
                transitionTime += Time.deltaTime;
                float opacity = Mathf.Lerp(0, 1, transitionTime / targetTransitionTime);
                spriteRenderer.color = new Color(0, 0, 0, opacity);
                yield return null;
            }

            GameController.GoToExitScene(false);
        }

        IEnumerator SlowPitch()
        {
            GameStarter starter = FindObjectOfType<GameStarter>();
            float timeToZero = 5f;

            while (timeToZero > 0)
            {
                starter.SetAudioPitch(Mathf.Lerp(0, 1, timeToZero / 5f));
                timeToZero -= Time.deltaTime;
                yield return null;
            }

            starter.SetAudioPitch(0);
        }
    }
}