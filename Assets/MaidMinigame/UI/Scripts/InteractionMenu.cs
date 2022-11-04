using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class InteractionMenu : MonoBehaviour
    {
        static InteractionMenu instance;
        public static InteractionMenu Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<InteractionMenu>();
                return instance;
            }
        }

        public enum AButtonText
        {
            ChoosePlate,
            GrabPlate,
            KitchenMenu,
            PlaceOrder,
            TakeOrder,
            TossPlate,
            GiveFood,
            None
        }

        public GameObject interactObject;
        public GameObject backObject;
        public GameObject moveObject;
        public SpriteRenderer[] aButtons;
        public SpriteRenderer[] aButtonSprites;
        public SpriteRenderer[] backButtons;
        public SpriteRenderer[] moveButtons;

        private void Awake()
        {
            SetAButton(AButtonText.None);
            SetBButton(false);
            SetADButton(true);
        }

        public static void SetAButton(AButtonText text)
        {
            for (int i = 0; i < Instance.aButtonSprites.Length; i++)
            {
                if (i == (int)text)
                    Instance.aButtonSprites[i].gameObject.SetActive(true);
                else
                    Instance.aButtonSprites[i].gameObject.SetActive(false);
            }

            foreach (SpriteRenderer sprite in Instance.aButtons)
                sprite.gameObject.SetActive(text != AButtonText.None);
        }

        public static void SetBButton(bool on)
        {
            foreach (SpriteRenderer sprite in Instance.backButtons)
                sprite.gameObject.SetActive(on);
        }

        public static void SetADButton(bool on)
        {
            foreach (SpriteRenderer sprite in Instance.moveButtons)
                sprite.gameObject.SetActive(on);
        }
    }
}