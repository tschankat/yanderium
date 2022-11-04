using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallucinationScript : MonoBehaviour
{
	public SkinnedMeshRenderer YandereHairRenderer;
	public SkinnedMeshRenderer YandereRenderer;

	public SkinnedMeshRenderer RivalHairRenderer;
	public SkinnedMeshRenderer RivalRenderer;

	public Animation YandereAnimation;
	public Animation RivalAnimation;

	public YandereScript Yandere;

	public Material Black;

	public bool Hallucinate;

	public float Alpha;
	public float Timer;

	public int Weapon;

	public Renderer[] WeaponRenderers;
	public Renderer SawRenderer;

	public GameObject[] Weapons;
	public string[] WeaponName;

	void Start()
	{
		YandereHairRenderer.material = Black;
		RivalHairRenderer.material = Black;

		YandereRenderer.materials[0] = Black;
		YandereRenderer.materials[1] = Black;
		YandereRenderer.materials[2] = Black;

		RivalRenderer.materials[0] = Black;
		RivalRenderer.materials[1] = Black;
		RivalRenderer.materials[2] = Black;

		foreach (Renderer renderer in this.WeaponRenderers)
		{
			if (renderer != null)
			{
				renderer.material = Black;
			}
		}

		SawRenderer.material = Black;

		MakeTransparent();
	}

	void Update()
	{
		if (Yandere.Sanity < 33.33333f)
		{
			if (!Yandere.Aiming && Yandere.CanMove)
			{
				Timer += Time.deltaTime;
			}

			if (Timer > 6)
			{
				Weapon = Random.Range(1, 6);

				transform.position = Yandere.transform.position + Yandere.transform.forward;
				transform.eulerAngles = Yandere.transform.eulerAngles;

				YandereAnimation["f02_" + WeaponName[Weapon] + "LowSanityA_00"].time = 0;
				RivalAnimation["f02_" + WeaponName[Weapon] + "LowSanityB_00"].time = 0;

				YandereAnimation.Play("f02_" + WeaponName[Weapon] + "LowSanityA_00");
				RivalAnimation.Play("f02_" + WeaponName[Weapon] + "LowSanityB_00");

				if (Weapon == 1)
				{
					YandereAnimation.transform.localPosition = new Vector3(0, 0, 0);
				}
				else if (Weapon == 5)
				{
					YandereAnimation.transform.localPosition = new Vector3(-.25f, 0, 0);
				}
				else
				{
					YandereAnimation.transform.localPosition = new Vector3(-.5f, 0, 0);
				}

				foreach (GameObject weapon in this.Weapons)
				{
					if (weapon != null)
					{
						weapon.SetActive(false);
					}
				}

				Weapons[Weapon].SetActive(true);

				Hallucinate = true;
				Timer = 0;
			}
		}

		if (Hallucinate)
		{
			if (YandereAnimation["f02_" + WeaponName[Weapon] + "LowSanityA_00"].time < 3f)
			{
				Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime * .33333f);
			}
			else
			{
				Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * .33333f);
			}

			YandereHairRenderer.material.SetFloat("_Alpha", Alpha);
			RivalHairRenderer.material.SetFloat("_Alpha", Alpha);

			YandereRenderer.materials[0].SetFloat("_Alpha", Alpha);
			YandereRenderer.materials[1].SetFloat("_Alpha", Alpha);
			YandereRenderer.materials[2].SetFloat("_Alpha", Alpha);

			RivalRenderer.materials[0].SetFloat("_Alpha", Alpha);
			RivalRenderer.materials[1].SetFloat("_Alpha", Alpha);
			RivalRenderer.materials[2].SetFloat("_Alpha", Alpha);

			foreach (Renderer renderer in this.WeaponRenderers)
			{
				if (renderer != null)
				{
					renderer.material.SetFloat("_Alpha", Alpha);
				}
			}

			SawRenderer.material.SetFloat("_Alpha", Alpha);

			if (YandereAnimation["f02_" + WeaponName[Weapon] + "LowSanityA_00"].time ==
				YandereAnimation["f02_" + WeaponName[Weapon] + "LowSanityA_00"].length || Yandere.Aiming)
			{
				MakeTransparent();
				Hallucinate = false;
			}
		}
	}

	void MakeTransparent()
	{
		Alpha = 0;

		YandereHairRenderer.material.SetFloat("_Alpha", Alpha);
		RivalHairRenderer.material.SetFloat("_Alpha", Alpha);

		YandereRenderer.materials[0].SetFloat("_Alpha", Alpha);
		YandereRenderer.materials[1].SetFloat("_Alpha", Alpha);
		YandereRenderer.materials[2].SetFloat("_Alpha", Alpha);

		RivalRenderer.materials[0].SetFloat("_Alpha", Alpha);
		RivalRenderer.materials[1].SetFloat("_Alpha", Alpha);
		RivalRenderer.materials[2].SetFloat("_Alpha", Alpha);

		foreach (Renderer renderer in this.WeaponRenderers)
		{
			if (renderer != null)
			{
				renderer.material.SetFloat("_Alpha", Alpha);
			}
		}

		SawRenderer.material.SetFloat("_Alpha", Alpha);
	}
}