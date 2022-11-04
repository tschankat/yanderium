using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class AIController : AIMover
    {
        public enum AIState { Entering, Menu, Ordering, Waiting, Eating, Leaving }

        public GameObject throbObject;
        public Meter happinessMeter;
        public Bubble speechBubble;
        public float distanceThreshold = 0.5f;

        Food desiredFood;
        Collider2D collider2d;
        Chair targetChair;
        [HideInInspector] public Transform leaveTarget;
        [HideInInspector] public AIState state = AIState.Entering;
        Animator animator;
        SpriteRenderer spriteRenderer;
        float patienceDegradation = 2f;
        float timeToOrder = 0.5f;
        float timeToEat;
        float happiness = 50;
        float orderTime;
        float eatTime;
        int normalSortingLayer;
        bool isPaused;
        public bool Male;

        public void Init()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            throbObject.SetActive(false);
            targetChair = Chair.RandomChair;
            collider2d = GetComponent<Collider2D>();
            collider2d.enabled = false;
            if (targetChair == null) Destroy(gameObject);
            happinessMeter.gameObject.SetActive(false);
            speechBubble.gameObject.SetActive(false);
        }

        private void Start()
        {
            leaveTarget.GetComponent<CustomerSpawner>().OpenDoor();
            moveSpeed = GameController.Instance.activeDifficultyVariables.customerMoveSpeed;
            timeToOrder = GameController.Instance.activeDifficultyVariables.timeSpentOrdering;
            eatTime = GameController.Instance.activeDifficultyVariables.timeSpentEatingFood;
            patienceDegradation = GameController.Instance.activeDifficultyVariables.customerPatienceDegradation;
            timeToEat = GameController.Instance.activeDifficultyVariables.timeSpentEatingFood;

			SFXController.PlaySound(SFXController.Sounds.DoorBell);
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
            GetComponent<Animator>().speed = isPaused ? 0 : 1;
        }

        private void Update()
        {
            if (isPaused) return;

            switch (state)
            {
                case AIState.Entering:
                    if (Mathf.Abs(transform.position.x - targetChair.transform.position.x) <= distanceThreshold)
                    {
                        SitDown();
                        happiness = 100;
                        happinessMeter.SetFill(happiness / 100f);
                        state = AIState.Menu;
                    }
                    break;

                case AIState.Menu:
                    if (happiness <= 0)
                    {
                        StandUp();
                        state = AIState.Leaving;
                        GameController.AddAngryCustomer();
                    }
                    else
                    {
                        ReduceHappiness();
                    }
                    break;

                case AIState.Ordering:
                    if (orderTime <= 0)
                    {
                        state = AIState.Waiting;
                        speechBubble.GetComponent<Animator>().SetTrigger("BubbleDrop");
                        animator.SetTrigger("DoneOrdering");
                    }
                    else
                    {
                        orderTime -= Time.deltaTime;
                    }
                    break;

                case AIState.Waiting:
                    if (happiness <= 0)
                    {
                        StandUp();
                        state = AIState.Leaving;
                        GameController.AddAngryCustomer();
                    }
                    else
                    {
                        ReduceHappiness();
                    }
                    break;

                case AIState.Eating:
                    if (eatTime <= 0)
                    {
                        StandUp();
                        state = AIState.Leaving;
                    }
                    else
                    {
                        eatTime -= Time.deltaTime;
                    }
                    break;

                case AIState.Leaving:
                    if (Mathf.Abs(transform.position.x - leaveTarget.position.x) <= distanceThreshold)
                    {
                        Destroy(gameObject);
                        leaveTarget.GetComponent<CustomerSpawner>().OpenDoor();
                    }
                    break;
            }
        }

        public override ControlInput GetInput()
        {
            ControlInput inputs = new ControlInput();
            if (isPaused) return inputs;

            switch (state)
            {
                case AIState.Entering:
                    if (targetChair.transform.position.x > transform.position.x)
                    {
                        inputs.horizontal = 1;
                        SetFlip(false);
                    }
                    else
                    {
                        inputs.horizontal = -1;
                        SetFlip(true);
                    }
                    break;

                case AIState.Leaving:
                    if (leaveTarget.position.x > transform.position.x)
                    {
                        inputs.horizontal = 1;
                        SetFlip(false);
                    }
                    else
                    {
                        inputs.horizontal = -1;
                        SetFlip(true);
                    }
                    break;
            }
            return inputs;
        }

        public void TakeOrder()
        {
            state = AIState.Ordering;
            happiness = 100;
            happinessMeter.SetFill(happiness / 100f);
            orderTime = timeToOrder;
            animator.SetTrigger("OrderTaken");
            animator.SetFloat("Happiness", happiness);
            desiredFood = FoodMenu.Instance.GetRandomFood();
            speechBubble.gameObject.SetActive(true);
            speechBubble.food = desiredFood;

            if (Male)
            {
				SFXController.PlaySound(SFXController.Sounds.MaleCustomerGreet);
			}
			else
			{
				SFXController.PlaySound(SFXController.Sounds.FemaleCustomerGreet);
			}

            //TODO: Choose random plate to order and display speach bubble depicting said plate.
        }

        public void DeliverFood(Food deliveredFood)
        {
            if (deliveredFood.name == desiredFood.name)
            {
                state = AIState.Eating;
                animator.SetTrigger("ServedFood");
                eatTime = timeToEat;
                GameController.AddTip(GameController.Instance.activeDifficultyVariables.baseTip * happiness);
                if (happiness <= 50)
                {
                    happiness = 50;
                    animator.SetFloat("Happiness", happiness);
                }

				if (Male)
	            {
					SFXController.PlaySound(SFXController.Sounds.MaleCustomerThank);
				}
				else
				{
					SFXController.PlaySound(SFXController.Sounds.FemaleCustomerThank);
				}
            }
            else
            {
                state = AIState.Leaving;
                happiness = 0;
                animator.SetFloat("Happiness", happiness);
                GameController.AddAngryCustomer();
                StandUp();

				if (Male)
	            {
					SFXController.PlaySound(SFXController.Sounds.MaleCustomerLeave);
				}
				else
				{
					SFXController.PlaySound(SFXController.Sounds.FemaleCustomerLeave);
				}
            }

            happinessMeter.gameObject.SetActive(false);

            //TODO: compare plate to desired plate and reject if different
        }

        void SitDown()
        {
            transform.position = new Vector3(targetChair.transform.position.x, transform.position.y, transform.position.z);
            animator.SetTrigger("SitDown");
            SetFlip(targetChair.transform.localScale.x > 0 ? false : true);
            SetSortingLayer(true);
            collider2d.enabled = true;
            happinessMeter.gameObject.SetActive(true);
        }

        void StandUp()
        {
            animator.SetTrigger("StandUp");
            SetSortingLayer(false);
            targetChair.available = true;
            collider2d.enabled = false;
            happinessMeter.gameObject.SetActive(false);
        }

        void ReduceHappiness()
        {
            happiness -= Time.deltaTime * patienceDegradation;
            animator.SetFloat("Happiness", happiness);
            happinessMeter.SetFill(happiness / 100f);
        }

        void SetFlip(bool flip)
        {
            spriteRenderer.flipX = flip;
            GetComponentInChildren<CharacterHairPlacer>().hairInstance.flipX = flip;
        }

        public void SetSortingLayer(bool back)
        {
            spriteRenderer.sortingLayerName = back ? "CustomerSitting" : "Default";
            GetComponent<CharacterHairPlacer>().hairInstance.sortingLayerName = back ? "CustomerSitting" : "Default";
            throbObject.GetComponent<SpriteRenderer>().sortingLayerName = back ? "CustomerSitting" : "Default";
        }
    }
}