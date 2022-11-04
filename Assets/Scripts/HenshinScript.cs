using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenshinScript : MonoBehaviour
{
	public RiggedAccessoryAttacher MiyukiCostume;

	public SkinnedMeshRenderer MiyukiRenderer;
	
	public Renderer WhiteMiyukiRenderer;
	public Renderer MiyukiHairRenderer;
	public Renderer White;

	public Animation WhiteMiyukiAnim;
	public Animation MiyukiAnim;

	public GameObject HenshinSparkleBlast;
	public GameObject MiyukiHair;

	public ParticleSystem HenshinSparkles;
	public ParticleSystem SpinSparkles;
	public ParticleSystem Sparkles;

	public AudioListener Listener;

	public YandereScript Yandere;

	public GameObject[] Cameras;

	public Camera MiyukiCamera;

	public Transform RightHand;
	public Transform Miyuki;
	public Transform Wand;
	public Transform TV;

	public float Rotation;
	public float Timer;

	public int Phase;

	public Texture MiyukiFace;
	public Texture MiyukiSkin;
	public Mesh NudeMesh;

	public Texture OriginalBody;
	public Texture OriginalFace;
	public Mesh OriginalMesh;

	public bool TransformingYandere;
	public bool Debugging;

	public Quaternion OriginalRotation;
	public Vector3 OriginalPosition;
	public AudioSource MyAudio;

	public AudioClip Catchphrase;

	public void TransformYandere()
	{
		TransformingYandere = true;

		Cameras[1].SetActive(false);
		Cameras[2].SetActive(false);
		Cameras[3].SetActive(false);
		Cameras[4].SetActive(false);
		Cameras[5].SetActive(false);
		Cameras[6].SetActive(false);

		MiyukiCamera.targetTexture = null;

		MiyukiCamera.enabled = true;

		Listener.enabled = true;

		OriginalPosition = Yandere.transform.position;
		OriginalRotation = Yandere.transform.rotation;

		Yandere.CharacterAnimation.Play("f02_henshin_00");
		Yandere.transform.parent = Miyuki;
		Yandere.enabled = false;

		Yandere.transform.localEulerAngles = new Vector3(0, 0, 0);
		Yandere.transform.localPosition = new Vector3(0, 0, 0);

		Yandere.Accessories[Yandere.AccessoryID].SetActive(false);

		Physics.SyncTransforms();

		AudioSource.PlayClipAtPoint(Catchphrase, transform.position);
		MyAudio.Play();

		Start();
	}

	void Start()
	{
		if (OriginalMesh == null)
		{
			OriginalMesh = MiyukiRenderer.sharedMesh;
			OriginalFace = MiyukiRenderer.materials[0].mainTexture;
			OriginalBody = MiyukiRenderer.materials[1].mainTexture;
		}

		MiyukiRenderer.sharedMesh = OriginalMesh;
		MiyukiRenderer.materials[0].mainTexture = this.OriginalFace;
		MiyukiRenderer.materials[1].mainTexture = this.OriginalBody;
		MiyukiRenderer.materials[2].mainTexture = this.OriginalBody;

		MiyukiHairRenderer.material.color = new Color(1, 1, 1, 0);
		WhiteMiyukiRenderer.materials[0].color = new Color(1, 1, 1, 0);
		WhiteMiyukiRenderer.materials[1].color = new Color(1, 1, 1, 0);
		WhiteMiyukiRenderer.materials[2].color = new Color(1, 1, 1, 0);

		Wand.gameObject.SetActive(true);
		Wand.transform.parent = transform.parent;
		Wand.localPosition = new Vector3(0, -0.6538f, 0.04405f);

		White.material.color = new Color(1, 1, 1, 1);

		Miyuki.gameObject.SetActive(false);

		if (MiyukiCostume.newRenderer != null)
		{
			MiyukiCostume.newRenderer.enabled = false;
		}

		HenshinSparkleBlast.SetActive(false);

		HenshinSparkles.emissionRate = 1;
		HenshinSparkles.Clear();
		HenshinSparkles.Stop();
		SpinSparkles.Clear();
		SpinSparkles.Stop();

		Sparkles.emissionRate = 1;
		Sparkles.startSize = .1f;
		Sparkles.Clear();
		Sparkles.Stop();

		Rotation = 3600;
		Timer = 0;
		Phase = 1;

		if (Debugging)
		{
			Time.timeScale = 1;
		}
	}

	void Update()
	{
		if (TransformingYandere)
		{
			if (Input.GetKeyDown("="))
			{
				MyAudio.pitch++;
				Time.timeScale++;
			}
		}

		if (TransformingYandere || Vector3.Distance(Yandere.transform.position, TV.position) < 15)
		{
			MiyukiCamera.enabled = true;

			if (Phase < 3)
			{
				Wand.localPosition = Vector3.Lerp(Wand.localPosition, new Vector3(0, -0.2833333f, 1), Time.deltaTime);
				Rotation = Mathf.Lerp(Rotation, 0, Time.deltaTime * 2);
				Wand.localEulerAngles = new Vector3(-90, 0, Rotation);
			}

			if (Phase == 1)
			{
				White.material.color -= new Color(0, 0, 0, Time.deltaTime);

				Timer += Time.deltaTime;

				if (Timer > 3)
				{
					White.material.color = new Color(1, 1, 1, 0);
					Timer = 0;
					Phase++;
				}
			}
			else if (Phase == 2)
			{
				if (!Sparkles.isPlaying)
				{
					Sparkles.Play();
				}

				Sparkles.startSize += Time.deltaTime * .25f;
				Sparkles.emissionRate += Time.deltaTime * 5;

				Timer += Time.deltaTime;

				if (Timer > 3)
				{
					White.material.color += new Color(1, 1, 1, Time.deltaTime);

					if (White.material.color.a >= 1)
					{
						Miyuki.localEulerAngles = new Vector3(0, 180, 45);
						Miyuki.localPosition = new Vector3(0, 0, .5f);

						Miyuki.gameObject.SetActive(true);
						Wand.gameObject.SetActive(false);

						if (TransformingYandere)
						{
							MiyukiHairRenderer.enabled = false;
							MiyukiRenderer.enabled = false;
							MiyukiHair.SetActive(false);

							Yandere.CharacterAnimation.Play("f02_henshin_00");
						}

						Sparkles.emissionRate = 1;
						Sparkles.startSize = .1f;
						Sparkles.Clear();
						Sparkles.Stop();

						Timer = 0;
						Phase++;
					}
				}
			}
			else if (Phase == 3)
			{
				White.material.color -= new Color(0, 0, 0, Time.deltaTime);

				Miyuki.localPosition -= new Vector3(Time.deltaTime * .1f, Time.deltaTime * .1f, 0);

				Rotation += Time.deltaTime;
				Miyuki.Rotate(0, Rotation * 360 * Time.deltaTime, 0);

				Timer += Time.deltaTime;

				if (Timer > 2)
				{
					if (!TransformingYandere)
					{
						float MiyukiColor = Timer - 2;

						MiyukiHairRenderer.material.color = new Color(1, 1, 1, MiyukiColor);
						WhiteMiyukiRenderer.materials[0].color = new Color(1, 1, 1, MiyukiColor);
						WhiteMiyukiRenderer.materials[1].color = new Color(1, 1, 1, MiyukiColor);
						WhiteMiyukiRenderer.materials[2].color = new Color(1, 1, 1, MiyukiColor);
					}

					if (Timer > 5)
					{
						Miyuki.localEulerAngles = new Vector3(0, 180, 0);
						Miyuki.localPosition = new Vector3(0, -.795f, 2);
						Timer = 0;
						Phase++;
					}
				}
			}
			else if (Phase == 4)
			{
				//Rotation += Time.deltaTime;
				Miyuki.Rotate(0, Rotation * 360 * Time.deltaTime, 0);

				Timer += Time.deltaTime;

				if (Timer > 1)
				{
					if (!HenshinSparkles.isPlaying)
					{
						HenshinSparkles.Play();
					}

					HenshinSparkles.emissionRate += Time.deltaTime * 100;

					if (Timer > 5)
					{
						Wand.gameObject.SetActive(true);
						Wand.parent = RightHand;

						Wand.localEulerAngles = new Vector3(0, 0, 90);
						Wand.localPosition = new Vector3(0, 0, 0);

						if (TransformingYandere)
						{
							MiyukiRenderer.enabled = true;
							Yandere.gameObject.SetActive(false);
						}

						MiyukiCostume.gameObject.SetActive(true);
						MiyukiHair.SetActive(true);

						if (MiyukiCostume.newRenderer != null)
						{
							MiyukiCostume.newRenderer.enabled = true;
						}

						MiyukiRenderer.sharedMesh = this.NudeMesh;
						MiyukiRenderer.materials[0].mainTexture = this.MiyukiFace;
						MiyukiRenderer.materials[1].mainTexture = this.MiyukiSkin;
						MiyukiRenderer.materials[2].mainTexture = this.MiyukiSkin;

						MiyukiHairRenderer.material.color = new Color(1, 1, 1, 0);
						WhiteMiyukiRenderer.materials[0].color = new Color(1, 1, 1, 0);
						WhiteMiyukiRenderer.materials[1].color = new Color(1, 1, 1, 0);
						WhiteMiyukiRenderer.materials[2].color = new Color(1, 1, 1, 0);

						Miyuki.localEulerAngles = new Vector3(15, -135, 15);
						WhiteMiyukiAnim.Play("f02_miyukiPose_00");
						MiyukiAnim.Play("f02_miyukiPose_00");

						HenshinSparkleBlast.SetActive(true);
						HenshinSparkles.emissionRate = 1;
						HenshinSparkles.Clear();
						HenshinSparkles.Stop();
						SpinSparkles.Clear();
						SpinSparkles.Stop();
						Timer = 0;
						Phase++;
					}
				}
			}
			else if (Phase == 5)
			{
				Timer += Time.deltaTime;

				if (Timer > 1)
				{
					White.material.color += new Color(0, 0, 0, Time.deltaTime);

					if (White.material.color.a >= 1)
					{
						if (TransformingYandere)
						{
							Cameras[1].SetActive(true);
							Cameras[2].SetActive(true);
							Cameras[3].SetActive(true);
							Cameras[4].SetActive(true);
							Cameras[5].SetActive(true);
							Cameras[6].SetActive(true);

							gameObject.SetActive(false);

							Yandere.transform.parent = null;
							Yandere.gameObject.SetActive(true);
							Yandere.transform.position = OriginalPosition;
							Yandere.transform.rotation = OriginalRotation;

							Yandere.Stance.Current = StanceType.Standing;
							Yandere.WeaponManager.Weapons[19].AnimID = 0;
							Yandere.SetAnimationLayers();
							Yandere.enabled = true;
							Yandere.CanMove = true;
							Yandere.Miyuki();

							transform.parent.gameObject.SetActive(false);

							Time.timeScale = 1;
						}
						else
						{
							Start();
						}
					}
				}
			}
		}
		else
		{
			MiyukiCamera.enabled = false;
		}
	}
}