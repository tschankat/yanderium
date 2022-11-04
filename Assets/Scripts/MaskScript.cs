using UnityEngine;

public class MaskScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public ClubManagerScript ClubManager;
	public YandereScript Yandere;
	public PromptScript Prompt;
	public PickUpScript PickUp;

	public Projector Blood;

	public Renderer MyRenderer;
	public MeshFilter MyFilter;

	public Texture[] Textures;

	public Mesh[] Meshes;

	public int ID = 0;

	void Start()
	{
		if (GameGlobals.MasksBanned)
		{
			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}
		else
		{
			this.MyFilter.mesh = this.Meshes[this.ID];
			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}

		this.enabled = false;
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1;

			this.StudentManager.CanAnyoneSeeYandere();

			if (!this.StudentManager.YandereVisible && !this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				Rigidbody rigidBody = this.GetComponent<Rigidbody>();
				rigidBody.useGravity = false;
				rigidBody.isKinematic = true;

				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Prompt.MyCollider.enabled = false;

				this.transform.parent = this.Yandere.Head;
				this.transform.localPosition = new Vector3(0.0f, 0.033333f, 0.10f);
				this.transform.localEulerAngles = Vector3.zero;

				this.Yandere.Mask = this;
				this.ClubManager.UpdateMasks();

				this.StudentManager.UpdateStudents();
			}
			else
			{
				this.Yandere.NotificationManager.CustomText = "Not now. Too suspicious.";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			}
		}
	}

	public void Drop()
	{
		this.Prompt.MyCollider.isTrigger = false;
		this.Prompt.MyCollider.enabled = true;

		Rigidbody rigidBody = this.GetComponent<Rigidbody>();
		rigidBody.useGravity = true;
		rigidBody.isKinematic = false;

		this.Prompt.enabled = true;

		this.transform.parent = null;

		this.Yandere.Mask = null;
		this.ClubManager.UpdateMasks();

		this.StudentManager.UpdateStudents();
	}
}
