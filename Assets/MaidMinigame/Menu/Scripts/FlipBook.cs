using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class FlipBook : MonoBehaviour
    {
        static FlipBook instance;
        public static FlipBook Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<FlipBook>();
                return instance;
            }
        }

        public List<FlipBookPage> flipBookPages;

        int curPage = 0;
        bool canGoBack;
        bool stopInputs;

        private void Awake()
        {
            StartCoroutine(OpenBook());
        }

        IEnumerator OpenBook()
        {
            yield return new WaitForSeconds(1f);
            FlipToPage(1);
        }

        private void Update()
        {
            if (stopInputs) return;
            if (curPage > 1 && Input.GetButtonDown("B") && canGoBack)
                FlipToPage(1);
        }

        public void StopInputs()
        {
            stopInputs = true;
        }

        public void FlipToPage(int page)
        {
            SFXController.PlaySound(SFXController.Sounds.PageTurn);
            StartCoroutine(FlipToPageRoutine(page));
        }

        IEnumerator FlipToPageRoutine(int page)
        {
            bool forward = curPage < page;
            canGoBack = false;

            if (forward)
            {
                while (curPage < page)
                {
                    flipBookPages[curPage++].Transition(forward);
                    //yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.4f);
                flipBookPages[curPage].ObjectActive();
            }
            else
            {
                flipBookPages[curPage].ObjectActive(false);
                while (curPage > page)
                {
                    flipBookPages[--curPage].Transition(forward);
                    //yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.6f);
                flipBookPages[curPage].ObjectActive();
            }

            canGoBack = true;
        }
    }
}