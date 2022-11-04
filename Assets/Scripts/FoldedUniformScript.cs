using UnityEngine;

public class FoldedUniformScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;

	public GameObject SteamCloud;

	public bool InPosition = true;
	public bool Clean = false;
	public bool Spare = false;

	public float Timer = 0.0f;

	public int Type = 0;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();

		bool DoNotContinue = false;

		if (this.Spare)
		{
			if (!GameGlobals.SpareUniform)
			{
				Destroy(gameObject);
				DoNotContinue = true;
			}
		}

		if (!DoNotContinue)
		{
			if (this.Clean)
			{
				if (this.Prompt.Button[0] != null)
				{
					this.Prompt.HideButton[0] = true;
					this.Yandere.StudentManager.NewUniforms++;
					this.Yandere.StudentManager.UpdateStudents();
					this.Yandere.StudentManager.Uniforms[this.Yandere.StudentManager.NewUniforms] = this.transform;

					Debug.Log("A new uniform has appeared. There are now " + this.Yandere.StudentManager.NewUniforms + " new uniforms at school.");
				}
			}
		}
	}

	void Update()
	{
		if (this.Clean)
		{
			// [af] Replaced if/else statement with boolean expression.
			this.InPosition = this.Yandere.StudentManager.LockerRoomArea.bounds.Contains(this.transform.position);

			if (this.Yandere.MyRenderer.sharedMesh == this.Yandere.Towel)
			{
				Debug.Log("Yandere-chan is wearing a towel.");
			}

			if (this.Yandere.Bloodiness == 0.0f)
			{
				Debug.Log("Yandere-chan is not bloody.");
			}

			if (this.InPosition)
			{
				Debug.Log("This uniform is in the locker room.");
			}

			// [af] Replaced if/else statement with boolean expression.
			if (this.Yandere.MyRenderer.sharedMesh != this.Yandere.Towel ||
				this.Yandere.Bloodiness != 0.0f || !this.InPosition)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				Instantiate(this.SteamCloud,
					this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
				this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleStripping);
				this.Yandere.CurrentUniformOrigin = 2;
				this.Yandere.Stripping = true;
				this.Yandere.CanMove = false;

				this.Timer += Time.deltaTime;
			}

			if (this.Timer > 0.0f)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 1.50f)
				{
					this.Yandere.Schoolwear = 1;
					this.Yandere.ChangeSchoolwear();

					Destroy(this.gameObject);
				}
			}
		}
	}
}
