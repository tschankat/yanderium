using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class Bubble : MonoBehaviour
    {
        [HideInInspector] public Food food;
        public SpriteRenderer foodRenderer;

        private void Awake()
        {
            foodRenderer.sprite = null;
        }

        private void OnEnable()
        {
            GameController.PauseGame += Pause;
        }

        private void OnDisable()
        {
            GameController.PauseGame -= Pause;
        }

        public void Pause(bool toPause)
        {
            if (toPause)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                foodRenderer.gameObject.SetActive(false);
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
                foodRenderer.gameObject.SetActive(true);
            }
        }

        public void BubbleReachedMax()
        {
            foodRenderer.gameObject.SetActive(true);
            foodRenderer.sprite = food.largeSprite;
        }

        public void BubbleClosing()
        {
            foodRenderer.gameObject.SetActive(false);
        }
        
        public void KillBubble()
        {
            Destroy(gameObject);
        }
    }
}