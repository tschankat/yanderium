using MaidDereMinigame.Malee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    [RequireComponent(typeof(Animator))]
    public class Chef : MonoBehaviour
    {
        public enum ChefState
        {
            Queueing,
            Cooking,
            Delivering
        }

        static Chef instance;
        public static Chef Instance {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<Chef>();
                return instance;
            }
        }

        [Reorderable] public Foods cookQueue;
        public FoodMenu foodMenu;
        public Meter cookMeter;
        public float cookTime = 3f;

        ChefState state;
        Food currentPlate;
        Animator animator;
        float timeToFinishDish;
        bool isPaused;

        private void Awake()
        {
            cookQueue = new Foods();
            animator = GetComponent<Animator>();
            cookMeter.gameObject.SetActive(false);
            isPaused = true;
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
            isPaused = toPause;
            animator.speed = isPaused ? 0 : 1;
        }

        public static void AddToQueue(Food foodItem)
        {
            Instance.cookQueue.Add(foodItem);
        }

        public static Food GrabFromQueue()
        {
            Food foodItem = Instance.cookQueue[0];
            Instance.cookQueue.RemoveAt(0);
            return foodItem;
        }

        private void Update()
        {
            if (isPaused) return;

            switch (state)
            {
                case ChefState.Queueing:
                    if (cookQueue.Count > 0)
                    {
                        currentPlate = GrabFromQueue();
                        timeToFinishDish = currentPlate.cookTimeMultiplier * cookTime;
                        state = ChefState.Cooking;
                        cookMeter.gameObject.SetActive(true);
                    }
                    break;

                case ChefState.Cooking:
                    if (timeToFinishDish <= 0)
                    {
                        state = ChefState.Delivering;
                        animator.SetTrigger("PlateCooked");
                        cookMeter.gameObject.SetActive(false);
                    }
                    else
                    {
                        timeToFinishDish -= Time.deltaTime;
                        cookMeter.SetFill(1 - (timeToFinishDish / (currentPlate.cookTimeMultiplier * cookTime)));
                    }
                    break;
            }
        }

        public void Deliver()
        {
            FindObjectOfType<ServingCounter>().AddPlate(currentPlate);
        }

        public void Queue()
        {
            state = ChefState.Queueing;
        }
    }
}