using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class Meter : MonoBehaviour
    {
        public SpriteRenderer fillBar;
        public float emptyPos;

        float startPos;

        private void Awake()
        {
            startPos = fillBar.transform.localPosition.x;
        }

        public void SetFill(float interpolater)
        {
            float posx = Mathf.Lerp(emptyPos, startPos, interpolater);
            posx = Mathf.Round(posx * 50f) / 50f;
            fillBar.transform.localPosition = new Vector3(posx, 0, 0);
        }
    }
}