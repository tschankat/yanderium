using MaidDereMinigame.Malee;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;

namespace MaidDereMinigame
{
    public class TipCard : MonoBehaviour
    {
        [Reorderable]
        public SpriteRenderers digits;
        public SpriteRenderer dollarSign;

        public void SetTip(float tip)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");

            if (tip == 0f)
            {
                base.gameObject.SetActive(false);
            }
            string text = string.Format("{0:#.00}", tip).Replace(".", "");
            string text2 = "";
            for (int i = text.Length - 1; i >= 0; i--)
            {
                text2 += text[i].ToString();
            }
            for (int j = 0; j < text2.Length; j++)
            {
                int index = -1;
                if (int.TryParse(text2[j].ToString(), NumberStyles.Float, NumberFormatInfo.InvariantInfo, out index))
                {
                    this.digits[j].sprite = GameController.Instance.numbers[index];
                }
                else
                {
                    Debug.LogError("There was an issue while parsing the value in TipCard.SetTip");
                }
            }
            if (text2.Length <= 3)
            {
                this.digits[3].sprite = GameController.Instance.numbers[10];
                this.dollarSign.gameObject.SetActive(false);
            }
            if (text2.Length <= 4 && this.digits.Count > 4)
            {
                this.digits[4].sprite = GameController.Instance.numbers[10];
                this.dollarSign.gameObject.SetActive(false);
                if (text2.Length < 4)
                {
                    this.digits[3].sprite = GameController.Instance.numbers[10];
                    this.digits[4].gameObject.SetActive(false);
                }
            }
        }
    }

    [System.Serializable]
    public class SpriteRenderers : ReorderableArray<SpriteRenderer> { }
}