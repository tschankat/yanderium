using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class GameStarter : MonoBehaviour
    {
        public List<Sprite> numbers;
        public SpriteRenderer whiteFadeOutPost;
        public Sprite timeUp;
        public TipPage tipPage;

        AudioSource audioSource;
        SpriteRenderer spriteRenderer;
        int timeToStart = 3;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
            StartCoroutine(CountdownToStart());
            GameController.Instance.tipPage = tipPage;
            GameController.Instance.whiteFadeOutPost = whiteFadeOutPost;
        }

        public void SetGameTime(float gameTime)
        {
            int gameSeconds = Mathf.CeilToInt(gameTime);

            if (gameSeconds == 10f) SFXController.PlaySound(SFXController.Sounds.ClockTick);
            if (gameTime > 3f) return;


            switch (gameSeconds)
            {
                case 3:
                case 2:
                case 1:
                    spriteRenderer.sprite = numbers[gameSeconds];
                    break;

                default:
                    EndGame();
                    break;
            }
        }

        public void EndGame()
        {
            StartCoroutine(EndGameRoutine());
            SFXController.PlaySound(SFXController.Sounds.GameSuccess);
        }

        IEnumerator CountdownToStart()
        {
            yield return new WaitForSeconds(GameController.Instance.activeDifficultyVariables.transitionTime);
            SFXController.PlaySound(SFXController.Sounds.Countdown);

            while (timeToStart > 0)
            {
                yield return new WaitForSeconds(1f);
                timeToStart--;
                spriteRenderer.sprite = numbers[timeToStart];
            }

            yield return new WaitForSeconds(1f);
            GameController.SetPause(false);
            spriteRenderer.sprite = null;
        }

        IEnumerator EndGameRoutine()
        {
            GameController.SetPause(true);
            spriteRenderer.sprite = timeUp;
            yield return new WaitForSeconds(1f);
            FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
            GameController.TimeUp();
        }

        public void SetAudioPitch(float value)
        {
            audioSource.pitch = value;
        }
    }
}
