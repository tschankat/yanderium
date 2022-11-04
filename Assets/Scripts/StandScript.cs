using UnityEngine;

public class StandScript : MonoBehaviour
{
	public AmplifyMotionEffect MotionBlur;
	public FalconPunchScript FalconPunch;
	public StandPunchScript StandPunch;
	public Transform SummonTransform;
	public GameObject SummonEffect;
	public GameObject StandCamera;
	public YandereScript Yandere;
	public GameObject Stand;

	public Transform[] Hands;

	public int FinishPhase = 0;
	public int Finisher = 0;
	public int Weapons = 0;
	public int Phase = 0;

	public AudioClip SummonSFX;

	public bool ReadyForFinisher = false;
	public bool SFX = false;

	void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.enabled = false;
		}
	}

	void Update()
	{
		if (!this.Stand.activeInHierarchy)
		{
			if (this.Weapons == 8)
			{
				if (this.Yandere.transform.position.y > 11.90f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_RB))
					{
						if (!MissionModeGlobals.MissionMode)
						{
							if (!this.Yandere.Laughing)
							{
								if (this.Yandere.CanMove)
								{
									this.Yandere.Jojo();
								}
							}
						}
					}
				}
			}
		}
		else
		{
			if (this.Phase == 0)
			{
				if ((this.Stand.GetComponent<Animation>()["StandSummon"].time >= 2.0f) &&
					(this.Stand.GetComponent<Animation>()["StandSummon"].time <= 2.50f))
				{
					if (!this.SFX)
					{
						AudioSource.PlayClipAtPoint(this.SummonSFX, this.transform.position);
						this.SFX = true;
					}

					Instantiate(this.SummonEffect, this.SummonTransform.position, Quaternion.identity);
				}

				if (this.Stand.GetComponent<Animation>()["StandSummon"].time >=
					this.Stand.GetComponent<Animation>()["StandSummon"].length)
				{
					this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
					this.Phase++;
				}
			}
			else
			{
				float v = Input.GetAxis("Vertical");
				float h = Input.GetAxis("Horizontal");

				if (this.Yandere.CanMove)
				{
					this.Return();

					if ((v != 0.0f) || (h != 0.0f))
					{
						if (this.Yandere.Running)
						{
							this.Stand.GetComponent<Animation>().CrossFade("StandRun");
						}
						else
						{
							this.Stand.GetComponent<Animation>().CrossFade("StandWalk");
						}
					}
					else
					{
						this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
					}
				}
				else
				{
					if (this.Yandere.RPGCamera.enabled)
					{
						if (this.Yandere.Laughing)
						{
							if (Vector3.Distance(this.Stand.transform.localPosition, new Vector3(0.0f, 0.20f, -0.40f)) > 0.010f)
							{
								this.Stand.transform.localPosition = Vector3.Lerp(
									this.Stand.transform.localPosition,
									new Vector3(0.0f, 0.20f, 0.10f),
									Time.deltaTime * 10.0f);

								this.Stand.transform.localEulerAngles = new Vector3(
									Mathf.Lerp(this.Stand.transform.localEulerAngles.x, 22.50f, Time.deltaTime * 10.0f),
									this.Stand.transform.localEulerAngles.y,
									this.Stand.transform.localEulerAngles.z);
							}

							this.Stand.GetComponent<Animation>().CrossFade("StandAttack");
							this.StandPunch.MyCollider.enabled = true;

							this.ReadyForFinisher = true;
						}
						else
						{
							if (this.ReadyForFinisher)
							{
								if (this.Phase == 1)
								{
									this.GetComponent<AudioSource>().Play();
									this.Finisher = Random.Range(1, 3);
									this.Stand.GetComponent<Animation>().CrossFade("StandFinisher" + this.Finisher.ToString());

									this.Phase++;
								}
								else if (this.Phase == 2)
								{
									if (this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].time >= 0.50f)
									{
										this.FalconPunch.MyCollider.enabled = true;
										this.StandPunch.MyCollider.enabled = false;
										this.Phase++;
									}
								}
								else if (this.Phase == 3)
								{
									//Debug.Log("StandFinisher" + Finisher);

									if (this.StandPunch.MyCollider.enabled ||
										(this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].time >=
										this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].length))
									{
										this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
										this.FalconPunch.MyCollider.enabled = false;
										this.ReadyForFinisher = false;
										this.Yandere.CanMove = true;
										this.Phase = 1;
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public void Spawn()
	{
		this.FalconPunch.MyCollider.enabled = false;
		this.StandPunch.MyCollider.enabled = false;
		this.StandCamera.SetActive(true);
		this.MotionBlur.enabled = true;
		this.Stand.SetActive(true);
	}

	void Return()
	{
		if (Vector3.Distance(this.Stand.transform.localPosition, new Vector3(0.0f, 0.0f, -0.50f)) > 0.010f)
		{
			this.Stand.transform.localPosition = Vector3.Lerp(
				this.Stand.transform.localPosition,
				new Vector3(0.0f, 0.0f, -0.50f),
				Time.deltaTime * 10.0f);

			this.Stand.transform.localEulerAngles = new Vector3(
				Mathf.Lerp(this.Stand.transform.localEulerAngles.x, 0.0f, Time.deltaTime * 10.0f),
				this.Stand.transform.localEulerAngles.y,
				this.Stand.transform.localEulerAngles.z);
		}
	}
}
