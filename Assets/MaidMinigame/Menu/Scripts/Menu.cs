using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class Menu : MonoBehaviour
    {
        public List<MenuButton> mainMenuButtons;
        [HideInInspector] public FlipBook flipBook;

        MenuButton activeMenuButton;
        bool prevVertical;
        bool cancelInputs;

		float PreviousFrameVertical;

        private void Start()
        {
            for (int i = 0; i < mainMenuButtons.Count; i++)
            {
                int index = i;
                mainMenuButtons[i].Init();
                mainMenuButtons[i].index = index;
                mainMenuButtons[i].spriteRenderer.enabled = false;
                mainMenuButtons[i].menu = this;
            }
            flipBook = FlipBook.Instance;
            SetActiveMenuButton(0);
        }

        private void Update()
        {
            if (cancelInputs) return;

            if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("A")) && activeMenuButton != null)
            {
                activeMenuButton.DoClick();
            }

            float vertical = Input.GetAxisRaw("Vertical") * -1;

			if (Input.GetKeyDown("up") || Input.GetAxis("DpadY") > .50f)
            {
				vertical = -1;
			}
			else if (Input.GetKeyDown("down") || Input.GetAxis("DpadY") < -.50f)
			{
				vertical = 1;
			}

			if (vertical != 0 && PreviousFrameVertical == 0)
			{
				SFXController.PlaySound(SFXController.Sounds.MenuSelect);
			}

			PreviousFrameVertical = vertical;

            if (vertical != 0)
            {
                if (!prevVertical)
                {
                    prevVertical = true;

                    if (vertical < 0)
                    {
                        //up
                        int index = mainMenuButtons.IndexOf(activeMenuButton);
                        if (index == 0)
                            SetActiveMenuButton(mainMenuButtons.Count - 1);
                        else
                            SetActiveMenuButton(index - 1);
                    }
                    else
                    {
                        //down
                        int index = mainMenuButtons.IndexOf(activeMenuButton);
                        SetActiveMenuButton((index + 1) % mainMenuButtons.Count);
                    }
                }
            }
            else
                prevVertical = false;
        }

        public void SetActiveMenuButton(int index)
        {
            if (activeMenuButton != null)
                activeMenuButton.spriteRenderer.enabled = false;
            activeMenuButton = mainMenuButtons[index];
            activeMenuButton.spriteRenderer.enabled = true;
        }

        public void StopInputs()
        {
            cancelInputs = true;
            flipBook.StopInputs();
        }
    }
}