using MaidDereMinigame.Malee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class FoodMenu : MonoBehaviour
    {
        static FoodMenu instance;
        public static FoodMenu Instance {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<FoodMenu>();
                return instance;
            }
        }

        [Reorderable]
        public Foods foodItems;
        public Transform menuSelector;
        public Transform menuSlotParent;
        public float selectorMoveSpeed = 3f;

        List<Transform> menuSlots;
        float menuSelectorTarget;
        float startY;
        float startZ;
        float interpolator;
        int activeIndex;

        private void Awake()
        {
            SetMenuIcons();

            menuSelectorTarget = menuSlots[0].position.x;
            startY = menuSelector.position.y;
            startZ = menuSelector.position.z;
        }

        public void SetMenuIcons()
        {
            menuSlots = new List<Transform>();
            for (int i = 0; i < menuSlotParent.childCount; i++)
            {
                Transform t = menuSlotParent.GetChild(i);
                menuSlots.Add(t);
                if (foodItems.Count >= i)
                    t.GetChild(0).GetComponent<SpriteRenderer>().sprite = foodItems[i].largeSprite;
            }
        }

        public void SetActive(int index)
        {
            menuSelectorTarget = menuSlots[index].position.x;
            interpolator = 0;
            activeIndex = index;
        }

        public Food GetActiveFood()
        {
            Food fd = Instantiate(foodItems[activeIndex]);
            fd.name = foodItems[activeIndex].name;
            return fd;
        }

        public Food GetRandomFood()
        {
            int index = Random.Range(0, foodItems.Count);
            Food fd = Instantiate(foodItems[index]);
            fd.name = foodItems[index].name;
            return fd;
        }

        private void Update()
        {
            if (interpolator < 1)
            {
                float posX = Mathf.Lerp(menuSelector.position.x, menuSelectorTarget, interpolator);
                menuSelector.position = new Vector3(posX, startY, startZ);
                interpolator += Time.deltaTime * selectorMoveSpeed;
            }
            else
            {
                menuSelector.transform.position = new Vector3(menuSelectorTarget, startY, startZ);
            }

            if (YandereController.rightButton)
                IncrementSelection();
            else if (YandereController.leftButton)
                DecrementSelection();
        }

        void IncrementSelection()
        {
            SetActive((activeIndex + 1) % menuSlots.Count);
			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
        }

        void DecrementSelection()
        {
            if (activeIndex == 0)
                SetActive(menuSlots.Count - 1);
            else
                SetActive(activeIndex - 1);

			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
        }
    }
}
