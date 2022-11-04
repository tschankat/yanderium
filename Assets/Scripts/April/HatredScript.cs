using UnityEngine;

public class HatredScript : MonoBehaviour
{
	public DepthOfFieldScatter DepthOfField;
	public HomeDarknessScript HomeDarkness;
	public HomeCameraScript HomeCamera;
	public GrayscaleEffect Grayscale;
	public Bloom Bloom;

	public GameObject CrackPanel;

	public AudioSource Voiceover;

	public GameObject SenpaiPhoto;
	public GameObject RivalPhotos;
	public GameObject Character;
	public GameObject Panties;
	public GameObject Yandere;
	public GameObject Shrine;

	public Transform AntennaeR;
	public Transform AntennaeL;
	public Transform Corkboard;

	public UISprite CrackDarkness;
	public UISprite Darkness;
	public UITexture Crack;
	public UITexture Logo;

	public bool Begin = false;

	public float Timer = 0.0f;

	public int Phase = 0;

	public Texture[] CrackTexture;

	void Start()
	{
		this.Character.SetActive(false);
	}

	// [af] Leaving this commented JS code here; assuming it might be useful later.
	/*
	function Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			HomeCamera.transform.localEulerAngles = Vector3(0, 180, 0);
			HomeCamera.transform.position = Vector3(-2.271, 1.25, 2.7);

			DepthOfField.focalTransform = transform;

			transform.position = Vector3(-2.271312, 0, 1);
			transform.eulerAngles = Vector3(0, 0, 0);

			Character.active = true;

			Character.animation["f02_photoPose_00"].layer = 1;
			Character.animation.Play("f02_photoPose_00");

			Character.animation["f02_pantiesPose_00"].layer = 2;
			Character.animation.Play("f02_pantiesPose_00");

			Character.animation.Play("f02_idleShort_00");

			Character.animation["f02_photoPose_00"].weight = 0;
			Character.animation["f02_pantiesPose_00"].weight = 0;
			Character.animation["f02_pantiesPose_00"].speed = .5;

			HomeDarkness.enabled = false;
			HomeCamera.enabled = false;

			SenpaiPhoto.active = false;
			RivalPhotos.active = true;
			Panties.active = false;
			Yandere.active = false;
			Shrine.active = false;

			Grayscale.enabled = true;

			CrackDarkness.color.a = 2;
			Darkness.color.a = 1;
			Logo.color.a = 1;

			Begin = true;

			Phase = 0;
		}

		if (Begin)
		{
			if (Phase == 0)
			{
				Timer += Time.deltaTime;

				if (Timer < 2)
				{
					CrackDarkness.color.a -= Time.deltaTime;
				}

				if (Timer > 3)
				{
					Crack.mainTexture = CrackTexture[1];
				}

				if (Timer > 4)
				{
					Crack.mainTexture = CrackTexture[2];
				}

				if (Timer > 5)
				{
					Crack.mainTexture = CrackTexture[3];
				}

				if (Timer > 8)
				{
					CrackDarkness.color.a += Time.deltaTime;
				}

				if (Timer > 10)
				{
					CrackPanel.active = false;
					Logo.active = false;
					Voiceover.Play();
					Phase = 1;
					Timer = 0;
				}
			}
			else if (Phase == 1)
			{
				if (Timer < 1)
				{
					Darkness.color.a -= Time.deltaTime;
				}

				if (Timer > 6)
				{
					Darkness.color.a += Time.deltaTime;
				}

				if (Timer < 7)
				{
					HomeCamera.transform.position.z -= Time.deltaTime * .1;
				}
				else
				{
					HomeCamera.transform.localEulerAngles = Vector3(0, -60, 0);
					HomeCamera.transform.position = Vector3(-2.666667, 1.4, 2.666667);

					Character.animation["f02_photoPose_00"].weight = 1;

					transform.position = Vector3(-3.7, 0, 3);

					SenpaiPhoto.active = true;
					Phase = 2;
					Timer = 0;
				}

				Timer += Time.deltaTime;
			}
			else if (Phase == 2)
			{
				if (Timer < 1)
				{
					Darkness.color.a -= Time.deltaTime;
				}

				if (Timer > 6)
				{
					Darkness.color.a += Time.deltaTime;
				}

				if (Timer < 7)
				{
					HomeCamera.transform.Translate(Vector3.forward * Time.deltaTime * .1);
				}
				else
				{
					HomeCamera.transform.localEulerAngles = Vector3(0, -135, 0);
					HomeCamera.transform.position = Vector3(-2.75, 1.5, 2.15);

					Character.animation["f02_pantiesPose_00"].weight = 1;
					Character.animation["f02_pantiesPose_00"].time = 1;

					transform.eulerAngles = Vector3(0, 180, 0);
					transform.position = Vector3(-3.7, 0, 1.5);

					SenpaiPhoto.active = false;
					Panties.active = true;
					Phase = 3;
					Timer = 0;
				}

				Timer += Time.deltaTime;
			}
			else if (Phase == 3)
			{
				if (Timer < 1)
				{
					Darkness.color.a -= Time.deltaTime;
				}

				if (Timer > 7)
				{
					Darkness.color.a += Time.deltaTime;
				}

				if (Timer < 8)
				{
					HomeCamera.transform.Translate(Vector3.forward * Time.deltaTime * .1);
				}
				else
				{
					HomeCamera.transform.localEulerAngles = Vector3(0, 135, 0);
					HomeCamera.transform.position = Vector3(-.1, 1.5, 2.1);

					Character.animation["f02_photoPose_00"].weight = 0;
					Character.animation["f02_pantiesPose_00"].weight = 0;
					Character.animation.Play("f02_idle_00");

					transform.eulerAngles = Vector3(0, 90, 0);
					transform.position = Vector3(-.66666, 0, 1.66666);

					DepthOfField.focalTransform = Corkboard.transform;
					Panties.active = false;
					//Bloom.enabled = false;
					Phase = 4;
					Timer = 0;
				}

				Timer += Time.deltaTime;
			}
			else if (Phase == 4)
			{
				if (Timer < 1)
				{
					Darkness.color.a -= Time.deltaTime;
				}

				if (Timer > 10)
				{
					Darkness.color.a += Time.deltaTime;
				}

				if (Timer < 11)
				{
					HomeCamera.transform.Translate(Vector3.back * Time.deltaTime * .2);
				}
				else
				{
					Phase = 5;
					Timer = 0;
				}

				Timer += Time.deltaTime;
			}
		}
	}

	function LateUpdate ()
	{
		AntennaeR.localScale = Vector3(0, 0, 0);
		AntennaeL.localScale = Vector3(0, 0, 0);
	}
	*/
}
