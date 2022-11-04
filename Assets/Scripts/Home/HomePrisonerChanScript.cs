using UnityEngine;

public class HomePrisonerChanScript : MonoBehaviour
{
	public HomeYandereDetectorScript YandereDetector;
	public HomeCameraScript HomeCamera;
	public CosmeticScript Cosmetic;
	public JsonScript JSON;

	public Vector3 RightEyeRotOrigin;
	public Vector3 LeftEyeRotOrigin;
	public Vector3 PermanentAngleR;
	public Vector3 PermanentAngleL;
	public Vector3 RightEyeOrigin;
	public Vector3 LeftEyeOrigin;
	public Vector3 Twitch;

	public Quaternion LastRotation;

	public Transform HomeYandere;
	public Transform RightBreast;
	public Transform LeftBreast;
	public Transform TwintailR;
	public Transform TwintailL;
	public Transform RightEye;
	public Transform LeftEye;
	public Transform Skirt;
	public Transform Neck;

	public GameObject RightMindbrokenEye;
	public GameObject LeftMindbrokenEye;
	public GameObject AnkleRopes;
	public GameObject Blindfold;
	public GameObject Character;
	public GameObject Tripod;

	public float HairRotation = 0.0f;
	public float TwitchTimer = 0.0f;
	public float NextTwitch = 0.0f;
	public float BreastSize = 0.0f;
	public float EyeShrink = 0.0f;
	public float Sanity = 0.0f;
	
	public float HairRot1 = 0.0f;
	public float HairRot2 = 0.0f;
	public float HairRot3 = 0.0f;
	public float HairRot4 = 0.0f;
	public float HairRot5 = 0.0f;

	public bool LookAhead = false;
	public bool Tortured = false;
	public bool Male = false;

	public int StudentID = 0;

	void Start()
	{
		if (SchoolGlobals.KidnapVictim > 0)
		{
			this.StudentID = SchoolGlobals.KidnapVictim;

			if (StudentGlobals.GetStudentSanity(this.StudentID) == 100)
			{
				AnkleRopes.SetActive(false);
			}

			this.PermanentAngleR = this.TwintailR.eulerAngles;
			this.PermanentAngleL = this.TwintailL.eulerAngles;

			if (!StudentGlobals.GetStudentArrested(this.StudentID) &&
				!StudentGlobals.GetStudentDead(this.StudentID))
			{
				this.Cosmetic.StudentID = this.StudentID;
				this.Cosmetic.enabled = true;

				this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;

				this.RightEyeRotOrigin = this.RightEye.localEulerAngles;
				this.LeftEyeRotOrigin = this.LeftEye.localEulerAngles;

				this.RightEyeOrigin = this.RightEye.localPosition;
				this.LeftEyeOrigin = this.LeftEye.localPosition;

				this.UpdateSanity();

				this.TwintailR.transform.localEulerAngles = new Vector3(0.0f, 180.0f, -90.0f);
				this.TwintailL.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f);

				this.Blindfold.SetActive(false);
				this.Tripod.SetActive(false);

				if (this.StudentID == 81)
				{
					if (!StudentGlobals.GetStudentBroken(81) && SchemeGlobals.HelpingKokona)
					{
						this.Blindfold.SetActive(true);
						this.Tripod.SetActive(true);
					}
				}
			}
			else
			{
				SchoolGlobals.KidnapVictim = 0;

				// [af] Added "gameObject" for C# compatibility.
				this.gameObject.SetActive(false);
			}
		}
		else
		{
			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}
	}

	void LateUpdate()
	{
		this.Skirt.transform.localPosition = new Vector3(0.0f, -0.135f, 0.010f);
		this.Skirt.transform.localScale = new Vector3(
			this.Skirt.transform.localScale.x,
			1.20f,
			this.Skirt.transform.localScale.z);

		if (!this.Tortured)
		{
			if (this.Sanity > 0.0f)
			{
				if (this.LookAhead)
				{
					this.Neck.localEulerAngles = new Vector3(
						this.Neck.localEulerAngles.x - 45.0f,
						this.Neck.localEulerAngles.y,
						this.Neck.localEulerAngles.z);
				}
				else if (this.YandereDetector.YandereDetected &&
					(Vector3.Distance(this.transform.position, this.HomeYandere.position) < 2.0f))
				{
					Quaternion targetRotation;

					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						targetRotation = Quaternion.LookRotation(
							this.HomeCamera.transform.position +
							(Vector3.down * (1.50f * ((100.0f - Sanity) / 100.0f))) -
							this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot1, Time.deltaTime * 2.0f);
					}
					else
					{
						targetRotation = Quaternion.LookRotation(
							(this.HomeYandere.position + (Vector3.up * 1.50f)) -
							this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot2, Time.deltaTime * 2.0f);
					}

					this.Neck.rotation = Quaternion.Slerp(
						this.LastRotation, targetRotation, Time.deltaTime * 2.0f);

					this.TwintailR.transform.localEulerAngles =
						new Vector3(this.HairRotation, 180.0f, -90.0f);
					this.TwintailL.transform.localEulerAngles =
						new Vector3(-this.HairRotation, 0.0f, -90.0f);
				}
				else
				{
					Quaternion targetRotation;

					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						targetRotation = Quaternion.LookRotation(
							this.HomeCamera.transform.position +
							(Vector3.down * (1.50f * ((100.0f - Sanity) / 100.0f))) -
							this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot3, Time.deltaTime * 2.0f);
					}
					else
					{
						targetRotation = Quaternion.LookRotation(
							(this.transform.position + this.transform.forward) -
							this.Neck.position);
						this.Neck.rotation = Quaternion.Slerp(
							this.LastRotation, targetRotation, Time.deltaTime * 2.0f);
					}

					this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot4, Time.deltaTime * 2.0f);

					this.TwintailR.transform.localEulerAngles =
						new Vector3(this.HairRotation, 180.0f, -90.0f);
					this.TwintailL.transform.localEulerAngles =
						new Vector3(-this.HairRotation, 0.0f, -90.0f);
				}
			}
			else
			{
				this.Neck.localEulerAngles = new Vector3(
					this.Neck.localEulerAngles.x - 45.0f,
					this.Neck.localEulerAngles.y,
					this.Neck.localEulerAngles.z);
			}
		}
		else
		{
			// Do nothing.
		}

		this.LastRotation = this.Neck.rotation;

		if (!this.Tortured)
		{
			if ((this.Sanity < 100.0f) && (this.Sanity > 0.0f))
			{
				this.TwitchTimer += Time.deltaTime;

				if (this.TwitchTimer > this.NextTwitch)
				{
					this.Twitch = new Vector3(
						(1.0f - (this.Sanity / 100.0f)) * Random.Range(-10.0f, 10.0f),
						(1.0f - (this.Sanity / 100.0f)) * Random.Range(-10.0f, 10.0f),
						(1.0f - (this.Sanity / 100.0f)) * Random.Range(-10.0f, 10.0f));

					this.NextTwitch = Random.Range(0.0f, 1.0f);
					this.TwitchTimer = 0.0f;
				}

				this.Twitch = Vector3.Lerp(this.Twitch, Vector3.zero, Time.deltaTime * 10.0f);
				this.Neck.localEulerAngles += this.Twitch;
			}
		}

		if (this.Tortured)
		{
			this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot5, Time.deltaTime * 2.0f);

			this.TwintailR.transform.localEulerAngles =
				new Vector3(this.HairRotation, 180.0f, -90.0f);
			this.TwintailL.transform.localEulerAngles =
				new Vector3(-this.HairRotation, 0.0f, -90.0f);

			// [af] Commented in JS code.
			/*
			if (EyeShrink > 1)
			{
				EyeShrink = 1;
			}

			if (Sanity >= 50)
			{
				LeftEye.localPosition.z = LeftEye.localPosition.z - (EyeShrink * .009);
				RightEye.localPosition.z = RightEye.localPosition.z + (EyeShrink * .009);
				LeftEye.localPosition.x = LeftEye.localPosition.x - (EyeShrink * 0.002);
				RightEye.localPosition.x = RightEye.localPosition.x - (EyeShrink * 0.002);

				LeftEye.localEulerAngles.x = LeftEye.localEulerAngles.x + 5 + Random.Range(EyeShrink * -1.0, EyeShrink * 1.0);
				LeftEye.localEulerAngles.y = LeftEye.localEulerAngles.y + Random.Range(EyeShrink * -1.0, EyeShrink * 1.0);
				LeftEye.localEulerAngles.z = LeftEye.localEulerAngles.z + Random.Range(EyeShrink * -1.0, EyeShrink * 1.0);

				RightEye.localEulerAngles.x = RightEye.localEulerAngles.x - 5 + Random.Range(EyeShrink * -1.0, EyeShrink * 1.0);
				RightEye.localEulerAngles.y = RightEye.localEulerAngles.y + Random.Range(EyeShrink * -1.0, EyeShrink * 1.0);
				RightEye.localEulerAngles.z = RightEye.localEulerAngles.z + Random.Range(EyeShrink * -1.0, EyeShrink * 1.0);

				LeftEye.localScale.x = 1 - (EyeShrink * .5);
				LeftEye.localScale.y = 1 - (EyeShrink * .5);

				RightEye.localScale.x = 1 - (EyeShrink * .5);
				RightEye.localScale.y = 1 - (EyeShrink * .5);
			}
			*/
		}

		// [af] Commented in JS code.
		//TwintailR.eulerAngles.x = PermanentAngleR.x;
		//TwintailL.eulerAngles.x = PermanentAngleL.x;
		//TwintailR.eulerAngles.z = PermanentAngleR.z;
		//TwintailL.eulerAngles.z = PermanentAngleL.z;
	}

	public void UpdateSanity()
	{
		this.Sanity = StudentGlobals.GetStudentSanity(this.StudentID);

		// [af] Replaced if/else statements with boolean expressions.
		bool zeroSanity = this.Sanity == 0.0f;
		this.RightMindbrokenEye.SetActive(zeroSanity);
		this.LeftMindbrokenEye.SetActive(zeroSanity);
	}
}
