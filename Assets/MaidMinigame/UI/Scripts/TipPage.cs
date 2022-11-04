using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class TipPage : MonoBehaviour
    {
        public TipCard wageCard;
        public TipCard totalCard;

        List<TipCard> cards;
        bool stopInteraction;

        public void Init()
        {
            cards = new List<TipCard>();
            foreach (Transform row in transform.GetChild(0))
                foreach (Transform col in row)
                    cards.Add(col.GetComponent<TipCard>());
            gameObject.SetActive(false);
        }

        public void DisplayTips(List<float> tips)
        {
            if (tips == null) tips = new List<float>();
            gameObject.SetActive(true);
            float total = 0;

            for (int i = 0; i < cards.Count; i++)
            {
                if (tips.Count > i)
                {
                    cards[i].SetTip(tips[i]);
                    total += tips[i];
                }
                else
                    cards[i].SetTip(0);
            }

            float basePay = GameController.Instance.activeDifficultyVariables.basePay;
            GameController.Instance.totalPayout = total + basePay;
            wageCard.SetTip(basePay);
            totalCard.SetTip(total + basePay);
        }

        private void Update()
        {
            if (stopInteraction) return;

            if (Input.GetButtonDown("A"))
            {
                GameController.GoToExitScene();
                stopInteraction = true;
            }
        }
    }
}