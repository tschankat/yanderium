using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class Timer : Meter
    {
        GameStarter starter;
        float gameTime;
        bool isPaused;

        private void Awake()
        {
            gameTime = GameController.Instance.activeDifficultyVariables.gameTime;
            starter = FindObjectOfType<GameStarter>();
            isPaused = true;
        }

        private void OnEnable()
        {
            GameController.PauseGame += SetPause;
        }

        private void OnDisable()
        {
            GameController.PauseGame -= SetPause;
        }

        public void SetPause(bool toPause)
        {
            isPaused = toPause;
        }

        private void Update()
        {
            if (isPaused) return;

            gameTime -= Time.deltaTime;
            SetFill(gameTime / GameController.Instance.activeDifficultyVariables.gameTime);

            starter.SetGameTime(gameTime);
        }
    }
}