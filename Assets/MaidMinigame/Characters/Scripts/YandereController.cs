using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class YandereController : AIMover
    {
        static YandereController instance;
        public static YandereController Instance {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<YandereController>();
                return instance;
            }
        }

        public static bool leftButton;
        public static bool rightButton;

        public Transform leftBounds;
        public Transform rightBounds;
        public Transform interactionIndicator;
        public Transform plateTransform;
        public Food heldItem;

        SpriteRenderer spriteRenderer;
        Animator animator;
        AIController aiTarget;
		public bool leftButtonPast;
		public bool rightButtonPast;
        bool isPaused;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            plateTransform.gameObject.SetActive(false);
            moveSpeed = GameController.Instance.activeDifficultyVariables.playerMoveSpeed;
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
            if (isPaused) animator.SetBool("Moving", false);
            animator.speed = isPaused ? 0 : 1;
        }

        private void Update()
        {
            rightButton = false;
            leftButton = false;

			if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetKey("right") || Input.GetAxis("DpadX") > .50f)
            {
                if (!rightButtonPast)
                {
                    rightButtonPast = true;
                    rightButton = true;
                }
            }
			else if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetKey("left") || Input.GetAxis("DpadX") < -.50f)
            {
                if (!leftButtonPast)
                {
                    leftButtonPast = true;
                    leftButton = true;
                }
            }
            else
            {
                leftButtonPast = false;
                rightButtonPast = false;
            }

            if (transform.position.x < leftBounds.position.x)
                transform.position = new Vector3(leftBounds.position.x, transform.position.y, transform.position.z);
            if (transform.position.x > rightBounds.position.x)
                transform.position = new Vector3(rightBounds.position.x, transform.position.y, transform.position.z);

            if (Input.GetButtonDown("A"))
            {
                if (aiTarget != null)
                {
                    if (aiTarget.state == AIController.AIState.Menu)
                    {
                        aiTarget.TakeOrder();
                        InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
                    }
                    else if (aiTarget.state == AIController.AIState.Waiting)
                    {
                        if (heldItem != null)
                        {
                            aiTarget.DeliverFood(heldItem);
                            SFXController.PlaySound(SFXController.Sounds.Plate);
                            InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
                            DropTray();
                        }
                    }
                }
            }

            if (aiTarget != null)
            {
                interactionIndicator.gameObject.SetActive(true);
                interactionIndicator.position = new Vector3(aiTarget.transform.position.x, aiTarget.transform.position.y + 0.6f, aiTarget.transform.position.z);
            }
            else
            {
                interactionIndicator.gameObject.SetActive(false);
            }
        }

        public override ControlInput GetInput()
        {
            if (isPaused)
            {
                animator.SetBool("Moving", false);
                return new ControlInput();
            }

            float Direction = 0;

			if (rightButtonPast)
            {
            	Direction = 1;
			}
			else if (leftButtonPast)
			{
				Direction = -1;
			}

            ControlInput inputs = new ControlInput();
			inputs.horizontal = Direction;//Input.GetAxisRaw("Horizontal");

            if (inputs.horizontal != 0)
            {
                if (inputs.horizontal < 0) spriteRenderer.flipX = true;
                else if (inputs.horizontal > 0) spriteRenderer.flipX = false;
                animator.SetBool("Moving", true);
            }
            else
                animator.SetBool("Moving", false);
            return inputs;
        }

        public void PickUpTray(Food plate)
        {
            animator.SetTrigger("GetTray");
            heldItem = plate;
            plateTransform.gameObject.SetActive(false);
            plateTransform.GetComponent<SpriteRenderer>().sprite = heldItem.smallSprite;
            plateTransform.gameObject.SetActive(true);
        }

        public void DropTray()
        {
            plateTransform.gameObject.SetActive(false);
            animator.SetTrigger("DropTray");
            heldItem = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            AIController ai = collision.GetComponent<AIController>();
            if (ai != null)
            {
                if (ai.state == AIController.AIState.Menu)
                {
                    aiTarget = ai;
                    InteractionMenu.SetAButton(InteractionMenu.AButtonText.TakeOrder);
                }

                if (ai.state == AIController.AIState.Waiting && heldItem != null)
                {
                    aiTarget = ai;
                    InteractionMenu.SetAButton(InteractionMenu.AButtonText.GiveFood);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            AIController ai = collision.GetComponent<AIController>();
            if (ai != null && ai == aiTarget)
            {
                aiTarget = null;
                InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
            }
        }

        public void SetPause(bool toPause)
        {
            isPaused = toPause;
        }

        public void PositionTray(string point)
        {
            string[] points = point.Split(',');
            float a, b;

            float.TryParse(points[0], out a);
            float.TryParse(points[1], out b);

            //TODO: set position of plate
            plateTransform.localPosition = new Vector3(spriteRenderer.flipX ? -a : a, b, 0);
        }
    }
}