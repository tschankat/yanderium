using UnityEngine;

public class FPSWalker : MonoBehaviour
{
	public float speed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	private bool grounded = false;

	void FixedUpdate()
	{
		if (this.grounded)
		{
			// We are grounded, so recalculate moveDirection directly from axes.
			this.moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
			this.moveDirection = this.transform.TransformDirection(this.moveDirection);
			this.moveDirection *= this.speed;

			if (Input.GetButton("Jump"))
			{
				this.moveDirection.y = this.jumpSpeed;
			}
		}

		// Apply gravity.
		this.moveDirection.y -= this.gravity * Time.deltaTime;

		// Move the controller.
		CharacterController controller = this.GetComponent<CharacterController>();
		CollisionFlags flags = controller.Move(this.moveDirection * Time.deltaTime);
		this.grounded = (flags & CollisionFlags.CollidedBelow) != 0;
	}

	void Awake()
	{
		CharacterController controller = this.GetComponent<CharacterController>();

		if (controller == null)
		{
			this.gameObject.AddComponent<CharacterController>();
		}
	}
}
